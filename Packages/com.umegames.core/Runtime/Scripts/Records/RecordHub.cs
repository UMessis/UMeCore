namespace UMeGames.Core.Records
{
    using System.IO;
    using UnityEngine;
    using static Logger.Logger;

    public static class RecordHub
    {
        private const string MANAGED_RECORDS_LOCATION = "Assets/Resources/Managed/ManagedRecords.asset";
        private const string MANAGED_RECORDS_DIRECTORY = "Assets/Resources/Managed";
        private const string MANAGED_RECORDS_RESOURCES_LOCATION = "Managed/ManagedRecords";

        private static ManagedRecords managed;

        public static void InitializeRecords()
        {
#if UNITY_EDITOR
            if (!File.Exists(MANAGED_RECORDS_LOCATION))
            {
                LogWarning(typeof(RecordHub), $"ManagedRecords asset was not found, creating it at {MANAGED_RECORDS_LOCATION}");
                if (!Directory.Exists(MANAGED_RECORDS_DIRECTORY))
                {
                    Directory.CreateDirectory(MANAGED_RECORDS_DIRECTORY);
                }
                ScriptableObject so = ScriptableObject.CreateInstance(typeof(ManagedRecords));
                UnityEditor.AssetDatabase.CreateAsset(so, MANAGED_RECORDS_LOCATION);
                ((ManagedRecords)so).UpdateManagedList();
            }
#endif

            managed = Resources.Load<ManagedRecords>(MANAGED_RECORDS_RESOURCES_LOCATION);
            if (managed == null)
            {
                LogError(typeof(RecordHub), "No Managed Records Asset!");
                return;
            }

            foreach (RootRecord rootRecord in managed.ManagedRootRecords)
            {
                rootRecord.Initialize();
            }
        }

        public static T GetRootRecord<T>() where T : RootRecord
        {
            foreach (RootRecord rootRecord in managed.ManagedRootRecords)
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