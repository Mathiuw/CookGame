using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Death : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Health>().OnDied += OnDied;
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnDied -= OnDied;
    }

    protected virtual void OnDied() 
    {
        Destroy(this);
    }
}
