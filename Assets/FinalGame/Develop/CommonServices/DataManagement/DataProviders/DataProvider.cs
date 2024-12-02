using System;
using System.Collections.Generic;

namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    public abstract class DataProvider <TData> where TData : ISaveData
    {
        private readonly ISaveLoadService _saveLoadService;

        private List<IDataWriter<TData>> _writers = new();
        private List<IDataReader<TData>> _readers = new();

        public DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
    
        private TData Data { get; set; }

        public void RegisterWriter(IDataWriter<TData> writer)
        {
            if (_writers.Contains(writer))
                throw new ArgumentException($"Writer {nameof(writer)}already exists ");
            
            _writers.Add(writer);
        }
        
        public void RegisterReader(IDataReader<TData> reader)
        {
            if (_readers.Contains(reader))
                throw new ArgumentException($"Reader {nameof(reader)}already exists ");
            
            _readers.Add(reader);
        }

        public void Load()
        {
            if (_saveLoadService.TryLoad(out TData data))
                Data = data;
            else
                Reset();

            foreach (IDataReader<TData> reader in _readers)
                reader.ReadFrom(Data);
        }
        
        public void Save()
        {
            foreach (IDataWriter<TData> writer in _writers)
                writer.WriteTo(Data);
            
            _saveLoadService.Save(Data);
        }
        
        protected abstract TData GetOriginData();
        
        private void Reset()
        {
            Data = GetOriginData();
            Save();
        }
        
        
    }
}
