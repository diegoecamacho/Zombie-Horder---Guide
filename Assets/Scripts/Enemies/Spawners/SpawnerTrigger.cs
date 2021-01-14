using UnityEngine;


namespace Enemies.Spawners
{
    public class SpawnerTrigger : MonoBehaviour
    {
        private BoxCollider Collider;

        private void Awake()
        {
            Collider = GetComponentInChildren<BoxCollider>();
            
            Debug.Log(GetPositionInBounds());
        }

        public Vector3 GetPositionInBounds()
        {
            Bounds bounds = Collider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                transform.position.y,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
