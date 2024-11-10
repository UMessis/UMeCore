namespace UMeGames.Core.Logger
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Unity.VisualScripting;
    using UnityEngine;

    public static class Logger
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log<T>(this T instance, string message)
        {
            string instanceName = instance.GetType().Name;
            Debug.Log($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogWarning<T>(this T instance, string message)
        {
            string instanceName = instance.GetType().Name;
            Debug.LogWarning($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogError<T>(this T instance, string message)
        {
            string instanceName = instance.GetType().Name;
            Debug.LogError($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        public static string GetColorHexFromString(string value)
        {
            MD5 hasher = MD5.Create();
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            int random = BitConverter.ToInt32(bytes, 0);
            Color color = Color.HSVToRGB(Math.Abs(random) % 256f / 256f, 1f, 0.75f);
            return color.ToHexString();
        }
    }
}