namespace UMeGames.Core.Attributes
{
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Linq;

    [CustomPropertyDrawer(typeof(AnimatorParameterAttribute))]
    public class AnimatorParameterAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AnimatorParameterAttribute animatorParameterAttribute = (AnimatorParameterAttribute)attribute;
            SerializedProperty animatorProperty = property.serializedObject.FindProperty(animatorParameterAttribute.AnimatorFieldName);

            if (animatorProperty is not { propertyType: SerializedPropertyType.ObjectReference } ||
                animatorProperty.objectReferenceValue == null || animatorProperty.objectReferenceValue is not Animator animator)
            {
                EditorGUI.HelpBox(position, $"Animator field '{animatorParameterAttribute.AnimatorFieldName}' not found or invalid.", MessageType.Error);
                return;
            }

            if (animator.runtimeAnimatorController is null)
            {
                EditorGUI.HelpBox(position, $"Animator does not have a controller.", MessageType.Error);
                return;
            }

            AnimatorControllerParameter[] parameters = animator.parameters;
            string[] parameterNames = parameters.Select(p => p.name).ToArray();
            int[] parameterHashes = parameters.Select(p => p.nameHash).ToArray();

            int currentIndex = Array.IndexOf(parameterHashes, property.intValue);
            int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, parameterNames);

            if (selectedIndex >= 0 && selectedIndex < parameterHashes.Length)
            {
                property.intValue = parameterHashes[selectedIndex];
            }
        }
    }
}