#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UVRPN.Core;

namespace UVRPN.Edit
{
    [CustomEditor(typeof(VRPN_Manager))]
    public class VRPN_ManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var settings = target as VRPN_Manager;

            if (settings == null) return;

            settings.Hostname = EditorGUILayout.TextField("Host: (IP/localhost)", settings.Hostname);

            if (GUI.changed) EditorUtility.SetDirty(settings);
        }
    }
}
#endif