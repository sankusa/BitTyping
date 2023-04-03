using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;

namespace WeedLib.SerializableStateMachine
{
    [Serializable]
    public abstract class StateBase
    {
        public virtual void OnEnter(){}
        public virtual void OnUpdate(float deltaTime){}
        public virtual void OnExit(){}
    }

    // サブステート付き
    public abstract class StateBase<StateTypeEnum> : StateBase where StateTypeEnum : Enum
    {
        [SerializeField] protected StateMachine<StateTypeEnum> subStateMachine;
        public StateMachine<StateTypeEnum> SubStateMachine => subStateMachine;

        private bool useDefaultStateType = false;
        private StateTypeEnum defaultStateType;

        protected StateBase()
        {
            
        }

        public StateBase(StateTypeEnum initialStateType)
        {
            subStateMachine = new StateMachine<StateTypeEnum>(initialStateType);
        }

        public void SetDefaultStateType(StateTypeEnum defaultStateType)
        {
            useDefaultStateType = true;
            this.defaultStateType = defaultStateType;
        }

        public void ClearDefaultStateType()
        {
            useDefaultStateType = false;
            defaultStateType = default(StateTypeEnum);
        }

        public override void OnEnter()
        {
            if(useDefaultStateType) subStateMachine.ChangeStateCalm(defaultStateType);
            subStateMachine.CurrentState.OnEnter();
        }

        public override void OnUpdate(float deltaTime)
        {
            subStateMachine.CurrentState.OnUpdate(deltaTime);
        }

        public override void OnExit()
        {
            subStateMachine.CurrentState.OnExit();
            if(useDefaultStateType) subStateMachine.ChangeStateCalm(defaultStateType);
        }
    }

    // サブステート&トリガー付き
    public abstract class StateBase<StateTypeEnum, TriggerTypeEnum> : StateBase where StateTypeEnum : Enum where TriggerTypeEnum : Enum
    {
        // サブステート
        [SerializeField] protected StateMachine<StateTypeEnum, TriggerTypeEnum> subStateMachine;
        public StateMachine<StateTypeEnum, TriggerTypeEnum> SubStateMachine => subStateMachine;

        private bool useDefaultStateType = false;
        private StateTypeEnum defaultStateType;

        protected StateBase()
        {
            
        }

        public StateBase(StateTypeEnum initialStateType)
        {
            subStateMachine = new StateMachine<StateTypeEnum, TriggerTypeEnum>(initialStateType);
        }

        public void SetDefaultStateType(StateTypeEnum defaultStateType)
        {
            useDefaultStateType = true;
            this.defaultStateType = defaultStateType;
        }

        public void ClearDefaultStateType()
        {
            useDefaultStateType = false;
            defaultStateType = default(StateTypeEnum);
        }

        public override void OnEnter()
        {
            if(useDefaultStateType) subStateMachine.ChangeStateCalm(defaultStateType);
            subStateMachine.CurrentState.OnEnter();
        }

        public override void OnUpdate(float deltaTime)
        {
            subStateMachine.CurrentState.OnUpdate(deltaTime);
        }

        public override void OnExit()
        {
            subStateMachine.CurrentState.OnExit();
            if(useDefaultStateType) subStateMachine.ChangeStateCalm(defaultStateType);
        }
    }
}