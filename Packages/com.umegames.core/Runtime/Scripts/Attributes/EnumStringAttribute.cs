namespace UMeGames.Core.Attributes
{
    using System;
    using Logger;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field)]
    public class EnumStringAttribute : PropertyAttribute
    {
        public Type EnumType { get; }

        public EnumStringAttribute(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                this.LogError("Provided type is not an enum");
                return;
            }
            EnumType = enumType;
        }
    }
}