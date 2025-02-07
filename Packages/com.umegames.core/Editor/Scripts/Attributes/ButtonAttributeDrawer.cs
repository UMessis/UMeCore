namespace UMeGames.Core.Attributes
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ButtonAttribute buttonAttribute = (ButtonAttribute)attribute;
            Object target = property.serializedObject.targetObject;

            if (GUI.Button(position, buttonAttribute.MethodName))
            {
                Type buttonType = target.GetType();
                MethodInfo method = buttonType.GetMethod(buttonAttribute.MethodName, BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

                if (method == null)
                {
                    while (buttonType.BaseType != null)
                    {
                        buttonType = buttonType.BaseType;
                        method = buttonType.GetMethod(buttonAttribute.MethodName, BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                        if (method != null)
                        {
                            method.Invoke(target, null);
                            return;
                        }
                    }
                    Debug.LogWarning($"Method '{buttonAttribute.MethodName}' not found");
                }
                else
                {
                    method.Invoke(target, null);
                }
            }
        }
    }
}