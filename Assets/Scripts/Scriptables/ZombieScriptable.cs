using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Zombie", menuName = "ScriptableObjects/Enemies", order = 1)]
    public class ZombieScriptable : ScriptableObject
    {

       public GameObject ZombiePrefab;
       public string Name;
       public int Health;
    }
}
