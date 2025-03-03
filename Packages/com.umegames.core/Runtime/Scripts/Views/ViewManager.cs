namespace UMeGames.Core.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Attributes;
    using UMeGames.Core.Logger;
    using UMeGames.Core.Singleton;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class ViewManager : MonoSingleton<ViewManager>
    {
        private class LoadedViewInfo
        {
            public AssetReference AssetReference { get; set; }
            public BaseView View { get; set; }
            public ViewLoadingType LoadingType { get; set; }
            public Action<BaseView> OnLoaded { get; set; }
        }

        [Serializable]
        private class ViewReference
        {
            [SerializeField, TypeDropdown(typeof(BaseView))] private string viewType;
            [SerializeField] private ViewLoadingType viewLoadingType;
            [SerializeField] private int viewPriority;
            [SerializeField] private AssetReference viewAssetReference;

            public string ViewType => viewType;
            public ViewLoadingType ViewLoadingType => viewLoadingType;
            public int ViewPriority => viewPriority;
            public AssetReference ViewAssetReference => viewAssetReference;
        }

        [SerializeField] private List<ViewReference> viewReferences = new();

        private readonly List<LoadedViewInfo> loadedViews = new();

        public void OpenView(Type type, Action<BaseView> onViewLoaded = null)
        {
            foreach (ViewReference reference in viewReferences)
            {
                if (reference.ViewType == type.Name)
                {
                    AsyncOperationHandle<GameObject> handle = reference.ViewAssetReference.LoadAssetAsync<GameObject>();
                    LoadedViewInfo info = new()
                    {
                        AssetReference = reference.ViewAssetReference,
                        LoadingType = reference.ViewLoadingType,
                        OnLoaded = onViewLoaded,
                    };
                    handle.Completed += (x) => OnViewLoaded(x, info);
                    return;
                }
            }
        }

        public void OpenView<T>(Action<BaseView> onViewLoaded = null) where T : BaseView
        {
            foreach (ViewReference reference in viewReferences)
            {
                if (reference.ViewType == typeof(T).Name)
                {
                    AsyncOperationHandle<GameObject> handle = reference.ViewAssetReference.LoadAssetAsync<GameObject>();
                    LoadedViewInfo info = new()
                    {
                        AssetReference = reference.ViewAssetReference,
                        LoadingType = reference.ViewLoadingType,
                        OnLoaded = onViewLoaded,
                    };
                    handle.Completed += (x) => OnViewLoaded(x, info);
                    return;
                }
            }
        }

        private void OnViewLoaded(AsyncOperationHandle<GameObject> handle, LoadedViewInfo info)
        {
            if (info.LoadingType == ViewLoadingType.Mono)
            {
                foreach (LoadedViewInfo loadedView in loadedViews)
                {
                    Destroy(loadedView.View);
                    loadedView.AssetReference.ReleaseAsset();
                }
                loadedViews.Clear();
            }

            BaseView view = handle.Result.GetComponent<BaseView>();
            info.View = Instantiate(view, transform);
            loadedViews.Add(info);
            info.View.OnViewOpened();
            info.OnLoaded?.Invoke(info.View);
        }

        public void CloseView<T>() where T : BaseView
        {
            foreach (LoadedViewInfo view in loadedViews)
            {
                if (view.View.GetType() == typeof(T))
                {
                    loadedViews.Remove(view);
                    Destroy(view.View);
                    view.AssetReference.ReleaseAsset();
                    return;
                }
            }
        }
    }
}