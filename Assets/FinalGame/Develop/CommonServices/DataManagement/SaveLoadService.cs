namespace FinalGame.Develop.CommonServices.DataManagement
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataRepository _repository;

        public SaveLoadService(IDataRepository repository, IDataSerializer serializer)
        {
            _serializer = serializer;
            _repository = repository;
        }
        
        public void Save<TData>(TData data) where TData : ISaveData
        {
            var serializeData = _serializer.Serialize(data);
            _repository.Write(SaveDataKeys.GetKeyFor<TData>(), serializeData);    
        }
        
        public bool TryLoad<TData>(out TData data) where TData : ISaveData
        {
            var key = SaveDataKeys.GetKeyFor<TData>();

            if (_repository.Exists(key) == false)
            {
                data = default(TData);
                return false;
            }

            var serializedData = _repository.Read(key);
            data = _serializer.Deserialize<TData>(serializedData);
            return true;
        }

    }
}
