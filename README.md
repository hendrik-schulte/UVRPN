# UVRPN - [VRPN](https://github.com/vrpn/vrpn/wiki) wrapper for Unity3D

You can build the plugin from scratch, or use the download in the releases, or use the Unity package in the releases which includes built plugins for Windows, OS X, Linux, and Android.

Recently tested with an *ART DTrack* device under Windows 64bit and Unity 2017.2.

#### Features
- Easy integration of multiple VRPN tracking devices
- Supports Trackers, Buttons and Analog data
- No external application or heavy-weight framework needed 
- Nice interface with custom editors

Getting Started
---------------
1. Download and import the [package](https://github.com/hendrik-schulte/UVRPN/raw/master/packages/UVRPN_V_1.4.unitypackage).
2. Add a **VRPN_Manager** component to any object in the scene.
    * *Hostname*:  The IP address or hostname of the VRPN server / *localhost*.
3. Add a **VRPN_Tracker**, **VRPN_Button** or **VRPN_Analog** component to any object you want to track with VRPN. Configure it as follows:
    * *Host reference*: The GameObject with the **VRPN_Manager** component you want to use for this object.
    * *Tracker*: The name of the tracker on the VRPN server.
    * *Channel*: Channel on the server.

## UVRPN Plugin

The the *.unitypackage* only contains *.dll* files. The source is included in /UVRPN_Unity/Assets/Scripts/.

## Native Plugin

### Source

The source is located in /native_source/.

### Compiling

Be sure to initialize/update the vrpn submodule in order to compile the plugin.
```
mkdir build
cd build
ccmake ../
make
```

### Reference

``` 
public static double Analog(string address, int channel)
```

This gets an analog value from the vrpn address and the channel.  When first called with a new address, the vrpn connection will be created.  An address's values will be updated at most once per frame.

``` 
public static bool Button(string address, int channel)
```

This gets a boolean value from the vrpn address and the channel.  When first called with a new address, the vrpn connection will be created.  An address's values will be updated at most once per frame.

``` 
public static Vector3 TrackerPos(string address, int channel)
```

This gets position component of a tracker at the vrpn address and the channel.  When first called with a new address, the vrpn connection will be created.  An address's values will be updated at most once per frame.

``` 
public static Quaternion TrackerQuat(string address, int channel)
```

This gets rotation component of a tracker at the /vrpn address and the channel.  When first called with a new address, the vrpn connection will be created.  An address's values will be updated at most once per frame.

## Acknowledgement

UVRPN has been forked from [arviceblot/unityVRPN](https://github.com/arviceblot/unityVRPN).
