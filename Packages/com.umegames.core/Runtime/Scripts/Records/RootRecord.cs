namespace UMeGames.Core.Records
{
    using System;
    using Attributes;
    using UnityEngine;

    [Serializable]
    public class RootRecord : ScriptableObject
    {
        // TODO : Make versioning work
        [SerializeField, ReadOnly] private int version = 1;
        
        public int Version => version;

#if UNITY_EDITOR
        [Button]
        public void IncrementVersion()
        {
            version++;
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}