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
            UpdateManagedList();
        }

        private void OnValidate()
        {
            UpdateManagedList();
        }

        [Button]
        public void UpdateManagedList()
        {
            managedRootRecords = Resources.LoadAll<RootRecord>("").ToList();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}