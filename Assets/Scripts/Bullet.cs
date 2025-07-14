using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; }
    [SerializeField] float timeToDestroy = 5f;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(new Vector2(Speed, 0) * transform.localScale.x * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collide");
        Destroy(gameObject);
    }
}