using UnityEngine;


namespace FinalGame.Develop.Gameplay.Features.Input
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }
        Vector3 Direction { get; }
    }
}