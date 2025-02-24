namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Attributes;
    using UnityEngine;

    [Serializable, CreateAssetMenu(fileName = "ManagedRecords", menuName = "Records/Managed Records Asset")]
    public class ManagedRecords : ScriptableObject
    {
        [SerializeField, ReadOnly] private List<RootRecord> managedRootRecords = new();

        public List<RootRecord> ManagedRootRecords => managedRootRecords;

#if UNITY_EDITOR
        private void Reset()
        {
            if (Application.isPlaying) { return; }
            UpdateManagedList();
        }

        private void OnValidate()
        {
            if (Application.isPlaying) { return; }
            UpdateManagedList();
        }

        [Button]
        public void UpdateManagedList()
        {
            managedRootRecords = Resources.LoadAll<RootRecord>("").ToList();
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}