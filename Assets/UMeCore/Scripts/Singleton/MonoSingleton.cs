namespace UMeGames.Core.Singleton
{
    using UnityEngine;

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    new GameObject(typeof(T).Name).AddComponent<T>();
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this as T;
            DontDestroyOnLoad(this);
        }
    }
}