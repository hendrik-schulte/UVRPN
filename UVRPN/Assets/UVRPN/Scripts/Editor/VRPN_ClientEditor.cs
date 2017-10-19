using UnityEditor;
using UnityEngine;
using UVRPN.Core;

namespace UVRPN.Edit
{
    [CustomEditor(typeof(VRPN_Client), true)]
    [CanEditMultipleObjects]
    public class VRPN_ClientEditor : Editor
    {
        protected SerializedProperty channel;
        protected SerializedProperty tracker;

        protected virtual void OnEnable()
        {
            channel = serializedObject.FindProperty("channel");
            tracker = serializedObject.FindProperty("tracker");
        }
        
        public sealed override void OnInspectorGUI()
        {
            serializedObject.Update();
            var client = target as VRPN_Client;

            EditorGUILayout.LabelField("Connection Settings", EditorStyles.boldLabel);
            client.Host = (VRPN_Manager)EditorGUILayout.ObjectField("Host Reference", client.Host, typeof(VRPN_Manager), true);

            EditorGUILayout.BeginHorizontal();
            {
                tracker.stringValue = EditorGUILayout.TextField("Tracker", tracker.stringValue, GUILayout.MinWidth(250));
                if (client.Host)
                {
                    EditorGUILayout.LabelField("@" + client.Host.Hostname, GUILayout.MaxWidth(100));
                }
                else
                {
                    EditorGUILayout.LabelField("Connect Host!", GUILayout.MaxWidth(100));
                }
            }
            EditorGUILayout.EndHorizontal();

            OnDrawChannel();

//            EditorGUILayout.Space();
            OnChildInspectorGUI();
            DrawPropertiesExcluding(serializedObject, "m_Script");

            if (GUI.changed) EditorUtility.SetDirty(client);
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnChildInspectorGUI() { }

        protected virtual void OnDrawChannel()
        {
            channel.intValue = Mathf.Clamp(EditorGUILayout.IntField("Channel", channel.intValue, GUILayout.MinWidth(250)), 0 , int.MaxValue);
        }
    }
}
