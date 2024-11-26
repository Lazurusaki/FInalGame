namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    public abstract class DataProvider <TData> where TData : ISaveData
    {
        private readonly ISaveLoadService _saveLoadService;

        public DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
    
        private TData Data { get; set; }

        public void Load()
        {
            if (_saveLoadService.TryLoad(out TData data))
                Data = data;
            else
                Reset();
        }
        
        protected abstract TData GetOriginData();
        
        private void Reset()
        {
            Data = GetOriginData();
            Save();
        }
        
        private void Save()
        {
            _saveLoadService.Save(Data);
        }
    }
}
