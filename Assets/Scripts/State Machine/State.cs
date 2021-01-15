using System.Collections;

namespace State_Machine
{
    public abstract class State
    {
        protected StateMachine StateMachine;
        public float UpdateInterval { get; protected set; } = 1f;
    
        // Start is called before the first frame update
        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        // Update is called once per frame
        public virtual void Enter()
        {
            
        }

        public virtual void IntervalUpdate()
        {
        
        }
        
        public virtual void Update()
        {
        
        }
    
        public virtual void FixedUpdate()
        {
        
        }
    
        // Update is called once per frame
        public virtual void Exit()
        {
        
        }
    }
}
