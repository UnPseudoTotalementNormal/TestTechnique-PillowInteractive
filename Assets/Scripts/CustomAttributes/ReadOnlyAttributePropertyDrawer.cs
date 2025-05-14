namespace CustomAttributes
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            bool _wasGUIEnabled = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(_position, _property, _label, true);
            GUI.enabled = _wasGUIEnabled;
        }
    }

}