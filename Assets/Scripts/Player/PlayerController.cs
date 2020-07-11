using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform weapon;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerMovement[] movementDirections;
    [SerializeField] private float movementSpeed = 5f;

    private Vector2 currentMovementDirection;
    private float currentWeaponRotation;

    private PlayerWeapon activeWeapon;

    private void Start()
    {
        activeWeapon = healthSystem.GetRandomActiveWeapon();
    }

    private void Update()
    {
        currentMovementDirection = Vector2.zero;

        foreach(var direction in movementDirections)
        {
            if (!direction.enabled)
            {
                continue;
            }

            currentMovementDirection += direction.Value;
        }

        Vector3 lookDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        currentWeaponRotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            activeWeapon.Fire(lookDirection.normalized);
        }
    }

    private void FixedUpdate()
    {
        Move(currentMovementDirection.normalized);
        var temp = weapon.rotation.eulerAngles;
        weapon.rotation = Quaternion.Euler(temp.x, temp.y, currentWeaponRotation); 
    }

    private void Move(Vector2 direction)
    {
        body.velocity = direction * movementSpeed;
    }
}
