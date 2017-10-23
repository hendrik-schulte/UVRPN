// unityVrpn.cpp : Defines the exported functions for the DLL application.
//

#include <map>
#include <stdexcept>
#include <string>
#include <vector>

#include "vrpn_Analog.h"
#include "vrpn_Button.h"
#include "vrpn_Tracker.h"

using namespace std;

#if defined (_WIN32)
  #if defined(unityVrpn_EXPORTS)
    #define UNITYVRPN_EXPORT __declspec(dllexport)
  #else
    #define UNITYVRPN_EXPORT __declspec(dllimport)
  #endif /* UnityVrpn_EXPORTS */
#else /* defined (_WIN32) */
 #define UNITYVRPN_EXPORT
#endif

////////////////////////////////////////////////////////////////////////////////
// Analog
////////////////////////////////////////////////////////////////////////////////

struct stampedAnalog{
    int frameCount;
    vrpn_Analog_Remote* analog;
    vector<double> *current;
};

void VRPN_CALLBACK handle_analog(void *userdata, vrpn_ANALOGCB b)
{
    stampedAnalog* ref = (stampedAnalog*)userdata;
    ref->current->resize(b.num_channel);
    for (int i = 0; i < b.num_channel; i++)
        (*ref->current)[i] = (double)b.channel[i];
}

map<string, stampedAnalog*> analogMap = map<string, stampedAnalog*>();


extern "C" UNITYVRPN_EXPORT double vrpnAnalogExtern(char* _address, int channel, int frameCount){
    string address = string(_address);
    stampedAnalog* ref = 0;
    try {
        ref = analogMap.at(address);
    }
    catch (out_of_range){
        ref = new stampedAnalog;
        ref->frameCount = -1;
        ref->analog = new vrpn_Analog_Remote(_address);
        ref->current = new vector<double>();
        ref->analog->register_change_handler((void*)ref, handle_analog);
        analogMap[address] = ref;
    }
    if (ref->frameCount != frameCount){
        ref->analog->mainloop();
        ref->frameCount = frameCount;
    }

    double result = 0;
    try{
        result = ref->current->at(channel);
    }
    catch (out_of_range){
    }
    return result;
}

////////////////////////////////////////////////////////////////////////////////
// Tracker
////////////////////////////////////////////////////////////////////////////////

struct stampedTracker{
    int frameCount;
    vrpn_Tracker_Remote* tracker;
    vector<double> *current;
};

void VRPN_CALLBACK handle_tracker(void *userdata, vrpn_TRACKERCB b) {
    stampedTracker* ref = (stampedTracker*)userdata;
    unsigned int arraySizeNeeded = 7 * (b.sensor + 1);
    if (ref->current->size() < arraySizeNeeded)
        ref->current->resize(arraySizeNeeded);

    for (int i = 0; i < 3; i++)
        (*ref->current)[7 * b.sensor + i] = (double)b.pos[i];
    //(*ref->current)[i] = (double) b.pos[i];

    for (int i = 0; i < 4; i++)
        (*ref->current)[7 * b.sensor + i + 3] = (double)b.quat[i];
    //(*ref->current)[i + 3] = (double) b.quat[i];
}

map<string, stampedTracker*> trackerMap = map<string, stampedTracker*>();


extern "C" UNITYVRPN_EXPORT double vrpnTrackerExtern(char* _address, int channel, int component, int frameCount){
    string address = string(_address);
    stampedTracker* ref = 0;
    try {
        ref = trackerMap.at(address);
    }
    catch (out_of_range){
        ref = new stampedTracker;
        ref->frameCount = -1;
        ref->tracker = new vrpn_Tracker_Remote(_address);
        ref->current = new vector<double>();
        //ref->current->resize(7);
        ref->tracker->register_change_handler((void*)ref, handle_tracker);
        trackerMap[address] = ref;
    }
    if (ref->frameCount != frameCount){
        ref->tracker->mainloop();
        ref->frameCount = frameCount;
    }

    double result = 0;
    try{
        result = ref->current->at(channel * 7 + component);
    }
    catch (out_of_range){
        result = -505;
    }
    return result;
}

////////////////////////////////////////////////////////////////////////////////
// Button
////////////////////////////////////////////////////////////////////////////////

struct stampedButton{
    int frameCount;
    vrpn_Button_Remote* button;
    vector<bool> *current;
};

void VRPN_CALLBACK handle_button(void *userdata, vrpn_BUTTONCB b)
{
    stampedButton* ref = (stampedButton*)userdata;
    if ((unsigned long int) b.button >= ref->current->size()){
        ref->current->resize(b.button + 1);
    }

    (*ref->current)[b.button] = b.state != 0;
}

map<string, stampedButton*> buttonMap = map<string, stampedButton*>();


extern "C" UNITYVRPN_EXPORT bool vrpnButtonExtern(char* _address, int channel, int frameCount){
    string address = string(_address);
    stampedButton* ref = 0;
    try {
        ref = buttonMap.at(address);
    }
    catch (out_of_range){
        ref = new stampedButton;
        ref->frameCount = -1;
        ref->button = new vrpn_Button_Remote(_address);
        ref->current = new vector<bool>();
        ref->button->register_change_handler((void*)ref, handle_button);
        buttonMap[address] = ref;
    }
    if (ref->frameCount != frameCount){
        ref->button->mainloop();
        ref->frameCount = frameCount;
    }

    bool result = false;
    try{
        result = ref->current->at(channel);
    }
    catch (out_of_range){
    }
    return result;
}
