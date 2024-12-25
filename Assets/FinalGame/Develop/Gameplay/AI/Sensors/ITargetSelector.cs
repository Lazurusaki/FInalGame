using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;

namespace FinalGame.Develop.Gameplay.AI.Sensors
{
    public interface ITargetSelector
    {
        bool TrySelectTarget(IEnumerable<Entity> targets, out Entity selectedTarget);
    }
}