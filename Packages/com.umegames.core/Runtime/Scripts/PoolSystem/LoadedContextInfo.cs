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
        private readonly Dictionary<Type, List<GameObject>> pooledObjects = new();
        private readonly PoolContext poolContext;
        private int assetsLoading;
        private bool isLoaded;

        public bool IsLoaded => isLoaded;
        public bool IsDoneLoading => assetsLoading == 0;
        
        public LoadedContextInfo(PoolContext poolContext)
        {
            this.poolContext = poolContext;
        }

        public GameObject GetObjectOfType<T>() where T : PoolItem
        {
            if (!pooledObjects.TryGetValue(typeof(T), out List<GameObject> list))
            {
                this.LogError($"No objects of type {typeof(T)} are loaded");
                return null;
            }

            if (list.Count == 0)
            {
                AddSingle<T>();
            }
            
            GameObject obj = list[0];
            list.RemoveAt(0);
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            Type type = obj.GetComponent<PoolItem>().GetType();
            
            if (!pooledObjects.TryGetValue(type, out List<GameObject> list))
            {
                this.LogError($"No objects of type {type} are loaded");
                return;
            }
            
            obj.transform.SetParent(PoolSystem.Instance.transform);
            obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            list.Add(obj);
        }

        public void Load()
        {
            isLoaded = true;
            
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
            
            foreach (KeyValuePair<Type, List<GameObject>> kvp in pooledObjects)
            {
                foreach (GameObject obj in kvp.Value)
                {
                    PoolSystem.Instance.DestroyPoolObject(obj);
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
                this.Log($"Loaded pool context {poolContext.PoolContextName}");
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
            pooledObjects[item.GetType()].Add(go);
        }
    }
}