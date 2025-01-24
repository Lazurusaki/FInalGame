using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class InstantShootingDirectionsArgs
    {
        private List<InstantShotDirectionArgs> _args;

        public InstantShootingDirectionsArgs(params InstantShotDirectionArgs[] args)
        {
            _args = new List<InstantShotDirectionArgs>(args);
        }

        public IReadOnlyList<InstantShotDirectionArgs> Args => _args;

        public void Add(InstantShotDirectionArgs shotDirectionArgs)
        {
            var arg = _args.FirstOrDefault(arg => arg.Angle == shotDirectionArgs.Angle);

            if (arg is not null)
            {
                arg.ProjectilesCount += shotDirectionArgs.ProjectilesCount;
                return;
            }
            
            _args.Add(shotDirectionArgs);
        }

        public void Remove(InstantShotDirectionArgs shotDirectionArgs)
        {
            var arg = _args.FirstOrDefault(arg => arg.Angle == shotDirectionArgs.Angle);

            if (arg is not null)
            {
                arg.ProjectilesCount -= shotDirectionArgs.ProjectilesCount;

                if (arg.ProjectilesCount <= 0)
                    _args.Remove(shotDirectionArgs);
            }
        }
    }

    public class InstantShotDirectionArgs
    {
        private int _angle;
        private int _projectilesCount;

        public InstantShotDirectionArgs(int angle, int projectilesCount)
        {
            _angle = angle;
            _projectilesCount = projectilesCount;
        }

        public int Angle => _angle;

        public int ProjectilesCount
        {
            get => _projectilesCount;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _projectilesCount = value;
            }
        }
    }
}