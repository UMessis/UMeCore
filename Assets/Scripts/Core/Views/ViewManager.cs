namespace UMeGames.Core.Views
{
    using System.Collections.Generic;
    using UMeGames.Core.Logger;
    using UMeGames.Core.Singleton;
    using UnityEngine;

    public class ViewManager : MonoSingleton<ViewManager>
    {
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
                        for (int i = viewHolder.childCount; i >= 0; i--)
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
            var mainCanvas = Resources.FindObjectsOfTypeAll<MainCanvas>();
            if (mainCanvas == null || mainCanvas.Length == 0)
            {
                this.LogError("No main canvas found in resources!");
                return;
            }
            if (mainCanvas.Length > 1)
            {
                this.LogError("More than 1 main canvas found in resources, there should only be 1!");
                return;
            }
            this.mainCanvas = Instantiate(mainCanvas[0], transform);
        }

        void GetAllViewsAndCreateCategories()
        {
            views = new((BaseView[])Resources.FindObjectsOfTypeAll(typeof(BaseView)));
            if (views == null || views.Count == 0)
            {
                this.LogWarning("No views found in the resources folder!");
                return;
            }

            views.Sort((x, y) => y.ViewPriority.CompareTo(x));

            foreach (var view in views)
            {
                if (!viewHolders.TryGetValue(view.ViewPriority, out var holder))
                {
                    viewHolders.Add(view.ViewPriority, Instantiate(
                        new GameObject($"Priority {view.ViewPriority}"),
                        mainCanvas.transform).GetComponent<Transform>()
                    );
                }
            }
        }
    }
}