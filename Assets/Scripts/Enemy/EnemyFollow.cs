using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float stopDistance = 1.5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null) return;

        Vector2 playerPosition = playerObj.transform.position;
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
        // Aqui pode adicionar lógica de ataque se desejar
    }
}