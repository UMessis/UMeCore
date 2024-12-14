namespace UMeGames.Core.Records
{
    using System;
    using UnityEngine;

    [Serializable, CreateAssetMenu(fileName = "Record", menuName = "Records/NewRecord")]
    public class RecordHolder : ScriptableObject
    {
        [SerializeReference] private Record data;

        public Record Data => data;

        public void Register(Record record)
        {
            data = record;
        }
    }
}