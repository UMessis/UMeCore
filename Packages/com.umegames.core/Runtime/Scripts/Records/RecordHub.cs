namespace UMeGames.Core.Records
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class RecordHub
    {
        private static readonly List<Record> records = new();

        public static void InitializeRecords()
        {
            foreach (Record record in (Record[])Resources.FindObjectsOfTypeAll(typeof(Record)))
            {
                records.Add(record);
            }
        }

        public static List<T> GetRecordsOfType<T>() where T : Record
        {
            return records.FindAll(x => x is T) as List<T>;
        }
    }
}