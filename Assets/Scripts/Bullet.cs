using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: Header("Bullet Settings")]
    [field: SerializeField] public float Speed { get; set; } = 3000f;
    [field: SerializeField] public float Damage { get; set; } = 30f;
    [SerializeField] float timeToDestroy = 5f;
    Rigidbody2D rb;

    public delegate void OnBulletHitDelegate();
    public event OnBulletHitDelegate OnBulletHit;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * Speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collide");
        
        if (collision.transform.TryGetComponent(out Health health))
        {
            // Apply damage
            health.ApplyDamage(Damage);

            // If character is dead, apply force to the body
            if (health.IsDead && collision.transform.TryGetComponent(out Rigidbody2D collisionRb))
            {
                collisionRb.AddForceAtPosition(new Vector2(Speed/100, 0), collision.GetContact(0).point);
                Debug.Log("Applied force to " + collision.transform.name + " body");
            }
        }

        OnBulletHit?.Invoke();
        Destroy(gameObject);
    }
}