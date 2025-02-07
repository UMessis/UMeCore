namespace UMeGames.Core.Records
{
    using System;
    using UnityEngine;

    [Serializable, CreateAssetMenu(fileName = "Record", menuName = "Records/SubRecord")]
    public class SubRecordHolder : ScriptableObject
    {
        [SerializeReference] private SubRecord data;

        public SubRecord Data => data;

        public void Register(SubRecord subRecord)
        {
            data = subRecord;
        }
    }
}