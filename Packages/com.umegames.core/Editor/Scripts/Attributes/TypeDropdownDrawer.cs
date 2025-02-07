namespace UMeGames.Core
{
    using System;
    using System.Collections.Generic;
    using Attributes;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
    public class TypeDropdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TypeDropdownAttribute typeDropdown = attribute as TypeDropdownAttribute;
            Type baseType = typeDropdown.baseType;
            List<Type> types = ReflectionUtils.GetAllTypesWithBaseClass(baseType);

            string[] typeNames = new string[types.Count];
            for (int i = 0; i < types.Count; i++)
            {
                typeNames[i] = types[i].Name;
            }

            string currentTypeName = property.stringValue;
            int selectedIndex = Array.IndexOf(typeNames, currentTypeName);

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, typeNames);

            if (selectedIndex >= 0 && selectedIndex < types.Count)
            {
                property.stringValue = typeNames[selectedIndex];
            }
        }
    }
}