using UnityEngine;

namespace UVRPN.Core
{
    /// <summary>
    /// This class is required for a VRPN_Client to work and forwards all VRPN calls to the right server address.
    /// </summary>
    public class VRPN_Manager : MonoBehaviour
    {
        #region Host Name
        
        [SerializeField]
        private string hostname = "localhost";

        private string _hostname;
        
        /// <summary>
        /// The address the VRPN server is located at.
        /// </summary>
        public string Hostname
        {
            get { return hostname; }
            set
            {
                hostname = value; 
                AssembleHostname();
            }
        }

        private string GetTrackerAdress(string tracker)
        {
            return tracker + _hostname;
        }

        /// <summary>
        /// Assembles the tracker and hostname to a single string.
        /// </summary>
        private void AssembleHostname()
        {
            _hostname = "@" + hostname;
        }

        private void OnValidate()
        {
            AssembleHostname();
        }

        private void Awake()
        {
            AssembleHostname();
        }

        #endregion

        #region Button

        /// <summary>
        /// Returns the current status of the given button at the given channel.
        /// </summary>
        /// <param name="tracker">Tracking device (e.g. DTrack)</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public bool IsButtonPressed(string tracker, int channel)
        {
            return VRPN_NativeBridge.Button(GetTrackerAdress(tracker), channel);
        }

        #endregion

        #region Analog

        /// <summary>
        /// Returns the current analog value at the given channel.
        /// </summary>
        /// <param name="tracker">Tracking device (e.g. DTrack)</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public double GetAnalog(string tracker, int channel)
        {
            return VRPN_NativeBridge.Analog(GetTrackerAdress(tracker), channel);
        }

        /// <summary>
        /// Returns the analog data of the given tracker and the given channel (and channel + 1) as a vector.
        /// </summary>
        /// <param name="tracker">Tracking device (e.g. DTrack)</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public Vector2 GetAnalogVec(string tracker, int channel)
        {
            var address = GetTrackerAdress(tracker);

            return new Vector2
            {
                x = (float)VRPN_NativeBridge.Analog(address, channel),
                y = (float)VRPN_NativeBridge.Analog(address, channel + 1)
            };
        }

        #endregion

        #region Tracker

        /// <summary>
        /// Returns the position of the given tracker at the given channel.
        /// </summary>
        /// <param name="tracker">Tracking device (e.g. DTrack)</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public Vector3 GetPosition(string tracker, int channel)
        {
            return VRPN_NativeBridge.TrackerPos(GetTrackerAdress(tracker), channel);
        }

        /// <summary>
        /// Returns the rotation of the given tracker at the given channel.
        /// </summary>
        /// <param name="tracker">Tracking device (e.g. DTrack)</param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public Quaternion GetRotation(string tracker, int channel)
        {
            return VRPN_NativeBridge.TrackerQuat(GetTrackerAdress(tracker), channel);
        }

        #endregion
    }
}
