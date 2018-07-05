using System;
using UnityEngine;

namespace UVRPN.Utility
{
    /// <summary>
    /// Utility class for inverting each axis.
    /// </summary>
    [Serializable]
    public class InvertAxis
    {
        public bool x, y, z;

        public Quaternion Apply(Quaternion quat)
        {
            var euler = quat.eulerAngles;

            return Quaternion.Euler(X(euler.x), Y(euler.y), Z(euler.z));
        }

        public Vector3 Apply(Vector3 vec)
        {
            return new Vector3
            {
                x = X(vec.x),
                y = Y(vec.y),
                z = Z(vec.z)
            };
        }

        private float X(float value)
        {
            if (x) return -value;
            return value;
        }

        private float Y(float value)
        {
            if (y) return -value;
            return value;
        }

        private float Z(float value)
        {
            if (z) return -value;
            return value;
        }

        public override string ToString()
        {
            return "Invert [X: " + x + " Y: " + y + " Z: " + z + "]";
        }
    }
}
