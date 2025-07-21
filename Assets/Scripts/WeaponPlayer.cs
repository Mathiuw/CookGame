using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EAimState
{
    Front,
    Up,
    Down
}

public class WeaponPlayer : Weapon
{
    public EAimState aimState { get; private set; } = EAimState.Front;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement)
        {
            playerMovement.Input.Player.Attack.started += OnAttackStarted;
            playerMovement.Input.Player.AimUp.started += OnAimUpStarted;
            playerMovement.Input.Player.AimUp.canceled += OnAimUpCanceled;
            playerMovement.Input.Player.AimDown.canceled += OnAimDownCanceled;
        }
    }

    private void OnDisable()
    {
        if (playerMovement)
        {
            playerMovement.Input.Player.Attack.started -= OnAttackStarted;
            playerMovement.Input.Player.AimUp.started -= OnAimUpStarted;
            playerMovement.Input.Player.AimUp.canceled -= OnAimUpCanceled;
            playerMovement.Input.Player.AimDown.canceled -= OnAimDownCanceled;
        }
    }

    private void Update()
    {
        // Aim down input
        if (playerMovement.Input.Player.AimDown.IsPressed())
        {
            if (!playerMovement.Grounded)
            {
                SetAimState(EAimState.Down);
            }
            else if (playerMovement.Input.Player.AimUp.IsPressed())
            {
                SetAimState(EAimState.Up);
            }
            else
            {
                SetAimState(EAimState.Front);
            }
        }
    }

    private void OnAimUpStarted(InputAction.CallbackContext context)
    {
        SetAimState(EAimState.Up);
    }

    private void OnAimUpCanceled(InputAction.CallbackContext context)
    {
        SetAimState(EAimState.Front);
    }

    private void OnAimDownCanceled(InputAction.CallbackContext context)
    {
        if (aimState != EAimState.Up)
        {
            SetAimState(EAimState.Front);
        }     
    }

    private void SetAimState(EAimState state)
    {
        aimState = state;
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        Quaternion bulletRotation = Quaternion.identity;
        float bulletSpeed = BaseBulletSpeed;

        switch (aimState)
        {
            case EAimState.Front:
                bulletSpeed *= transform.localScale.x;
                break;
            case EAimState.Up:
                bulletRotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case EAimState.Down:
                bulletRotation = Quaternion.Euler(0f, 0f, -90f);
                break;
            default:
                break;
        }

        SpawnBullet(transform.position, bulletRotation, bulletSpeed);
    }
}
