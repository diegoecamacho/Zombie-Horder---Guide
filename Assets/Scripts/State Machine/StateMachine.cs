using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }
        protected Dictionary<string, State> States;
        private bool Running = false;

        private void Awake()
        {
            States = new Dictionary<string, State>();
        }

        public void AddState(string stateName, State state)
        {
            States.Add(stateName, state);
        }
    
        public void RemoveState(string stateName)
        {
            States.Remove(stateName);
        }
    
        // Start is called before the first frame update
        public void Initialize(string startingState)
        {
            if (States.ContainsKey(startingState))
            {
                ChangeState(startingState);
            }
            else
            {
                ChangeState("Idle");
            }
        }

        // Update is called once per frame
        public void ChangeState(string nextState)
        {
            if (Running)
            {
                StopRunningState();
            }
            
            if (!States.ContainsKey(nextState)) return;
        
            CurrentState = States[nextState];
            CurrentState.Enter();

            if (CurrentState.UpdateInterval > 0)
            {
                InvokeRepeating(nameof(IntervalUpdate), 0f ,CurrentState.UpdateInterval);
            }
            Running = true;
        }

        private void StopRunningState()
        {
            Running = false;
            CurrentState.Exit();
            CancelInvoke(nameof(IntervalUpdate));
        }

        public void IntervalUpdate()
        {
            CurrentState.IntervalUpdate();
        }

        public void Update()
        {
            if (Running)
            {
                CurrentState.Update();
            }
            
        }
    
        public void FixedUpdate()
        {
            if (Running)
            {
                CurrentState.FixedUpdate();
            }
            
        }
    }
}
