using UnityEngine;


namespace Enemies.Spawners
{
    public class SpawnerTrigger : MonoBehaviour
    {
        private BoxCollider Collider;

        private void Awake()
        {
            Collider = GetComponentInChildren<BoxCollider>();
        }

        public Vector3 GetPositionInBounds()
        {
            var bounds = Collider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                transform.position.y,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}
