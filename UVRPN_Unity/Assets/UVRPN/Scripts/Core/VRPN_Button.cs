using UnityEngine;
using UVRPN.Events;

namespace UVRPN.Core
{
    /// <summary>
    /// This component receives button event via VRPN_NativeBridge and distributes them by the given UnityEvents.
    /// </summary>
    public sealed class VRPN_Button : VRPN_Client
    {
        [SerializeField] private bool debugLog;

        /// <summary>
        /// This is true during the frame the button is pressed down.
        /// </summary>
        public bool ButtonDown { get; private set; }
        
        /// <summary>
        /// This is true when the button is currently down.;
        /// </summary>
        public bool ButtonHold { get; private set; }

        /// <summary>
        /// This is true during the frame the button is released.
        /// </summary>
        public bool ButtonUp { get; private set; }

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
                OnButtonDown.AddListener((int c) => print("Button " + c + " Down"));
                OnButtonUp.AddListener((int c) => print("Button " + c + " Up"));
                OnButtonHold.AddListener((int c) => print("Button " + c + " Hold"));
            }
        }

        private void Update()
        {            
            ButtonUp = ButtonDown = false;
            
            var pressed = host.IsButtonPressed(tracker, channel);
            
            if (ButtonHold)
            {   
                if (pressed) OnButtonHold.Invoke(channel);

                else
                {
                    ButtonUp = true;
                    OnButtonUp.Invoke(channel);
                }
            }
            else if (pressed)
            {
                ButtonDown = true;
                OnButtonDown.Invoke(channel);
            }

            ButtonHold = pressed;
        }
    }
}