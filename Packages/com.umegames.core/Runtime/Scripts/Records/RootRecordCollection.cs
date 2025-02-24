namespace UMeGames.Core.Records
{
    using System.Collections.Generic;
    using Attributes;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public abstract class RootRecordCollection : RootRecord
    {
        [SerializeField, TypeDropdown(typeof(SubRecord))] private List<string> subRecordTypes = new();
        [SerializeField, ReadOnly] private List<SubRecord> subRecordCollection = new();

        private readonly List<SubRecord> tempSubRecords = new();

        public List<SubRecord> GetAllSubRecords()
        {
            return subRecordCollection;
        }

        public List<SubRecord> GetAllSubRecordsOfType<T>() where T : SubRecord
        {
            List<SubRecord> subRecords = new();
            foreach (SubRecord record in subRecordCollection)
            {
                if (record is T)
                {
                    subRecords.Add(record);
                }
            }
            return subRecords;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (Application.isPlaying) { return; }
            UpdateCollectionList();
        }

        private void OnValidate()
        {
            if (Application.isPlaying) { return; }
            UpdateCollectionList();
        }

        [Button]
        public void UpdateCollectionList()
        {
            tempSubRecords.Clear();
            foreach (string typeName in subRecordTypes)
            {
                Object[] resources = Resources.LoadAll("");
                if (resources == null || resources.Length == 0) { return; }
                foreach (Object resource in resources)
                {
                    if (resource.GetType().Name == typeName)
                    {
                        tempSubRecords.Add((SubRecord)resource);
                    }
                }
            }

            subRecordCollection = tempSubRecords;

            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}