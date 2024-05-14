using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsEx
{
    public static void AddExplosionForce(this Rigidbody2D rb, float force, Vector2 pos,
        float radius,
        float upwardsModifier = 0.0F,
        ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - pos;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, force, (1 - explosionDistance)) * explosionDir, mode);
    }
}
