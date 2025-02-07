namespace UMeGames
{
    using Core.Saves;

    public class TestSaveComponent : SaveComponent<TestSaveComponentData>
    {
        protected override string FileName => "TestSave.ume";

        public void SetTestFloat(float value)
        {
            SaveData.testFloat = value;
            IsDirty = true;
        }
        
        public float GetTestFloat()
        {
            return SaveData.testFloat;
        }
    }
}