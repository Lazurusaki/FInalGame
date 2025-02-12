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
        Rigidbody,
        
        Health,
        MaxHealth,
        
        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,
        
        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
        
        AttackTrigger,
        AttackCondition,
        IsAttackProcess, 
        AttackCancelCondition,
        
        Damage,
        ShootPoint,
        InstantAttackEvent,
        InstantShootingDirections,
        
        AttackInterval,
        AttackCooldown,
        
        Team,
        
        DetectedEntitiesBuffer,
        
        IsMainHero,
        IsProjectile,
        Owner,
        
        BaseStats,
        ModifiedStats,
        StatsEffectsList,
        AbilityList,
        
        Experience,
        Level,
        
        DeathLayer,
        IsTouchDeathLayer,
        IsTouchAnotherTeam,
        
        BounceCount,
        BounceEvent,
        BounceLayer,
        
        Target,
        
        IsPickupable,
        IsPickupProcess,
        IsCollected,
        
        Coins,
        
        DropLootCondition,
        IsLootDropped,
        IsSpawningProcess,
        
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