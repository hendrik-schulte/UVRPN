using UnityEditor;
using UnityEngine;
using UVRPN.Core;
using UVRPN.Utility;

namespace UVRPN.Edit
{
    [CustomEditor(typeof(VRPN_Tracker))]
//    [CanEditMultipleObjects]
    public class VRPN_TrackerEditor : VRPN_ClientEditor
    {
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

            trackPos = serializedObject.FindProperty("trackPosition");
            localPos = serializedObject.FindProperty("localPosition");
            invertPos = serializedObject.FindProperty("invertPos");
            scale = serializedObject.FindProperty("scale");

            trackRot = serializedObject.FindProperty("trackRotation");
            localRot = serializedObject.FindProperty("localRotation");
            invertRot = serializedObject.FindProperty("invertRot");

//            var window = EditorWindow.GetWindow(typeof(VRPN_TrackerEditor));
//            window.titleContent.text = "VRPN_Tracker";
//            window.titleContent.image = null;
        }

        protected override void OnChildInspectorGUI()
        {

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
            DrawInvertAxisProperty(invertPos);
            EditorGUILayout.PropertyField(scale);
        }

        private void DrawRotationPanel()
        {
            localRot.boolValue = EditorGUILayout.Toggle("Local", localRot.boolValue);
            DrawInvertAxisProperty(invertRot);
        }

        private static void DrawInvertAxisProperty(SerializedProperty invertAxis)
        {
            EditorGUILayout.BeginHorizontal();
            {
                var invert = invertAxis.objectReferenceValue as InvertAxis;

                if (invert == null)
                {
                    invertAxis.objectReferenceValue = invert = CreateInstance<InvertAxis>();
                }

                EditorGUILayout.LabelField("Invert Axis", GUILayout.MaxWidth(70));

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                GUILayout.FlexibleSpace();

                EditorGUILayout.LabelField("X", GUILayout.MaxWidth(15));
                invert.x = EditorGUILayout.Toggle(invert.x);
                EditorGUILayout.LabelField("Y", GUILayout.MaxWidth(15));
                invert.y = EditorGUILayout.Toggle(invert.y);
                EditorGUILayout.LabelField("Z", GUILayout.MaxWidth(15));
                invert.z = EditorGUILayout.Toggle(invert.z);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}