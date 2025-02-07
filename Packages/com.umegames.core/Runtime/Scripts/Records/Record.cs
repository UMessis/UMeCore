namespace UMeGames.Core.Records
{
    using System;
    using Attributes;
    using UnityEngine;

    [Serializable]
    public class Record : ScriptableObject
    {
        [SerializeField, ReadOnly] private int version = 1;

        private Guid id = Guid.NewGuid();
        public Guid Id => id;
        public int Version => version;

        [Button]
        public void IncrementVersion()
        {
            version++;
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
        }
    }
}