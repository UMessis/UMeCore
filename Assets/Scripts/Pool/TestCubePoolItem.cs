namespace UMeGames
{
    using Core.Logger;
    using Core.Pool;

    public class TestCubePoolItem : PoolItem
    {
        public override void OnPoolInstantiate()
        {
            this.Log("Test cube instantiated");
            ReturnToPool();
        }
    }
}