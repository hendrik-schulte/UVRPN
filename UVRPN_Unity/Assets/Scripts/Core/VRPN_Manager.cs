using UnityEngine;

namespace UVRPN.Core
{
    public class VRPN_Manager : MonoBehaviour
    {
        [SerializeField]
        private string hostname = "localhost";

        private string _hostname;

        public string Hostname
        {
            get { return hostname; }
            set { hostname = value; }
        }

        #region Button

        public bool IsButtonPressed(string tracker, int channel)
        {
            var address = GetTrackerAdress(tracker);
            
            return VRPN_NativeBridge.Button(address, channel);
        }

        #endregion

        #region Analog

        public double GetAnalog(string tracker, int channel)
        {
            return VRPN_NativeBridge.Analog(GetTrackerAdress(tracker), channel);
        }

        public Vector2 GetAnalogVec(string tracker, int channel)
        {
            var address = GetTrackerAdress(tracker);

            return new Vector2()
            {
                x = (float)VRPN_NativeBridge.Analog(address, channel),
                y = (float)VRPN_NativeBridge.Analog(address, channel + 1)
            };
        }

        #endregion

        #region Tracker

        public Vector3 GetPosition(string tracker, int channel)
        {
            return VRPN_NativeBridge.TrackerPos(GetTrackerAdress(tracker), channel);
        }

        public Quaternion GetRotation(string tracker, int channel)
        {
            return VRPN_NativeBridge.TrackerQuat(GetTrackerAdress(tracker), channel);
        }

        #endregion

        private string GetTrackerAdress(string tracker)
        {
            return tracker + _hostname;
        }

        private void OnValidate()
        {
            _hostname = "@" + hostname;
        }
    }
}
