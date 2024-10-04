namespace UMeGames.Core.Logger
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Unity.VisualScripting;

    public static class Logger
    {
        public static void Log<T>(this T instance, string message)
        {
            var instanceName = instance.GetType().Name;
            UnityEngine.Debug.Log($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public static void LogWarning<T>(this T instance, string message)
        {
            var instanceName = instance.GetType().Name;
            UnityEngine.Debug.LogWarning($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public static void LogError<T>(this T instance, string message)
        {
            var instanceName = instance.GetType().Name;
            UnityEngine.Debug.LogError($"<color=#{GetColorHexFromString(instanceName)}>[{instanceName}]</color> {message}");
        }

        public static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public static string GetColorHexFromString(string value)
        {
            var hasher = MD5.Create();
            var bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            var random = BitConverter.ToInt32(bytes, 0);
            var color = UnityEngine.Color.HSVToRGB(Math.Abs(random) % 256f / 256f, 1f, 0.75f);
            return color.ToHexString();
        }
    }
}