using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float bulletSpeed = 40;
    public bool AimUp { get; private set; } = false;

    private void Start()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement)
        {
            playerMovement.Input.Player.Attack.started += OnAttackStarted;
            playerMovement.Input.Player.AimUp.started += OnAimUpStarted;
            playerMovement.Input.Player.AimUp.canceled += OnAimUpCanceled;
        }
    }

    private void OnDisable()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement)
        {
            playerMovement.Input.Player.Attack.started -= OnAttackStarted;
            playerMovement.Input.Player.AimUp.started -= OnAimUpStarted;
            playerMovement.Input.Player.AimUp.canceled -= OnAimUpCanceled;
        }
    }

    private void OnAimUpStarted(InputAction.CallbackContext context)
    {
        AimUp = true;
    }

    private void OnAimUpCanceled(InputAction.CallbackContext context)
    {
        AimUp = false;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>(), true);

        if (AimUp)
        {
            bullet.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            bullet.transform.localScale = Vector2.one;
        }
        else
        {
            bullet.transform.eulerAngles = Vector3.zero;
            bullet.transform.localScale = new Vector2(transform.localScale.x, 1f);
        }

        bullet.Speed = bulletSpeed;

        Debug.Log("Shoot Weapon");
    }
}
