namespace UMeGames.Core.Pool
{
    using UnityEngine;
    
    public abstract class PoolItem : MonoBehaviour
    {
        public void ReturnToPool()
        {
            // todo
            // PoolSystem.Instance.ReturnObject(gameObject);
        }
    }
}