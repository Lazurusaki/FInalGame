using System.Collections.Generic;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;

namespace FinalGame.Develop.CommonServices.LevelsService
{
    public class CompletedLevelsService: IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private List<int> _completedLevels = new();

        public CompletedLevelsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public bool IsLevelCompleteted(int levelNumber) => _completedLevels.Contains(levelNumber);

        public bool TryAddLevelCompleted(int levelNumber)
        {
            if (IsLevelCompleteted(levelNumber))
                return false;

            _completedLevels.Add(levelNumber);
                return true;
        }

        public void ReadFrom(PlayerData data)
        {
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }

        public void WriteTo(PlayerData data)
        {
            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(_completedLevels);
        }
    }
}
