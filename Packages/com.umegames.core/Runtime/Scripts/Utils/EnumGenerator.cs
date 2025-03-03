namespace UMeGames.Core.Utils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Logger;

    public static class EnumGenerator
    {
        public static void GenerateEnum(List<string> names, string directory, string enumName)
        {
#if UNITY_EDITOR
            StringBuilder enumCode = new();
            enumCode.AppendLine("// This file is auto generated, do not change manually");
            enumCode.AppendLine("public enum " + enumName);
            enumCode.AppendLine("{");

            foreach (string name in names)
            {
                enumCode.AppendLine("    " + name + ",");
            }

            enumCode.AppendLine("}");

            if (!Directory.Exists(directory))
            {
                Logger.Log(typeof(EnumGenerator), $"Directory {directory} does not exist, creating it");
                Directory.CreateDirectory(directory);
            }

            string filePath = Path.Combine(directory, enumName + ".cs");
            File.WriteAllText(filePath, enumCode.ToString());
            Logger.Log(typeof(EnumGenerator), $"Enum {enumName} generated with {names.Count} entries");

            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}