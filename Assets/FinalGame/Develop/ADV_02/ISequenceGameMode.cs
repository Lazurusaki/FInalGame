using System;

namespace FinalGame.Develop.ADV_02
{
    public interface ISequenceGameMode
    {
        public event Action Success;
        public event Action Fail;

        public string GetSequence();
        public void ValidateSequence(string sequence);

        public void Start();
    }
}