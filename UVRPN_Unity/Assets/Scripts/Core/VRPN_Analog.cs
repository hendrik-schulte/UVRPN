using UnityEngine;
using UVRPN.Events;
using UVRPN.Utility;

namespace UVRPN.Core
{
    /// <summary>
    /// This component receives analog stick data via VRPN_NativeBridge and distributes it via an UnityEvent.
    /// </summary>
    public class VRPN_Analog : VRPN_Client
    {
        [Header("Output")]
        [NonEditable]
        [SerializeField]
        private Vector2 analog;
        [Header("Events")]
        [Tooltip("This event is triggered every frame.")]
        public AnalogEvent OnAnalog = new AnalogEvent();

        public Vector2 Analog {get { return analog; }} 

        private void Update()
        {
            analog = host.GetAnalogVec(tracker, channel);

            OnAnalog.Invoke(analog);
        }
    }
}
