using UnityEditor;
using UnityEngine;
using UVRPN.Core;

namespace UVRPN.Edit
{
    [CustomEditor(typeof(VRPN_Analog))]
    [CanEditMultipleObjects]
    public class VRPN_AnalogEditor : VRPN_ClientEditor
    {
        protected override void OnChildInspectorGUI()
        {
        }

        protected override void OnDrawChannel()
        {
            EditorGUILayout.BeginHorizontal();
            {
                base.OnDrawChannel();
                EditorGUILayout.LabelField("+ " + (channel.intValue + 1), GUILayout.MaxWidth(100));
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}