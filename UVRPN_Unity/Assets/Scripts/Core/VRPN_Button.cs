using UnityEngine;
using UVRPN.Events;

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
        public ButtonEvent OnButtonUp = new ButtonEvent();
        [Tooltip("This is triggered when a button is released.")]
        public ButtonEvent OnButtonDown = new ButtonEvent();
        [Tooltip("This is triggered every frame as long as the button is pressed.")]
        public ButtonEvent OnButtonHold = new ButtonEvent();

        private void Start()
        {
            if (debugLog)
            {
                OnButtonDown.AddListener((int c) => print("Button " + channel + " Down"));
                OnButtonUp.AddListener((int c) => print("Button " + channel + " Up"));
                OnButtonHold.AddListener((int c) => print("Button " + channel + " Hold"));
            }
        }

        private void Update()
        {
            var pressed = host.IsButtonPressed(tracker, channel);

            if (previouslyPressed && !pressed) OnButtonUp.Invoke(channel);
            if (previouslyPressed && pressed) OnButtonHold.Invoke(channel);
            if (!previouslyPressed && pressed) OnButtonDown.Invoke(channel);

            previouslyPressed = pressed;
        }
    }
}