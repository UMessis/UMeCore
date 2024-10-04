namespace UMeGames.Game.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UMeGames.Core.Services;

    public class SampleDependantService : IService
    {
        public List<Type> Dependencies => new()
        {
            typeof(SampleService)
        };

        public IEnumerator Initialize()
        {
            yield break;
        }

        public void Dispose()
        {
        }
    }
}