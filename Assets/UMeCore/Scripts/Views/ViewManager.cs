namespace UMeGames.Core.Views
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UMeGames.Core.Logger;
    using UMeGames.Core.Singleton;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class ViewManager : MonoSingleton<ViewManager>
    {
        private const string MAIN_CANVAS_PATH = "MainCanvas";

        private List<BaseView> views;
        private MainCanvas mainCanvas;
        private readonly Dictionary<int, Transform> viewHolders = new();
        private readonly Dictionary<int, List<BaseView>> activeViews = new();

        public IEnumerator Initialize()
        {
            AsyncOperationHandle<GameObject> mainCanvasLoad = Addressables.InstantiateAsync(MAIN_CANVAS_PATH, transform);
            yield return mainCanvasLoad;

            if (mainCanvasLoad.Status == AsyncOperationStatus.Failed)
            {
                this.LogError("Failed to load main canvas");
                yield break;
            }

            mainCanvas = mainCanvasLoad.Result.GetComponent<MainCanvas>();
            GetAllViewsAndCreateCategories();
        }

        public T OpenView<T>() where T : BaseView
        {
            foreach (BaseView view in views)
            {
                if (view.GetType() != typeof(T))
                {
                    continue;
                }

                BaseView instantiatedView = Instantiate(view, viewHolders[view.ViewPriority]);

                if (activeViews[view.ViewPriority].Count == 0)
                {
                    activeViews[view.ViewPriority].Add(instantiatedView);
                    return instantiatedView as T;
                }

                foreach (BaseView activeView in activeViews[view.ViewPriority])
                {
                    activeViews[view.ViewPriority].Remove(activeView);
                    activeView.OnViewClosed();
                    Destroy(activeView.gameObject);
                }

                activeViews[view.ViewPriority].Add(instantiatedView);
                return instantiatedView as T;
            }

            this.LogError("Failed to add view, specified type is not present");
            return default(T);
        }

        public void CloseView<T>() where T : BaseView
        {
            foreach (BaseView view in views)
            {
                if (view.GetType() != typeof(T))
                {
                    continue;
                }

                if (activeViews[view.ViewPriority].Count == 0)
                {
                    return;
                }

                foreach (BaseView activeView in activeViews[view.ViewPriority])
                {
                    if (activeView.GetType() != typeof(T))
                    {
                        continue;
                    }

                    activeViews[view.ViewPriority].Remove(activeView);
                    activeView.OnViewClosed();
                    Destroy(activeView.gameObject);
                }
            }
        }

        private void GetAllViewsAndCreateCategories()
        {
            views = Resources.LoadAll("", typeof(BaseView)).Cast<BaseView>().ToList();

            if (views == null || views.Count == 0)
            {
                this.LogWarning("No views found in the resources folder!");
                return;
            }

            views.Sort((x, y) => y.ViewPriority.CompareTo(x));

            foreach (BaseView view in views)
            {
                if (!viewHolders.TryGetValue(view.ViewPriority, out Transform _))
                {
                    // todo : creating and destroying a gameobject here is weird
                    GameObject layer = new($"Priority {view.ViewPriority}");
                    viewHolders.Add(view.ViewPriority, Instantiate(layer, mainCanvas.transform).transform);
                    Destroy(layer);
                }

                if (!activeViews.TryGetValue(view.ViewPriority, out List<BaseView> _))
                {
                    activeViews.Add(view.ViewPriority, new List<BaseView>());
                }
            }
        }
    }
}