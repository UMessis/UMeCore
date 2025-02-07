namespace UMeGames.Core.Saves
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Records;
    using UnityEngine;
    
    public static class SaveSystem
    {
        private const string MENU_PATH = "Tools/SaveSystem/";
        private const string SAVE_FOLDER_NAME = "SaveData";
        
        private static readonly HashSet<BaseSaveComponent> saveComponents = new();
        private static string saveFolderPath;
        private static SaveSystemRootRecord saveSystemData;
        
        public static void Initialize()
        {
            saveSystemData = RecordHub.GetRootRecord<SaveSystemRootRecord>();
            saveFolderPath = Path.Combine(Application.persistentDataPath, SAVE_FOLDER_NAME);
            CreateSaveFolder();
            LoadSaveComponents();
        }

        public static T GetSaveComponent<T>() where T : BaseSaveComponent
        {
            foreach (BaseSaveComponent saveComponent in saveComponents)
            {
                if (saveComponent is T component)
                {
                    return component;
                }
            }
            return null;
        }

        public static void Save()
        {
            foreach (BaseSaveComponent saveComponent in saveComponents)
            {
                if (saveComponent.IsDirty)
                {
                    saveComponent.Save();
                }
            }
        }

        private static void CreateSaveFolder()
        {
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }
        }

        private static void LoadSaveComponents()
        {
            foreach (Type type in ReflectionUtils.GetAllTypesWithBaseClass<BaseSaveComponent>())
            {
                BaseSaveComponent saveComponentInstance = (BaseSaveComponent)Activator.CreateInstance(type);
                saveComponents.Add(saveComponentInstance);
                saveComponentInstance.Initialize(saveFolderPath);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem(MENU_PATH + "Clear Save")]
        private static void ClearSave()
        {
            string path = Path.Combine(Application.persistentDataPath, SAVE_FOLDER_NAME);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
        
        [UnityEditor.MenuItem(MENU_PATH + "Open Save Folder")]
        private static void OpenSaveFolder()
        {
            string path = Path.Combine(Application.persistentDataPath, SAVE_FOLDER_NAME);
            UnityEditor.EditorUtility.RevealInFinder(Directory.Exists(path) ? path : Application.persistentDataPath);
        }
#endif
    }
}