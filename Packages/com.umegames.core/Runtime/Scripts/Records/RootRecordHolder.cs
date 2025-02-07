namespace UMeGames.Core.Records
{
    using System;
    using UnityEngine;

    [Serializable, CreateAssetMenu(fileName = "Record", menuName = "Records/RootRecord")]
    public class RootRecordHolder : ScriptableObject
    {
        [SerializeReference] private RootRecord data;

        public RootRecord Data => data;

        public void Register(RootRecord rootRecord)
        {
            data = rootRecord;
        }
    }
}