using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Transform weaponPivot;
    Weapon weapon;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        if (weapon.AimUp)
        {
            weaponPivot.localEulerAngles = new Vector3(0f, 0f, 90);
        }
        else
        {
            weaponPivot.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
