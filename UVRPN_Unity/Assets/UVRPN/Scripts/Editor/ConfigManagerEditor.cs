#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UVRPN.Utility;

namespace UVRPN.Edit
{
    [CustomEditor(typeof(VRPN_ConfigManager))]
    public class ConfigManagerEditor : Editor
    {
        private int spacing = 5;

        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "m_Script");
            
            var config = ((VRPN_ConfigManager) target);

            if (GUILayout.Button("Save Config"))
            {
                config.SaveConfig();
            }

            GUILayout.Label("Saves current hostname to '" + config.GetFilename() + "'.");
            
            GUILayout.Space(spacing);
            
            if (GUILayout.Button("Load Config"))
            {
                config.LoadConfig();
            }

            GUILayout.Label("Loads hostname from '" + config.GetFilename() + "'.");


            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif