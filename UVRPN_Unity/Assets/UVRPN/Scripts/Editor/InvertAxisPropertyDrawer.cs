#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UVRPN.Utility;

namespace UVRPN.Edit
{
    [CustomPropertyDrawer(typeof(InvertAxis))]
    public class InvertAxisPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            // Calculate rects
            var leftPos = position.width / 2.6f;
            
            var labelRect = new Rect(position.x, position.y, 100, position.height);
            
            var rectx1 = new Rect(position.x + leftPos + 000, position.y, 20, position.height);
            var rectx2 = new Rect(position.x + leftPos + 020, position.y, 50, position.height);

            var recty1 = new Rect(position.x + leftPos + 050, position.y, 20, position.height);
            var recty2 = new Rect(position.x + leftPos + 070, position.y, 50, position.height);

            var rectz1 = new Rect(position.x + leftPos + 100, position.y, 20, position.height);
            var rectz2 = new Rect(position.x + leftPos + 120, position.y, 50, position.height);

            EditorGUI.LabelField(labelRect, "Invert Axis");

            EditorGUI.LabelField(rectx1, "X");
            EditorGUI.PropertyField(rectx2, property.FindPropertyRelative("x"), GUIContent.none);
            
            EditorGUI.LabelField(recty1, "Y");
            EditorGUI.PropertyField(recty2, property.FindPropertyRelative("y"), GUIContent.none);
            
            EditorGUI.LabelField(rectz1, "Z");
            EditorGUI.PropertyField(rectz2, property.FindPropertyRelative("z"), GUIContent.none);
            
            EditorGUI.EndProperty();
        }
    }
}
#endif