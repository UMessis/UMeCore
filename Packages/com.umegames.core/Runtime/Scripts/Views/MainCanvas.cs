namespace UMeGames.Core.Views
{
    using UnityEngine;

    public class MainCanvas : MonoBehaviour
    {
        private RectTransform rect;

        public RectTransform Rect => rect;

        void Awake()
        {
            rect = GetComponent<RectTransform>();
        }
    }
}