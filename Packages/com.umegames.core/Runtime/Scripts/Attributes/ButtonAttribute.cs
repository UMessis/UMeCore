namespace UMeGames.Core.Attributes
{
    using System;
    using UnityEngine;
    
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string Label { get; private set; }

        public ButtonAttribute(string label = null)
        {
            Label = label;
        }
    }
}