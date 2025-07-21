using UnityEngine;

public class DeathEnemy : Death
{
    protected override void OnDied()
    {
        // Disable enemy AI
        TryGetComponent(out EnemyAI enemyFollow);
        if (enemyFollow)
        {
            Destroy(enemyFollow);
        }

        // Enable Enemy rigidody roll
        TryGetComponent(out Rigidbody2D rb);
        if (rb)
        {
            rb.freezeRotation = false;
        }

        base.OnDied();
    }
}