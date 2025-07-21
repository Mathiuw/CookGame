using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathPlayer : Death
{
    protected override void OnDied() 
    {
        // Disable Player Input
        TryGetComponent(out PlayerMovement playerMovement);
        if (playerMovement)
        {
            playerMovement.Input.Disable();
        }

        // Make Player colllider slide to simulte a body falling
        TryGetComponent(out Collider2D collider2D);
        if (collider2D)
        {
            PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D();
            physicsMaterial2D.friction = 0;
            physicsMaterial2D.frictionCombine = 0;
            physicsMaterial2D.frictionCombine = PhysicsMaterialCombine2D.Maximum;

            collider2D.sharedMaterial = physicsMaterial2D;
        }

        // Enable Player rigidody roll
        TryGetComponent(out Rigidbody2D rb);
        if (rb)
        {
            rb.freezeRotation = false;
        }

        base.OnDied();
    }
}
