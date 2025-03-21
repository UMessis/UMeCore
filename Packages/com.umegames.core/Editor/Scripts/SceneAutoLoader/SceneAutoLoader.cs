#if UNITY_EDITOR
namespace UMeGames.Core.Editor
{
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    [InitializeOnLoad]
    public static class SceneAutoLoader
    {
        private const string EDITOR_MENU_OPTIONS_PATH = "Tools/SceneAutoLoader/";
        private const string EDITOR_PREF_LOAD_MASTER_ON_PLAY = "SceneAutoLoader.LoadMasterOnPlay";
        private const string EDITOR_PREF_MASTER_SCENE = "SceneAutoLoader.MasterScene";
        private const string EDITOR_PREF_PREVIOUS_SCENE = "SceneAutoLoader.PreviousScene";
        
        private static bool LoadMasterOnPlay
        {
            get => EditorPrefs.GetBool(EDITOR_PREF_LOAD_MASTER_ON_PLAY, false);
            set => EditorPrefs.SetBool(EDITOR_PREF_LOAD_MASTER_ON_PLAY, value);
        }

        private static string MasterScene
        {
            get => EditorPrefs.GetString(EDITOR_PREF_MASTER_SCENE, "Master.unity");
            set => EditorPrefs.SetString(EDITOR_PREF_MASTER_SCENE, value);
        }

        private static string PreviousScene
        {
            get => EditorPrefs.GetString(EDITOR_PREF_PREVIOUS_SCENE, EditorSceneManager.GetActiveScene().path);
            set => EditorPrefs.SetString(EDITOR_PREF_PREVIOUS_SCENE, value);
        }
        
        static SceneAutoLoader()
        {
            EditorApplication.playmodeStateChanged += OnPlayModeChanged;
        }

        [MenuItem(EDITOR_MENU_OPTIONS_PATH + "Select Master Scene...")]
        private static void SelectMasterScene()
        {
            string masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
            if (!string.IsNullOrEmpty(masterScene))
            {
                MasterScene = masterScene;
                LoadMasterOnPlay = true;
            }
        }

        [MenuItem(EDITOR_MENU_OPTIONS_PATH + "Load Master On Play", true)]
        private static bool ShowLoadMasterOnPlay()
        {
            return !LoadMasterOnPlay;
        }

        [MenuItem(EDITOR_MENU_OPTIONS_PATH + "Load Master On Play")]
        private static void EnableLoadMasterOnPlay()
        {
            LoadMasterOnPlay = true;
        }

        [MenuItem(EDITOR_MENU_OPTIONS_PATH + "Don't Load Master On Play", true)]
        private static bool ShowDontLoadMasterOnPlay()
        {
            return LoadMasterOnPlay;
        }

        [MenuItem(EDITOR_MENU_OPTIONS_PATH + "Don't Load Master On Play")]
        private static void DisableLoadMasterOnPlay()
        {
            LoadMasterOnPlay = false;
        }

        private static void OnPlayModeChanged()
        {
            if (!LoadMasterOnPlay)
            {
                return;
            }

            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                PreviousScene = EditorSceneManager.GetActiveScene().path;
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(MasterScene);
                }
                else
                {
                    EditorApplication.isPlaying = false;
                }
            }
            if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorSceneManager.OpenScene(PreviousScene);
            }
        }
    }
}
#endif