using UnityEngine;
using UnityEngine.Events;

namespace UVRPN.Core
{
    /// <summary>
    /// This component receives button event via VRPN_NativeBridge and distributes them by the given UnityEvents.
    /// </summary>
    public sealed class VRPN_Button : VRPN_Client
    {
        [SerializeField]
        private bool debugLog;

        private bool previouslyPressed;

        [Header("Events")]
        [Tooltip("This is triggered when a button is pressed.")]
        public UnityEvent OnButtonUp = new UnityEvent();
        [Tooltip("This is triggered when a button is released.")]
        public UnityEvent OnButtonDown = new UnityEvent();
        [Tooltip("This is triggered every frame as long as the button is pressed.")]
        public UnityEvent OnButtonHold = new UnityEvent();

        private void Start()
        {
            if (debugLog)
            {
                OnButtonDown.AddListener(() => print("Button " + channel + " Down"));
                OnButtonUp.AddListener(() => print("Button " + channel + " Up"));
                OnButtonHold.AddListener(() => print("Button " + channel + " Hold"));
            }
        }

        private void Update()
        {
            var pressed = host.IsButtonPressed(tracker, channel);

            if (previouslyPressed && !pressed) OnButtonUp.Invoke();
            if (previouslyPressed && pressed) OnButtonHold.Invoke();
            if (!previouslyPressed && pressed) OnButtonDown.Invoke();

            previouslyPressed = pressed;
        }
    }
}