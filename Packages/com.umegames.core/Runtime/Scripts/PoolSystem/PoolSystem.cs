namespace UMeGames.Core.Pool
{
    using System;
    using System.Collections.Generic;
    using Logger;
    using Records;
    using Singleton;
    using UnityEngine;

    public class PoolSystem : MonoSingleton<PoolSystem>
    {
        private readonly Dictionary<string, LoadedContextInfo> poolContexts = new();
        private PoolRootRecord poolData;

        public Action<string> OnContextLoaded;
        
        public void Initialize()
        {
            poolData = RecordHub.GetRootRecord<PoolRootRecord>();
            foreach (PoolContext poolContext in poolData.PoolContexts)
            {
                poolContexts.Add(poolContext.PoolContextName, new(poolContext));
            }
        }
        
        public T GetObjectOfType<T>() where T : PoolItem
        {
            LoadedContextInfo context = GetContextInfoForType<T>();
            if (context == null)
            {
                this.LogError($"Failed to get object of type {typeof(T)}, context must not be loaded");
                return null;
            }
            return context.GetObjectOfType<T>();
        }

        public void ReturnObject(PoolItem poolItem)
        {
            GetContextInfoForObject(poolItem)?.ReturnObject(poolItem);
        }
        
        public void LoadContext(string contextName, bool unloadAll = false)
        {
            if (unloadAll) { UnloadAllContexts(); }
            
            if (!poolContexts.TryGetValue(contextName, out LoadedContextInfo poolContextInfo))
            {
                this.LogError($"Failed to get pool context info for context: {contextName}");
                return;
            }
            poolContextInfo.Load();
        }

        public void UnloadAllContexts()
        {
            foreach (LoadedContextInfo context in poolContexts.Values)
            {
                context.Unload();
            }
        }
        
        public void UnloadContext(string contextName)
        {
            if (!poolContexts.TryGetValue(contextName, out LoadedContextInfo poolContextInfo))
            {
                this.LogError($"Failed to get pool context info for context: {contextName}");
                return;
            }
            
            poolContextInfo.Unload();
        }

        public GameObject InstantiatePoolObject(GameObject prefab)
        {
            return Instantiate(prefab, transform, false);
        }
        
        public static void DestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }
        
        private LoadedContextInfo GetContextInfoForType<T>() where T : PoolItem
        {
            foreach (LoadedContextInfo context in poolContexts.Values)
            {
                if (context.IsTypeInContext<T>())
                {
                    return context;
                }
            }
            return null;
        }
        
        private LoadedContextInfo GetContextInfoForObject(PoolItem item)
        {
            foreach (LoadedContextInfo context in poolContexts.Values)
            {
                if (context.IsTypeInContext(item.GetType()))
                {
                    return context;
                }
            }
            return null;
        }
    }
}