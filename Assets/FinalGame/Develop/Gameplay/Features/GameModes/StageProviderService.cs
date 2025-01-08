using System;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.GameModes
{
    public class StageProviderService
    {
        private ReactiveVariable<int> _nextStageIndex = new();
        private readonly LevelConfig _levelConfig;

        public StageProviderService(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public IReadOnlyVariable<int> NextStageIndex => _nextStageIndex;

        public StageResults StageResult { get; private set; } = StageResults.Uncompleted;

        public int StageCount => _levelConfig.WaveConfigs.Count;

        public bool HasNextStage() => _nextStageIndex.Value < StageCount - 1;

        public void CompleteStage() => StageResult = StageResults.Completed;
        
        public void SwitchToNext()
        {
            if (HasNextStage() == false)
                throw new InvalidOperationException("No more stages available");

            _nextStageIndex.Value++;
            StageResult = StageResults.Uncompleted;
        }
    }
}