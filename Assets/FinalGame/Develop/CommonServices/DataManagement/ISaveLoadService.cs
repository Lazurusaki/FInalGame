namespace FinalGame.Develop.CommonServices.DataManagement
{
    public interface ISaveLoadService
    {
        bool TryLoad<TData>(out TData data) where TData : ISaveData;
        void Save<TData>(TData data) where TData : ISaveData;
    }
}
