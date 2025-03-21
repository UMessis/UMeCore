namespace UMeGames.Core.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Attributes;
    using Logger;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [Serializable]
    public abstract class RootRecordCollection : RootRecord
    {
        [SerializeField, TypeDropdown(typeof(SubRecord))] private List<string> subRecordTypes = new();
        [SerializeField, ReadOnly] private List<SubRecord> subRecords = new();

        private readonly Dictionary<string, List<SubRecord>> subRecordCollection = new();

        protected List<T> GetAllSubRecordsOfType<T>() where T : SubRecord
        {
            this.Log($"Getting {typeof(T).Name} from collection");
            return subRecordCollection[typeof(T).Name] as List<T>;
        }

#if UNITY_EDITOR
        private void Reset()
        {
            UpdateCollectionList();
        }

        private void OnValidate()
        {
            UpdateCollectionList();
        }

        private void UpdateCollectionList()
        {
            List<Object> resources = Resources.LoadAll("").ToList();
            if (resources.Count == 0) { return; }

            subRecords.Clear();
            subRecordCollection.Clear();

            foreach (string typeName in subRecordTypes)
            {
                List<Object> typedResources = resources.FindAll(x => x.GetType().Name == typeName);

                if (subRecordCollection.ContainsKey(typeName))
                {
                    return;
                }

                if (typedResources.Count > 0)
                {
                    this.Log($"Adding {typeName} to collection");
                    subRecordCollection.Add(typeName, new List<SubRecord>());
                }

                foreach (Object resource in typedResources)
                {
                    subRecords.Add((SubRecord)resource);
                    subRecordCollection[typeName].Add((SubRecord)resource);
                }
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}