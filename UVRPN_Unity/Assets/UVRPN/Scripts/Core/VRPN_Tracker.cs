using System;
using UnityEngine;
using System.Collections;
using UVRPN.Utility;

namespace UVRPN.Core
{
    /// <summary>
    /// This component reveives spatial data via VRPN and applies it to the attached transform.
    /// </summary>
    public class VRPN_Tracker : VRPN_Client
    {
        #region Position Fields

        [SerializeField] [HideInInspector] private bool trackPosition = true;
        [SerializeField] [HideInInspector] private bool localPosition = true;
        [SerializeField] [HideInInspector] private InvertAxis invertPos;
        [SerializeField] [HideInInspector] [Range(0, 100)] private float scale = 1;


        private Coroutine positionCoroutine;

        #endregion

        #region Rotation Fields

        [SerializeField] [HideInInspector] private bool trackRotation = true;
        [SerializeField] [HideInInspector] private bool localRotation = true;
        [SerializeField] [HideInInspector] private InvertAxis invertRot;
        private Coroutine rotationCoroutine;

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if this tracker has been found at least once.
        /// </summary>
        public bool InitiallyConnected {get { return initiallyConnected; }
        }

        [SerializeField] [HideInInspector] private bool initiallyConnected;

        /// <summary>
        /// When this is true, the position of this tracker is applied.
        /// </summary>
        public bool TrackPosition
        {
            get { return trackPosition; }
            set
            {
                trackPosition = value;

                if (!trackPosition || !Application.isPlaying) return;

                StopCoroutine(positionCoroutine);
                positionCoroutine = StartCoroutine(Position());
            }
        }

        /// <summary>
        /// When this is true, the rotation of this tracker is applied.
        /// </summary>
        public bool TrackRotation
        {
            get { return trackRotation; }
            set
            {
                trackRotation = value;

                if (!trackRotation || !Application.isPlaying) return;

                StopCoroutine(rotationCoroutine);
                rotationCoroutine = StartCoroutine(Rotation());
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

        private IEnumerator Position()
        {
            yield return StartCoroutine(WaitForInitialConnection());
            
            while (true)
            {
                var output = Process(invertPos.Apply(host.GetPosition(tracker, channel)) * scale);

                if (localPosition) transform.localPosition = output;
                else transform.position = output;

                yield return null;
            }
        }

        /// <summary>
        /// Takes the position vector right before it is applied to the transform. Allows inherited classes to 
        /// intervene by overriding the function.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual Vector3 Process(Vector3 input)
        {
            return input;
        }

        #endregion

        #region Rotation Functions

        private IEnumerator Rotation()
        {
            yield return StartCoroutine(WaitForInitialConnection());

            while (true)
            {
                var output = Process(invertRot.Apply(host.GetRotation(tracker, channel)));

                if (localRotation) transform.localRotation = output;
                else transform.rotation = output;

                yield return null;
            }
        }

        /// <summary>
        /// Takes the rotation quaternion right before it is applied to the transform. Allows inherited classes to 
        /// intervene by overriding the function.
        /// </summary>
        /// <param name="input"></param>
        protected virtual Quaternion Process(Quaternion input)
        {
            return input;
        }

        #endregion
        
        /// <summary>
        /// This runs until tracker data is received and sets 'initiallyConnected' to true afterwards.
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitForInitialConnection()
        {
            while (!InitiallyConnected)
            {
                var temp = host.GetPosition(tracker, channel);
                
                //Vector (505, 505, 505) is hard coded in native library
                if (Math.Abs(temp.x - (-505)) < 0.001f && 
                    Math.Abs(temp.y - (-505)) < 0.001f &&
                    Math.Abs(temp.z - (-505)) < 0.001f)
                {
                    yield return null;
                    continue;
                }

                initiallyConnected = true;
            }
        }
    }
}