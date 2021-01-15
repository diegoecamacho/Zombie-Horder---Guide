using Enemies;

namespace State_Machine
{
    public class ZombieState : State
    {
        protected ZombieComponent Zombie;
    
        protected ZombieState(ZombieComponent zombie, StateMachine stateMachine) : base(stateMachine)
        {
            Zombie = zombie;
        }
    }
}
