using Events;
using UnityEngine;

namespace Animations
{
    public class Anim_ReloadingScript : StateMachineBehaviour
    {
        private static readonly int Reloading = Animator.StringToHash("reloading");

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Reloading, false);
        }
    
    }
}
