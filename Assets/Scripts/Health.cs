using UnityEngine;

public class Health : MonoBehaviour
{
    [field: Header("Health Settings")]
    [field: SerializeField] public float MaxHealth { get; private set; } = 100;
    public float HealthValue { get; private set; }

    public bool IsDead { get; private set; } = false;

    public delegate void OnHealthChangeDelegate(float health);
    public event OnHealthChangeDelegate OnHealthChange;

    public delegate void OnDiedDelegate();
    public event OnDiedDelegate OnDied;

    private void Awake()
    {
        SetHealth(MaxHealth);
    }

    public void SetHealth(float health) 
    {
        if (IsDead)
        {
            return;
        }

        HealthValue = health;
        HealthValue = Mathf.Clamp(HealthValue, 0, MaxHealth);
        OnHealthChange?.Invoke(health);
        Debug.Log(transform.name + " health value changed");

        if (HealthValue <= 0)
        {
            Debug.Log(transform.name + " died");
            OnDied?.Invoke();
            IsDead = true;
        }
    }

    public void ApplyDamage(float damage) 
    {
        float healthResult = HealthValue - damage;

        SetHealth(healthResult);
    }
}
