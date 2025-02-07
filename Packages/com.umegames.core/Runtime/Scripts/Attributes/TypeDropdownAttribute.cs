namespace UMeGames.Core.Attributes
{
    using System;
    using UnityEngine;

    public class TypeDropdownAttribute : PropertyAttribute
    {
        public Type baseType;

        public TypeDropdownAttribute(Type baseType)
        {
            this.baseType = baseType;
        }
    }
}