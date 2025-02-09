namespace UMeGames.Core.Pool
{
    using System;
    using System.Collections.Generic;
    using Logger;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class LoadedContextInfo
    {
        private readonly List<AsyncOperationHandle<GameObject>> loadedHandles = new();
        private readonly Dictionary<Type, List<PoolItem>> pooledObjects = new();
        private readonly PoolContext poolContext;
        private int assetsLoading;
        private bool isLoaded;

        public bool IsLoaded => isLoaded;
        public bool IsDoneLoading => assetsLoading == 0;
        
        public LoadedContextInfo(PoolContext poolContext)
        {
            this.poolContext = poolContext;
        }

        public bool IsTypeInContext(Type type)
        {
            return pooledObjects.ContainsKey(type);
        }
        
        public bool IsTypeInContext<T>()
        {
            return pooledObjects.ContainsKey(typeof(T));
        }
        
        public T GetObjectOfType<T>() where T : PoolItem
        {
            if (!pooledObjects.TryGetValue(typeof(T), out List<PoolItem> list))
            {
                this.LogError($"No objects of type {typeof(T)} are loaded");
                return null;
            }

            if (list.Count == 0)
            {
                AddSingle<T>();
            }
            
            PoolItem item = list[0];
            list.RemoveAt(0);
            item.gameObject.transform.SetParent(null);
            item.gameObject.SetActive(true);
            item.OnPoolInstantiate();
            return item as T;
        }

        public void ReturnObject(PoolItem poolItem)
        {
            if (!pooledObjects.TryGetValue(poolItem.GetType(), out List<PoolItem> list))
            {
                this.LogError($"No objects of type {poolItem.GetType()} are loaded");
                return;
            }
            
            poolItem.transform.SetParent(PoolSystem.Instance.transform);
            poolItem.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            poolItem.gameObject.SetActive(false);
            list.Add(poolItem);
        }

        public void Load()
        {
            foreach (PoolEntry entry in poolContext.PoolEntries)
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(entry.PoolItem);
                handle.Completed += (x) => OnAssetLoaded(x, entry.Amount);
                assetsLoading++;
            }
        }

        public void Unload()
        {
            if (isLoaded == false) { return; }
            
            foreach (KeyValuePair<Type, List<PoolItem>> kvp in pooledObjects)
            {
                foreach (PoolItem item in kvp.Value)
                {
                    PoolSystem.DestroyPoolObject(item.gameObject);
                }
                kvp.Value.Clear();
            }
            pooledObjects.Clear();

            foreach (AsyncOperationHandle<GameObject> handle in loadedHandles)
            {
                handle.Release();
            }
            loadedHandles.Clear();
            
            assetsLoading = 0;
            isLoaded = false;
            this.Log($"Unloaded pool context {poolContext.PoolContextName}");
        }

        private void AddSingle<T>() where T : PoolItem
        {
            foreach (AsyncOperationHandle<GameObject> handle in loadedHandles)
            {
                if (handle.Result.GetComponent<PoolItem>().GetType() == typeof(T))
                {
                    InstantiateItem(handle.Result);
                    return;
                }
            }
        }

        private void OnAssetLoaded(AsyncOperationHandle<GameObject> handle, int amount)
        {
            assetsLoading--;
            loadedHandles.Add(handle);

            for (int i = 0; i < amount; i++)
            {
                InstantiateItem(handle.Result);
            }

            if (IsDoneLoading)
            {
                isLoaded = true;
                this.Log($"Loaded pool context {poolContext.PoolContextName}");
                PoolSystem.Instance.OnContextLoaded?.Invoke(poolContext.PoolContextName);
            }
        }

        private void InstantiateItem(GameObject prefab)
        {
            GameObject go = PoolSystem.Instance.InstantiatePoolObject(prefab);
            PoolItem item = go.GetComponent<PoolItem>();
            go.SetActive(false);
                
            if (!pooledObjects.ContainsKey(item.GetType()))
            {
                pooledObjects.Add(item.GetType(), new());
            }
            pooledObjects[item.GetType()].Add(item);
        }
    }
}