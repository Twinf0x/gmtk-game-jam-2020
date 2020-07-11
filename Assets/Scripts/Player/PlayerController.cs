using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerMovement[] movementDirections;
    [SerializeField] private float movementSpeed = 5f;

    private void Update()
    {
        Vector2 currentMovementDirection = Vector2.zero;

        foreach(var direction in movementDirections)
        {
            if (!direction.enabled)
            {
                continue;
            }

            currentMovementDirection += direction.Value;
        }

        Move(currentMovementDirection.normalized);
    }

    private void Move(Vector2 direction)
    {
        body.velocity = direction * movementSpeed;
    }
}
