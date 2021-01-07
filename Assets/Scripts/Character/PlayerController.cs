using UI;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        public CrossHairFollowMouse CrossHairComponent => CrossHairFollowMouse;
        [SerializeField] private CrossHairFollowMouse CrossHairFollowMouse;
        // Start is called before the first frame update
    }
}
