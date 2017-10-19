using UnityEngine;

namespace UVRPN.Core
{
    /// <summary>
    /// This is the base class for all VRPN_NativeBridge components.
    /// </summary>
    public abstract class VRPN_Client : MonoBehaviour
    {
        #region VRPN_NativeBridge Settings

        [Header("Connection Settings")]
        [SerializeField]
        [HideInInspector]
        [Tooltip("This should reference a VRPN_Manager object.")]
        protected VRPN_Manager host;
        [SerializeField]
        [HideInInspector]
        [Tooltip("This is the VRPN_NativeBridge Server name. For ART devices this is 'DTrack'.")]
        protected string tracker;
        [SerializeField]
        [HideInInspector]
        [Tooltip("This is the VRPN_NativeBridge channel.")]
        protected int channel;

        #endregion

        #region Properties

        public VRPN_Manager Host
        {
            get { return host; }
            set { host = value; }
        }

        #endregion
    }
}
