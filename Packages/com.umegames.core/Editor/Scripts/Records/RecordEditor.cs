namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;

    [CustomEditor(typeof(RecordHolder), true)]
    public class RecordEditor : Editor
    {
        List<Type> subclasses;
        string[] options;

        public override void OnInspectorGUI()
        {
            subclasses ??= ReflectionUtils.GetAllTypesWithBaseClass<Record>();
            if (options == null)
            {
                options = new string[subclasses.Count];
                for (var i = 0; i < subclasses.Count; i++)
                {
                    options[i] = subclasses[i].Name;
                }
            }

            var selectedIndex = EditorGUILayout.Popup("Select Record Type", -1, options);

            if (selectedIndex >= 0 && selectedIndex < subclasses.Count)
            {
                var path = AssetDatabase.GetAssetPath((RecordHolder)serializedObject.targetObject);
                var so = CreateInstance(subclasses[selectedIndex]);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.CreateAsset(so, path);
                Selection.objects = new UnityEngine.Object[] { so };
            }
        }
    }
}