namespace UMeGames
{
    using UMeGames.Core.Boot;
    using UMeGames.Core.Views;
    using UnityEngine;

    public class TestBoot : MonoBehaviour
    {
        [SerializeField] private Bootstrap bootstrap;

        void Awake()
        {
            bootstrap.OnInitializationComplete += OpenTestView;
        }

        void OnDestroy()
        {
            bootstrap.OnInitializationComplete -= OpenTestView;
        }

        void OpenTestView()
        {
            ViewManager.Instance.OpenView<TestView>();
        }
    }
}
