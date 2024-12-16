using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities
{
    public abstract class MonoEntityRegistrator : MonoBehaviour
    {
    public abstract void Register(Entity entity);
    }
}