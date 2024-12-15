namespace UMeGames.Core.Records
{
    using System;
    using Unity.Collections;
    using UnityEngine;

    [Serializable]
    public class Record : ScriptableObject
    {
        [Header("Base")] // todo : versioning
        [SerializeField] private Guid id;
        [SerializeField, ReadOnly] private int version = 1;

        public Guid Id => id;
        public int Version => version;

        public Record()
        {
            id = Guid.NewGuid();
        }
    }
}