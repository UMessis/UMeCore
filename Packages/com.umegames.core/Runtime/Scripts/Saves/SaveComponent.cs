namespace UMeGames.Core.Saves
{
    using System;
    using System.IO;
    using UnityEngine;

    public abstract class SaveComponent<T> : BaseSaveComponent where T : SaveComponentData
    {
        private string savePath;
        
        protected abstract string FileName { get; }
        protected T SaveData;
        
        public override void Initialize(string saveFolderPath)
        {
            savePath = Path.Combine(saveFolderPath, FileName);
            LoadData();
        }

        public override void Save()
        {
            string json = JsonUtility.ToJson(SaveData);
            FileStream stream = File.Open(savePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter writer = new(stream);
            writer.Write(json);
            stream.Close();
            IsDirty = false;
        }

        private void LoadData()
        {
            if (File.Exists(savePath))
            {
                SaveData = JsonUtility.FromJson<T>(File.ReadAllText(savePath));
                return;
            }
            
            SaveData = Activator.CreateInstance<T>();
            FileStream stream = File.Create(savePath);
            stream.Close();
        }
    }
}