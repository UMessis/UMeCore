namespace UMeGames.Core.Records
{
    using System.Collections.Generic;
    using UnityEngine;
    using static Logger.Logger;

    public static class RecordHub
    {
        private static readonly HashSet<RootRecord> rootRecords = new();

        public static void InitializeRecords()
        {
            foreach (RootRecord rootRecord in (RootRecord[])Resources.FindObjectsOfTypeAll(typeof(RootRecord)))
            {
                if (!rootRecords.Add(rootRecord))
                {
                    LogError($"Root record {rootRecord.name} already exists, cannot have more than one!");
                }
                
            }
        }

        public static T GetRootRecord<T>() where T : RootRecord
        {
            foreach (RootRecord rootRecord in rootRecords)
            {
                if (rootRecord is T record)
                {
                    return record;
                }
            }
            return null;
        }
    }
}