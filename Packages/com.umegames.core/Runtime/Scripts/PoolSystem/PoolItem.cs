namespace UMeGames.Core.Pool
{
    using UnityEngine;
    
    public abstract class PoolItem : MonoBehaviour
    {
        public void ReturnToPool()
        {
            PoolSystem.Instance.ReturnObject(this);
        }
        
        public abstract void OnPoolInstantiate();
    }
}