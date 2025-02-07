namespace UMeGames
{
    using Core.Saves;

    public class TestSaveComponent : SaveComponent<TestSaveComponentData>
    {
        protected override string FileName => "TestSave.ume";

        public float GetTestFloat()
        {
            return SaveData.TestFloat;
        }
    }
}