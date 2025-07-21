using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Transform weaponPivot;
    WeaponPlayer weaponPlayer;

    private void Start()
    {
        weaponPlayer = GetComponent<WeaponPlayer>();
    }

    private void Update()
    {
        switch (weaponPlayer.aimState)
        {
            case EAimState.Front:
                weaponPivot.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case EAimState.Up:
                weaponPivot.localEulerAngles = new Vector3(0f, 0f, 90);
                break;
            case EAimState.Down:
                weaponPivot.localEulerAngles = new Vector3(0f, 0f, -90);
                break;
            default:
                break;
        }
    }
}
