namespace UMeGames.Core.Saves
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Records;
    using UnityEngine;
    using static Logger.Logger;
    
    public class SaveSystem
    {
        private const string MENU_PATH = "Tools/SaveSystem/";
        private const string SAVE_FOLDER_NAME = "SaveData";

        private static readonly Dictionary<Type, BaseSaveComponent> saveComponents = new();
        private static string saveFolderPath;
        private static SaveSystemRootRecord saveSystemData;
        private static Coroutine saveCoroutine;
        
        public static void Initialize()
        {
            saveSystemData = RecordHub.GetRootRecord<SaveSystemRootRecord>();
            saveFolderPath = Path.Combine(Application.persistentDataPath, SAVE_FOLDER_NAME);
            CreateSaveFolder();
            LoadSaveComponents();
        }

        public static T GetSaveComponent<T>() where T : BaseSaveComponent
        {
            return saveComponents[typeof(T)] as T;
        }

        private static void Save()
        {
            foreach ((Type _, BaseSaveComponent saveComponent) in saveComponents)
            {
                if (saveComponent.IsDirty)
                {
                    saveComponent.Save();
                }
            }
            
            Log($"[SaveSystem] {saveComponents.Count} components saved successfully");
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
            saveComponents.Clear();
            foreach (Type type in ReflectionUtils.GetAllTypesWithBaseClass<BaseSaveComponent>())
            {
                BaseSaveComponent saveComponentInstance = (BaseSaveComponent)Activator.CreateInstance(type);
                saveComponents.Add(type, saveComponentInstance);
                saveComponentInstance.Initialize(saveFolderPath);
            }

            Save();
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