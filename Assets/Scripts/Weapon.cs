using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [field: Header("Bullet Settings")]
        [SerializeField] protected Bullet bulletPrefab;
        [field: SerializeField] public float BaseBulletSpeed { get; private set; } = 3000;

        public Bullet SpawnBullet(Vector3 position, Quaternion rotation, float speed) 
        {
            Bullet bullet = Instantiate(bulletPrefab, position, rotation);
            bullet.Speed = speed;
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>(), true);

            Debug.Log("Spawned Bullet");
            return bullet;
        }
    }
}