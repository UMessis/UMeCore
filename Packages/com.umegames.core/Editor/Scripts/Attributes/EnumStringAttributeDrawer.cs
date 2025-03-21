namespace UMeGames.Core.Attributes
{
    using System;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(EnumStringAttribute))]
    public class EnumStringAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnumStringAttribute enumAttribute = (EnumStringAttribute)attribute;

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use EnumString with a string field.");
                return;
            }

            Type enumType = enumAttribute.EnumType;
            string currentValue = property.stringValue;
            string[] enumNames = Enum.GetNames(enumType);

            int selectedIndex = Array.IndexOf(enumNames, currentValue);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, enumNames);
            property.stringValue = enumNames[selectedIndex];
        }
    }
}