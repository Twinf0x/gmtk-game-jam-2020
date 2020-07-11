using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform weapon;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerMovement[] movementDirections;
    [SerializeField] private Dash dash;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private UnityEvent onSwitchedWeapons;

    private Vector2 currentMovementDirection;
    private float currentWeaponRotation;

    [HideInInspector] public PlayerWeapon currentWeapon = null;

    private void Start()
    {
        SwitchWeapons();
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

        Vector2 direction2D = new Vector2(lookDirection.x, lookDirection.y).normalized;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentWeapon.Fire(direction2D);
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
        if (dash.active) {
            body.velocity = direction * dash.speed;
            return;
        }
        body.velocity = direction * movementSpeed;
    }

    public void SwitchWeapons()
    {
        currentWeapon = healthSystem.GetRandomActiveWeapon();
        currentWeapon.Activate();

        onSwitchedWeapons?.Invoke();
    }
}
