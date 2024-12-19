using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.Sensors
{
    public class SelfTriggerReceiverRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private TriggerReceiver _triggerReceiver;
        
        public override void Register(Entity entity)
        {
            entity.AddSelfTriggerReceiver(_triggerReceiver);
        }
    }
}