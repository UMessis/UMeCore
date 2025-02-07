namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(SubRecordHolder), true)]
    public class SubRecordEditor : Editor
    {
        private List<Type> subclasses;
        private string[] options;

        public override void OnInspectorGUI()
        {
            subclasses ??= ReflectionUtils.GetAllTypesWithBaseClass<SubRecord>();
            if (options == null)
            {
                options = new string[subclasses.Count];
                for (int i = 0; i < subclasses.Count; i++)
                {
                    options[i] = subclasses[i].Name;
                }
            }

            int selectedIndex = EditorGUILayout.Popup("Select Sub Record", -1, options);

            if (selectedIndex >= 0 && selectedIndex < subclasses.Count)
            {
                string path = AssetDatabase.GetAssetPath((SubRecordHolder)serializedObject.targetObject);
                ScriptableObject so = CreateInstance(subclasses[selectedIndex]);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.CreateAsset(so, path);
                Selection.objects = new UnityEngine.Object[] { so };
            }
        }
    }
}