using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Pause
{
    public class TimeScalePauseService : IPauseService
    {
        public bool isPaused { get; private set; }
        
        public void Pause()
        {
            Time.timeScale = 0;
            isPaused = true;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}