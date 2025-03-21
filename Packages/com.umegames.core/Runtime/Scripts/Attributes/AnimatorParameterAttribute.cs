namespace UMeGames.Core.Attributes
{
    using UnityEngine;

    public class AnimatorParameterAttribute : PropertyAttribute
    {
        public string AnimatorFieldName { get; private set; }

        public AnimatorParameterAttribute(string animatorFieldName)
        {
            AnimatorFieldName = animatorFieldName;
        }
    }
}