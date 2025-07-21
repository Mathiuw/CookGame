using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class EnemyAI : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] float speed = 2f;
    [SerializeField] float stopDistance = 1.5f;
    private Rigidbody2D rb;

    [Header("Attack Settings")]
    [SerializeField] private float shootCooldown = 2f;
    private Weapon weapon;

    public Transform Target { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        Target = GameObject.FindWithTag("Player").transform;

        InvokeRepeating("ShootTarget", 1f, shootCooldown);
    }

    void Update()
    {
        if (!Target) return;

        Vector2 playerPosition = Target.position;
        float distance = Vector2.Distance(transform.position, playerPosition);

        if (distance > stopDistance)
        {
            Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
            // Move apenas no eixo X, mantendo a velocidade Y (gravidade)
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else
        {
            // Para o movimento horizontal ao se aproximar do player
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        SetSpriteScale(rb.linearVelocityX);
    }

    private void ShootTarget() 
    {
        if (!Target) return;

        Vector2 playerPosition = Target.position;
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        float bulletSpeed = weapon.BaseBulletSpeed * direction.x;

        Bullet bullet = weapon.SpawnBullet(transform.position, Quaternion.identity, bulletSpeed);
        bullet.OnBulletHit += IsTargetAlive;
    }

    private void IsTargetAlive() 
    {
        if (Target.TryGetComponent(out Health health) && health.IsDead)
        {
            Debug.Log("Target is Dead, target == null");
            Target = null;
        }
        else
        {
            Debug.Log("Target doesnt have life");
        }
    }

    private void SetSpriteScale(float xMoveForce)
    {
        if (xMoveForce < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}