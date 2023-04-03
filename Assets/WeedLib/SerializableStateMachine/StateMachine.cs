using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;
using UniRx;

namespace WeedLib.SerializableStateMachine
{
    [Serializable]
    public class StateMachine<StateTypeEnum> where StateTypeEnum : Enum
    {
        protected Dictionary<StateTypeEnum, StateBase> states = new Dictionary<StateTypeEnum, StateBase>();
        public IReadOnlyDictionary<StateTypeEnum, StateBase> States => states;

        public void AddState(StateTypeEnum stateType, StateBase state)
        {
            states[stateType] = state;
        }

        [SerializeField] protected ReactiveProperty<StateTypeEnum> currentStateTypeReactiveProperty = new ReactiveProperty<StateTypeEnum>();
        public StateTypeEnum CurrentStateType
        {
            get => currentStateTypeReactiveProperty.Value;
            private set => currentStateTypeReactiveProperty.Value = value;
        }
        public IObservable<StateTypeEnum> OnCurrentStateTypeChanged => currentStateTypeReactiveProperty;

        public StateBase CurrentState
        {
            get
            {
                if(states.Count > 0 && !states.ContainsKey(CurrentStateType)) Debug.LogWarning($"{nameof(CurrentStateType)} is invalid. value = {CurrentStateType}");
                return states.ContainsKey(CurrentStateType) ? states[CurrentStateType] : null;
            }
        }

        protected StateMachine()
        {
            
        }

        public StateMachine(StateTypeEnum initialStateType)
        {
            CurrentStateType = initialStateType;
        }

        public void Update(float deltaTime)
        {
            CurrentState?.OnUpdate(deltaTime);
        }

        public void ChangeState(StateTypeEnum stateType)
        {
            CurrentState?.OnExit();

            CurrentStateType = stateType;

            CurrentState?.OnEnter();
        }

        public void InvokeOnEnter()
        {
            CurrentState?.OnEnter();
        }

        public void ChangeStateCalm(StateTypeEnum stateType)
        {
            CurrentStateType = stateType;
        }
    }

    [Serializable]
    public class StateMachine<StateTypeEnum, TriggerTypeEnum> : StateMachine<StateTypeEnum> where StateTypeEnum : Enum where TriggerTypeEnum : Enum
    {
        protected Dictionary<StateTypeEnum, List<Transition<StateTypeEnum, TriggerTypeEnum>> > transitionListDic = new Dictionary<StateTypeEnum, List<Transition<StateTypeEnum, TriggerTypeEnum>>>();

        protected StateMachine()
        {
            
        }

        public StateMachine(StateTypeEnum initialState) : base(initialState)
        {

        }

        public void ExecuteTrigger(TriggerTypeEnum triggerType)
        {
            List<Transition<StateTypeEnum, TriggerTypeEnum>> transitions = transitionListDic[CurrentStateType];
            foreach(Transition<StateTypeEnum, TriggerTypeEnum> transition in transitions)
            {
                if (transition.TriggerType.Equals(triggerType))
                {
                    ChangeState(transition.ToStateType);
                    break;
                }
            }
        }
    
        public void AddTransition(StateTypeEnum fromStateType, StateTypeEnum toStateType, TriggerTypeEnum triggerType)
        {
            if (!transitionListDic.ContainsKey(fromStateType))
            {
                transitionListDic.Add(fromStateType, new List<Transition<StateTypeEnum, TriggerTypeEnum>>());
            }

            List<Transition<StateTypeEnum, TriggerTypeEnum>> transitions = transitionListDic[fromStateType];

            Transition<StateTypeEnum, TriggerTypeEnum> transition = transitions.FirstOrDefault(x => x.ToStateType.Equals(toStateType));
            if (transition == null)
            {
                transitions.Add(new Transition<StateTypeEnum, TriggerTypeEnum>(toStateType, triggerType));
            }
            else
            {
                transition.ToStateType = toStateType;
                transition.TriggerType = triggerType;
            }
        }        
    }
}