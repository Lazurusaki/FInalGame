using System.Collections.Generic;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public interface IStatsEffect
    {
        void ApplyTo(Dictionary<StatTypes, float> stats);
    }
}