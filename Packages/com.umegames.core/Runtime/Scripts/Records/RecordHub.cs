namespace UMeGames.Core.Records
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class RecordHub
    {
        static List<Record> records = new();

        public static void InitializeRecords()
        {
            foreach (Record record in (Record[])Resources.FindObjectsOfTypeAll(typeof(Record)))
            {
                records.Add(record);
            }
        }

        public static List<T> GetRecordsOfType<T>() where T : Record
        {
            var recordsOfType = new List<T>();
            foreach (var record in records)
            {
                if (record.GetType() == typeof(T))
                {
                    recordsOfType.Add(record as T);
                }
            }
            return recordsOfType;
        }
    }
}