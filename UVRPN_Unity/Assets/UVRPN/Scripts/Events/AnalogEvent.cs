using System;
using UnityEngine;
using UnityEngine.Events;

namespace UVRPN.Events
{
    [Serializable]
    public class AnalogEvent : UnityEvent<Vector2>
    {
    }
}