namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using UnityEditor;

    [CustomEditor(typeof(RecordHolder), true)]
    public class RecordEditor : Editor
    {
        List<Type> subclasses;
        string[] options;

        public override void OnInspectorGUI()
        {
            RecordHolder recordHolder = (RecordHolder)serializedObject.targetObject;

            subclasses ??= ReflectionUtils.GetAllTypesWithBaseClass<Record>();
            if (options == null)
            {
                options = new string[subclasses.Count];
                for (var i = 0; i < subclasses.Count; i++)
                {
                    options[i] = subclasses[i].Name;
                }
            }

            var selectedIndex = recordHolder.Data != null ? subclasses.IndexOf(recordHolder.Data.GetType()) : -1;
            selectedIndex = EditorGUILayout.Popup("Selected Record", selectedIndex, options);

            if (selectedIndex >= 0 && selectedIndex < subclasses.Count && (recordHolder.Data == null || recordHolder.Data.GetType() != subclasses[selectedIndex]))
            {
                recordHolder.Register((Record)Activator.CreateInstance(subclasses[selectedIndex]));
            }

            if (recordHolder.Data != null)
            {
                SerializedObject serializedSubclass = new(recordHolder);
                serializedSubclass.Update();

                SerializedProperty property = serializedSubclass.FindProperty("data");
                while (property.NextVisible(true))
                {
                    // todo : this does not allow the nameing of values to Size or Element X
                    if (property.displayName != "Size" && !Regex.IsMatch(property.displayName, @"Element [\d-]"))
                    {
                        EditorGUILayout.PropertyField(property, true);
                    }
                }

                serializedSubclass.ApplyModifiedProperties();
            }
        }
    }
}