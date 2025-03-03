namespace UMeGames.Core.Logger
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;

    public static class Logger
    {
        private const string MENU_PATH = "Tools/Logger/";
        
        private enum LogLevel
        {
            Normal,
            Warning,
            Error,
            Exception
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log<T>(this T instance, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Normal)) { return; }
#endif
            string instanceName = instance.GetType().Name;
            Debug.Log($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log(Type sourceType, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Normal)) { return; }
#endif
            string instanceName = sourceType.Name;
            Debug.Log($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogWarning<T>(this T instance, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Warning)) { return; }
#endif
            string instanceName = instance.GetType().Name;
            Debug.LogWarning($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogWarning(Type sourceType, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Warning)) { return; }
#endif
            string instanceName = sourceType.Name;
            Debug.LogWarning($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogError<T>(this T instance, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Error)) { return; }
#endif
            string instanceName = instance.GetType().Name;
            Debug.LogError($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogError(Type sourceType, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Error)) { return; }
#endif
            string instanceName = sourceType.Name;
            Debug.LogError($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogException<T>(this T instance, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Exception)) { return; }
#endif
            string instanceName = instance.GetType().Name;
            throw new Exception($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogException(Type sourceType, string message)
        {
#if UNITY_EDITOR
            if (!IsEnabled(LogLevel.Exception)) { return; }
#endif
            string instanceName = sourceType.Name;
            throw new Exception($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public static string GetColorHexFromString(string value)
        {
            MD5 hasher = MD5.Create();
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            int random = BitConverter.ToInt32(bytes, 0);
            Color color = Color.HSVToRGB(Math.Abs(random) % 256f / 256f, 1f, 0.75f);
            return ColorUtility.ToHtmlStringRGB(color);
        }
        
#if UNITY_EDITOR
        private static void SetLogLevel(LogLevel level, bool value)
        {
            UnityEditor.EditorPrefs.SetBool($"{MENU_PATH}{level}", value);
        }

        private static bool IsEnabled(LogLevel level)
        {
            return UnityEditor.EditorPrefs.GetBool($"{MENU_PATH}{level}", true);
        }
        
        [UnityEditor.MenuItem(MENU_PATH + "Normal")]
        private static void ToggleNormalLogLevel()
        {
            SetLogLevel(LogLevel.Normal, !IsEnabled(LogLevel.Normal));
        }
 
        [UnityEditor.MenuItem(MENU_PATH + "Normal", true)]
        private static bool ToggleNormalValidate()
        {
            UnityEditor.Menu.SetChecked(MENU_PATH + "Normal", IsEnabled(LogLevel.Normal));
            return true;
        }
        
        [UnityEditor.MenuItem(MENU_PATH + "Warning")]
        private static void ToggleWarningLogLevel()
        {
            SetLogLevel(LogLevel.Warning, !IsEnabled(LogLevel.Warning));
        }
 
        [UnityEditor.MenuItem(MENU_PATH + "Warning", true)]
        private static bool ToggleWarningValidate()
        {
            UnityEditor.Menu.SetChecked(MENU_PATH + "Warning", IsEnabled(LogLevel.Warning));
            return true;
        }
        
        [UnityEditor.MenuItem(MENU_PATH + "Error")]
        private static void ToggleErrorLogLevel()
        {
            SetLogLevel(LogLevel.Error, !IsEnabled(LogLevel.Error));
        }
 
        [UnityEditor.MenuItem(MENU_PATH + "Error", true)]
        private static bool ToggleErrorValidate()
        {
            UnityEditor.Menu.SetChecked(MENU_PATH + "Error", IsEnabled(LogLevel.Error));
            return true;
        }
        
        [UnityEditor.MenuItem(MENU_PATH + "Exception")]
        private static void ToggleExceptionLogLevel()
        {
            SetLogLevel(LogLevel.Exception, !IsEnabled(LogLevel.Exception));
        }
 
        [UnityEditor.MenuItem(MENU_PATH + "Exception", true)]
        private static bool ToggleExceptionValidate()
        {
            UnityEditor.Menu.SetChecked(MENU_PATH + "Exception", IsEnabled(LogLevel.Exception));
            return true;
        }
#endif
    }
}