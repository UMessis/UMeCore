#if UNITY_EDITOR
namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RootRecordHolder), true)]
    public class RootRecordEditor : Editor
    {
        private List<Type> subclasses;
        private string[] options;

        public override void OnInspectorGUI()
        {
            subclasses ??= ReflectionUtils.GetAllTypesWithBaseClass<RootRecord>();
            if (options == null)
            {
                options = new string[subclasses.Count];
                for (int i = 0; i < subclasses.Count; i++)
                {
                    options[i] = subclasses[i].Name;
                }
            }

            int selectedIndex = EditorGUILayout.Popup("Select Root Record", -1, options);

            if (selectedIndex >= 0 && selectedIndex < subclasses.Count)
            {
                string path = AssetDatabase.GetAssetPath((RootRecordHolder)serializedObject.targetObject);
                ScriptableObject so = CreateInstance(subclasses[selectedIndex]);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.CreateAsset(so, path);
                Selection.objects = new UnityEngine.Object[] { so };
            }
        }
    }
}
#endif