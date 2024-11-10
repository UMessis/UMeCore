namespace UMeGames.Core.Records
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class RecordHub
    {
        private static readonly List<Record> records = new();

        public static void InitializeRecords()
        {
            foreach (RecordHolder recordHolder in Resources.LoadAll<RecordHolder>(""))
            {
                records.Add(recordHolder.Data);
            }
        }

        public static List<T> GetRecordsOfType<T>() where T : Record
        {
            List<T> recordsOfType = new();
            foreach (Record record in records)
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