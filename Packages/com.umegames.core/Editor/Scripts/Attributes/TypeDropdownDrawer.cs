#if UNITY_EDITOR
namespace UMeGames.Core.Attributes
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
    public class TypeDropdownDrawer : PropertyDrawer
    {
        List<Type> types;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TypeDropdownAttribute typeDropdown = attribute as TypeDropdownAttribute;
            Type baseType = typeDropdown.baseType;
            if (types == null)
            {
                types = ReflectionUtils.GetAllTypesWithBaseClass(baseType);
            }

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
#endif