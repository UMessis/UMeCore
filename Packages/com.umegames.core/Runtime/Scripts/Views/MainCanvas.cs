namespace UMeGames.Core.Views
{
    using UnityEngine;

    public class MainCanvas : MonoBehaviour
    {
        private RectTransform rect;

        public RectTransform Rect => rect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }
    }
}