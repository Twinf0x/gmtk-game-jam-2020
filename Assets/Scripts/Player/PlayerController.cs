using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Finally! Wish for a specific weapon here:")]
    [SerializeField] private int startWeaponIndex = -1;

    [Header("All the other, boring, usual stuff")]
    [SerializeField] private Camera camera;
    [SerializeField] private Transform weapon;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerHealthSystem healthSystem;
    [SerializeField] private PlayerMovement[] movementDirections;
    [SerializeField] private Dash dash;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] public float reloadTime = 0.5f;
    [SerializeField] private UnityEvent onSwitchedWeapons;

    [SerializeField] private SpriteRenderer characterSpriteRenderer;
    [SerializeField] private Animator characterAnimator;

    [SerializeField] private GameObject reloadIndicator;
    [SerializeField] private Image reloadIndicatorFill;

    private Vector2 currentMovementDirection;
    private float currentWeaponRotation;
    private float weaponXRight;
    private float weaponXLeft;

    [HideInInspector] public PlayerWeapon currentWeapon = null;

    private void Start()
    {
        weaponXRight = weapon.localPosition.x;
        weaponXLeft = weaponXRight * -1;
        ScoreController.instance.ResetScores();

        if(startWeaponIndex == -1)
        {
            StartCoroutine(SwitchWeapons(0f));
        }
        else
        {
            StartCoroutine(SwitchWeapons(0f, startWeaponIndex));
        }
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

        HandleCharacterAnimator(currentMovementDirection);

        Vector3 lookDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        currentWeaponRotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        Vector2 direction2D = new Vector2(lookDirection.x, lookDirection.y).normalized;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentWeapon.Fire(direction2D);
        }
    }

    private void HandleCharacterAnimator(Vector2 movement) {
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        if(mouseWorldPos.x < transform.position.x) {
            characterSpriteRenderer.flipX = true;
            if(currentWeapon != null) {
                Vector3 weaponPosition = new Vector3(weaponXLeft, weapon.localPosition.y, weapon.localPosition.z);
                weapon.localPosition = weaponPosition;
                weapon.localScale = new Vector3(1, -1, 1);
            }
        } else {
            characterSpriteRenderer.flipX = false;
            if (currentWeapon != null) {
                Vector3 weaponPosition = new Vector3(weaponXRight, weapon.localPosition.y, weapon.localPosition.z);
                weapon.localPosition = weaponPosition;
                weapon.localScale = new Vector3(1, 1, 1);
            }
        }
        if(movement.magnitude > 0.1f) {
            characterAnimator.SetBool("isWalking", true);
        } else {
            characterAnimator.SetBool("isWalking", false);
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
        StartCoroutine(SwitchWeapons(reloadTime));
    }

    public IEnumerator SwitchWeapons(float timer)
    {
        reloadIndicator.SetActive(true);

        if (currentWeapon != null)
            currentWeapon.OnDeactivation();

        var timeLeft = timer;

        while(timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            reloadIndicatorFill.fillAmount = 1f - (timeLeft / timer);
            yield return null;
        }

        if(currentWeapon != null) {
            currentWeapon.weaponRenderer.enabled = false;
        }
        currentWeapon = healthSystem.GetRandomActiveWeapon();
        currentWeapon.Activate();
        currentWeapon.weaponRenderer.enabled = true;

        reloadIndicator.SetActive(false);
        onSwitchedWeapons?.Invoke();
    }

    public IEnumerator SwitchWeapons(float timer, int index)
    {
        reloadIndicator.SetActive(true);

        if(currentWeapon != null)
            currentWeapon.OnDeactivation();

        var timeLeft = timer;

        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            reloadIndicatorFill.fillAmount = 1f - (timeLeft / timer);
            yield return null;
        }

        if (currentWeapon != null)
        {
            currentWeapon.weaponRenderer.enabled = false;
        }
        currentWeapon = healthSystem.GetWeaponAtIndex(index);
        currentWeapon.Activate();
        currentWeapon.weaponRenderer.enabled = true;

        reloadIndicator.SetActive(false);
        onSwitchedWeapons?.Invoke();
    }
}
