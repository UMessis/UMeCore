namespace UMeGames.Core.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using UMeGames.Core.Logger;
    using UMeGames.Core.Singleton;
    using UnityEngine;

    public class ViewManager : MonoSingleton<ViewManager>
    {
        const string MAIN_CANVAS_PATH = "Core/Views/MainCanvas";

        List<BaseView> views;
        MainCanvas mainCanvas;
        Dictionary<int, Transform> viewHolders = new();

        public void Initialize()
        {
            CreateMainCanvas();
            GetAllViewsAndCreateCategories();
        }

        public T OpenView<T>() where T : BaseView
        {
            foreach (var view in views)
            {
                if (view.GetType() == typeof(T))
                {
                    var viewHolder = viewHolders[view.ViewPriority];
                    if (viewHolder.childCount > 0)
                    {
                        for (int i = viewHolder.childCount; i >= 0; i--)
                        {
                            var presentView = viewHolder.GetChild(i).GetComponent<BaseView>();
                            presentView.OnViewClosed();
                            Destroy(presentView.gameObject);
                        }
                    }
                    return Instantiate(view, viewHolder) as T;
                }
            }
            return default;
        }

        public void CloseView<T>() where T : BaseView
        {
            foreach (var view in views)
            {
                if (view.GetType() == typeof(T))
                {
                    var viewHolder = viewHolders[view.ViewPriority];
                    if (viewHolder.childCount > 0)
                    {
                        for (int i = viewHolder.childCount - 1; i >= 0; i--)
                        {
                            var presentView = viewHolder.GetChild(i).GetComponent<BaseView>();
                            if (presentView.GetType() == typeof(T))
                            {
                                presentView.OnViewClosed();
                                Destroy(presentView.gameObject);
                                return;
                            }
                        }
                    }
                }
            }
        }

        void CreateMainCanvas()
        {
            mainCanvas = Instantiate(Resources.Load<MainCanvas>(MAIN_CANVAS_PATH), transform);
            mainCanvas.Rect.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        void GetAllViewsAndCreateCategories()
        {
            foreach (var type in ReflectionUtils.GetAllTypesWithBaseClass<BaseView>())
            {
                // todo : optimize
                views = Resources.LoadAll("", typeof(BaseView)).Cast<BaseView>().ToList();
            }

            if (views == null || views.Count == 0)
            {
                this.LogWarning("No views found in the resources folder!");
                return;
            }

            views.Sort((x, y) => y.ViewPriority.CompareTo(x.ViewPriority));

            foreach (var view in views)
            {
                if (!viewHolders.TryGetValue(view.ViewPriority, out var holder))
                {
                    var go = new GameObject($"Priority {view.ViewPriority}");
                    go.transform.SetParent(mainCanvas.transform);
                    var rect = go.AddComponent<RectTransform>();
                    rect.localScale = new(1, 1, 1);
                    rect.sizeDelta = mainCanvas.Rect.sizeDelta;
                    rect.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    viewHolders.Add(view.ViewPriority, go.transform);
                }
            }
        }
    }
}