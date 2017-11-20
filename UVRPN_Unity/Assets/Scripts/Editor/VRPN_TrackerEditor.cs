using UnityEditor;
using UVRPN.Core;

namespace UVRPN.Edit
{
    [CustomEditor(typeof(VRPN_Tracker))]
    public class VRPN_TrackerEditor : VRPN_ClientEditor
    {
        private SerializedProperty isConnected;
        
        private SerializedProperty trackPos;
        private SerializedProperty localPos;
        private SerializedProperty invertPos;
        private SerializedProperty scale;

        private SerializedProperty trackRot;
        private SerializedProperty localRot;
        private SerializedProperty invertRot;

        protected override void OnEnable()
        {
            base.OnEnable();

            isConnected = serializedObject.FindProperty("initiallyConnected");
            
            trackPos = serializedObject.FindProperty("trackPosition");
            localPos = serializedObject.FindProperty("localPosition");
            invertPos = serializedObject.FindProperty("invertPos");
            scale = serializedObject.FindProperty("scale");

            trackRot = serializedObject.FindProperty("trackRotation");
            localRot = serializedObject.FindProperty("localRotation");
            invertRot = serializedObject.FindProperty("invertRot");
        }

        protected override void OnChildInspectorGUI()
        {
            EditorGUILayout.LabelField("Initially Connected", isConnected.boolValue.ToString());
            
            using (var posGroup = new EditorGUILayout.ToggleGroupScope("Position Tracking", trackPos.boolValue))
            {
                trackPos.boolValue = posGroup.enabled;
                if(trackPos.boolValue) DrawPositionPanel();
            }

            EditorGUILayout.Space();

            using (var rotGroup = new EditorGUILayout.ToggleGroupScope("Rotation Tracking", trackRot.boolValue))
            {
                trackRot.boolValue = rotGroup.enabled;
                if (trackRot.boolValue) DrawRotationPanel();
            }

            EditorGUILayout.Space();
        }

        private void DrawPositionPanel()
        {
            localPos.boolValue = EditorGUILayout.Toggle("Local", localPos.boolValue);
            EditorGUILayout.PropertyField(invertPos);
            EditorGUILayout.PropertyField(scale);
        }

        private void DrawRotationPanel()
        {
            localRot.boolValue = EditorGUILayout.Toggle("Local", localRot.boolValue);
            EditorGUILayout.PropertyField(invertRot);
        }
    }
}