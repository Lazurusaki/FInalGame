namespace FinalGame.Develop.Gameplay.Entities
{
    public enum EntityValues
    {
        MoveDirection,
        MoveSpeed,
        MoveCondition,
        IsMoving,
        
        RotationDirection,
        RotationSpeed,
        RotationCondition,
        
        SelfTriggerReceiver,
        SelfTriggerDamage,
        
        CharacterController,
        Transform,
        
        Health,
        MaxHealth,
        
        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,
        
        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
        
        //ADV_03
        Energy,
        MaxEnergy,
        SpendEnergyEvent,
        SpendEnergyRequest,
        SpendEnergyCondition,
        RestoreEnergyEvent,
        RestoreEnergyCooldown,
        RestoreEnergyCondition,
        
        RadiusAttackDamage,
        RadiusAttackRadius,
        RadiusAttackTrigger,
        RadiusAttackCondition,
        
        TeleportRadius,
        TeleportEnergyCost,
        TeleportCondition,
        TeleportTrigger,
        TeleportStartEvent,
        TeleportEndEvent,
        
        RadialAttackTeleportTrigger
    }
}