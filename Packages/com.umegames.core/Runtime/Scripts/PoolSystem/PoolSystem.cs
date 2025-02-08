namespace UMeGames.Core.Pool
{
    using System.Collections.Generic;
    using Logger;
    using Records;
    using Singleton;
    using UnityEngine;

    public class PoolSystem : MonoSingleton<PoolSystem>
    {
        private readonly Dictionary<string, LoadedContextInfo> poolContexts = new();
        private PoolRootRecord poolData;
        
        public void Initialize()
        {
            poolData = RecordHub.GetRootRecord<PoolRootRecord>();
            foreach (PoolContext poolContext in poolData.PoolContexts)
            {
                poolContexts.Add(poolContext.PoolContextName, new(poolContext));
            }
        }
        
        public GameObject GetObjectOfType<T>(string poolContext) where T : PoolItem
        {
            return GetLoadedContextInfo(poolContext).GetObjectOfType<T>();
        }

        public void ReturnObject(GameObject gameObject, string poolContext)
        {
            GetLoadedContextInfo(poolContext).ReturnObject(gameObject);
        }

        private LoadedContextInfo GetLoadedContextInfo(string poolContext)
        {
            if (!poolContexts.TryGetValue(poolContext, out LoadedContextInfo loadedContextInfo))
            {
                this.LogError($"Context {poolContext} not found");
                return null;
            }

            if (!loadedContextInfo.IsLoaded)
            {
                this.LogError($"Context {poolContext} not loaded");
                return null;
            }
            
            return loadedContextInfo;
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
        
        public void DestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }
    }
}