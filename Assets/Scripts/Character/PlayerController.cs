using Interfaces;
using UI;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour, IPlayer
    {
        public CrossHairFollowMouse CrossHairComponent => CrossHairFollowMouse;
        [SerializeField] private CrossHairFollowMouse CrossHairFollowMouse;
        
        public bool IsFiring = false;
        
        public bool IsReloading = false;
        
        public bool IsJumping = false;
        
        public bool IsRunning = false;
    }
}
