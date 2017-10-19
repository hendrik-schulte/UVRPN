using UnityEngine;
using System.Collections;
using UVRPN.Utility;

namespace UVRPN.Core
{
    /// <summary>
    /// This component reveives spatial data via VRPN_NativeBridge and applies it to the attached transform.
    /// </summary>
    public class VRPN_Tracker : VRPN_Client
    {
        #region Position Fields

        [SerializeField]
        [HideInInspector]
        private bool trackPosition = true;
        [SerializeField]
        [HideInInspector]
        private bool localPosition = true;
        [SerializeField]
        [HideInInspector]
        private InvertAxis invertPos;
        [SerializeField]
        [HideInInspector]
        [Range(0, 100)]
        private float scale = 1;


        private Coroutine positionCoroutine;

        #endregion

        #region Rotation Fields

        [SerializeField]
        [HideInInspector]
        private bool trackRotation = true;
        [SerializeField]
        [HideInInspector]
        private bool localRotation = true;
        [SerializeField]
        [HideInInspector]
        private InvertAxis invertRot;
        private Coroutine rotationCoroutine;

        #endregion

        #region Properties
        
        public bool TrackPosition
        {
            get { return trackPosition; }
            set
            {
                trackPosition = value;
                if (trackPosition && Application.isPlaying)
                {
                    StopCoroutine(positionCoroutine);
                    positionCoroutine = StartCoroutine(Position());
                }
            }
        }

        public bool TrackRotation
        {
            get { return trackRotation; }
            set
            {
                trackRotation = value;
                if (trackRotation && Application.isPlaying)
                {
                    StopCoroutine(rotationCoroutine);
                    rotationCoroutine = StartCoroutine(Rotation());
                }
            }
        }

        #endregion

        protected virtual void Start()
        {
            if (trackPosition)
            {
                positionCoroutine = StartCoroutine(Position());
            }

            if (trackRotation)
            {
                rotationCoroutine = StartCoroutine(Rotation());
            }
        }

        #region Position Functions
        
        protected IEnumerator Position()
        {
            while (true)
            {
                var output = Process(invertPos.Apply(host.GetPosition(tracker, channel)) * scale);

                if (localPosition) transform.localPosition = output;
                else transform.position = output;

                yield return null;
            }
        }

        protected virtual Vector3 Process(Vector3 input)
        {
            return input;
        }

        #endregion

        #region Rotation Functions

        protected IEnumerator Rotation()
        {
            while (true)
            { 
                var output = Process(invertRot.Apply(host.GetRotation(tracker, channel)));
                
                if (localRotation) transform.localRotation = output;
                else transform.rotation = output;

                yield return null;
            }
        }

        /// <summary>
        /// Processes the rotation of this tracker.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        protected virtual Quaternion Process(Quaternion input)
        {
            return input;
        }

        #endregion
    }
}