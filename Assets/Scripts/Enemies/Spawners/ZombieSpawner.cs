using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Spawners
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private int ZombiesToSpawn = 5;

        [SerializeField] private GameObject[] ZombieEnemies;
        [SerializeField] private SpawnerTrigger[] TriggerZones;

        private GameObject PlayerGameObject;

        // Start is called before the first frame update
        private void Start()
        {
            
            PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
            
            for (int i = 0; i < ZombiesToSpawn; i++)
            {
                SpawnZombie();
            }
        }

        private void SpawnZombie()
        {
            GameObject spawnedZombie = ZombieEnemies[Random.Range(0, ZombieEnemies.Length)];
            SpawnerTrigger trigger = TriggerZones[Random.Range(0, TriggerZones.Length)];
            GameObject zombie = Instantiate(spawnedZombie, trigger.GetPositionInBounds(), trigger.transform.rotation);
            
            zombie.GetComponent<ZombieComponent>().Initialize(PlayerGameObject.gameObject);
        }
        
    }
}