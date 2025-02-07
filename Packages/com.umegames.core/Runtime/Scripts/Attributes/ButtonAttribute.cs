namespace UMeGames.Core.Attributes
{
    using UnityEngine;
    
    public class ButtonAttribute : PropertyAttribute
    {
        public string MethodName { get; private set; }

        public ButtonAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}