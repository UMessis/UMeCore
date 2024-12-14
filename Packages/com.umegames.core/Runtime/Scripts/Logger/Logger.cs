namespace UMeGames.Core.Logger
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;

    public static class Logger
    {
        public static void Log<T>(this T instance, string message)
        {
            var instanceName = instance.GetType().Name;
            Debug.Log($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogWarning<T>(this T instance, string message)
        {
            var instanceName = instance.GetType().Name;
            Debug.LogWarning($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void LogError<T>(this T instance, string message)
        {
            var instanceName = instance.GetType().Name;
            Debug.LogError($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        public static string GetColorHexFromString(string value)
        {
            var hasher = MD5.Create();
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            var random = BitConverter.ToInt32(bytes, 0);
            var color = Color.HSVToRGB(Math.Abs(random) % 256f / 256f, 1f, 0.75f);
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}