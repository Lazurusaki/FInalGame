using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Timer;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.AI.States;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Utils.Conditions;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI
{
    public class AIFactory
    {
        private readonly DIContainer _container;
        private readonly TimerServiceFactory _timerServiceFactory;

        public AIStateMachine CreateMainHeroBehavior(Entity entity, ITargetSelector targetSelector)
        {
            //AIStateMachine autoAimAttackState = CreateAutoAimAttackStateMachine(entity, targetSelector);

            var autoAimAttackState = new AutoAimAttackState(
                entity.GetRotationDirection(),
                targetSelector,
                entity.GetTransform(),
                entity.GetDetectedEntitiesBuffer(),
                entity.GetAttackTrigger(),
                entity.GetAttackCondition());
            
            var moveDirection = entity.GetMoveDirection();
            var rotationDirection = entity.GetRotationDirection();
            var inputService = _container.Resolve<IInputService>();

            var playerInputState = new PlayerInputState(inputService, moveDirection, rotationDirection);

            ICompositeCondition fromPlayerInputStateToAutoAimAttackState =
                new CompositeCondition(LogicOperations.AndOperation)
                    .Add(new FuncCondition(() => targetSelector.TrySelectTarget(entity.GetDetectedEntitiesBuffer(), out Entity selectedTarget)))
                    .Add(new FuncCondition(() => inputService.Direction == Vector3.zero));
            
            ICompositeCondition fromAutoAimAttackStateToPlayerInputState =
                new CompositeCondition(LogicOperations.OrOperation)
                    .Add(new FuncCondition(() => targetSelector.TrySelectTarget(entity.GetDetectedEntitiesBuffer(), out Entity selectedTarget) == false))
                    .Add(new FuncCondition(() => inputService.Direction != Vector3.zero));
            
            var rootStateMachine = new AIStateMachine();
            rootStateMachine.AddState(playerInputState);
            rootStateMachine.AddState(autoAimAttackState);
            
            rootStateMachine.AddTransition(playerInputState, autoAimAttackState , fromPlayerInputStateToAutoAimAttackState );
            rootStateMachine.AddTransition(autoAimAttackState, playerInputState , fromAutoAimAttackStateToPlayerInputState );
            
            return rootStateMachine;
        }
        
        public AIFactory(DIContainer container)
        {
            _container = container;
            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
        }

        public AIStateMachine CreateGhostBehavior(Entity entity)
            => CreateWanderingStateMachine(entity);

        private AIStateMachine CreateWanderingStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            var moveDirection = entity.GetMoveDirection();
            var rotationDirection = entity.GetRotationDirection();

            Wandering wanderingState = new Wandering(moveDirection, rotationDirection, 3f);

            Idle idleState = new Idle();

            TimerService wanderingTimer = _timerServiceFactory.Create(9);
            disposables.Add(wanderingState.Entered.Subscribe(wanderingTimer.Restart));
            FuncCondition wanderingTimerEndedCondition = new FuncCondition(() => wanderingTimer.IsOver);

            TimerService idleTimer = _timerServiceFactory.Create(3);
            disposables.Add(idleState.Entered.Subscribe(idleTimer.Restart));
            FuncCondition idleTimerEndedCondition = new FuncCondition(() => idleTimer.IsOver);

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(wanderingState);
            stateMachine.AddState(idleState);

            stateMachine.AddTransition(wanderingState, idleState, wanderingTimerEndedCondition);
            stateMachine.AddTransition(idleState, wanderingState, idleTimerEndedCondition);
            
            return stateMachine;
        }
        
        private AIStateMachine CreateAutoAimAttackStateMachine(Entity entity, ITargetSelector targetSelector)
        {
            var rotationDirection = entity.GetRotationDirection();
            var transform = entity.GetTransform();

            var rotateToTargetState = new RotateToTargetState(
                rotationDirection,
                targetSelector,
                transform,
                entity.GetDetectedEntitiesBuffer());

            var attackTrigger = entity.GetAttackTrigger();
            var attackState = new AttackState(attackTrigger);
            
            ICompositeCondition fromRotateToTargetStateToAttackStateCondition =
                new CompositeCondition(LogicOperations.AndOperation)
                    .Add(entity.GetAttackCondition())
                    .Add(new FuncCondition(() =>
                    {
                        if (targetSelector.TrySelectTarget(entity.GetDetectedEntitiesBuffer(),
                                out Entity selectedTarget))
                            return Quaternion.Angle(transform.rotation,
                                       Quaternion.LookRotation(selectedTarget.GetTransform().position - transform.position)) < 1f;

                        return false;
                    }));
            
            ICompositeCondition fromAttackStateToRotateToTargetStateCondition =
                new CompositeCondition(LogicOperations.AndOperation)
                    .Add(new FuncCondition(() => entity.GetIsAttackProcess().Value == false));

            var autoAimAttackState = new AIStateMachine();
            
            autoAimAttackState.AddState(rotateToTargetState);
            autoAimAttackState.AddState(attackState);
            
            autoAimAttackState.AddTransition(rotateToTargetState, attackState, fromRotateToTargetStateToAttackStateCondition);
            autoAimAttackState.AddTransition(attackState, rotateToTargetState, fromAttackStateToRotateToTargetStateCondition);

            return autoAimAttackState;
        }
    }
}