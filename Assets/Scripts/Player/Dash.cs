using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dash : PlayerMovement
{
    [SerializeField] private PlayerHealthSystem playerHealthSystem;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] internal float timeBetweenDashes = 7f;
    [SerializeField] internal Vector3 breatherSize = new Vector3(5f, 5f, 1f);

    internal float timeToNextDash;

    [HideInInspector] public bool isActive;
    [HideInInspector] public float speed = 3f;

    internal void Start() {
        timeToNextDash = 0f;
    }

    internal override void Update() {
        if (timeToNextDash > 0f){
            timeToNextDash -= Time.deltaTime;
            return;
        }
        if (Input.GetKey(KeyCode.Space)) {
            StartCoroutine(ActivateDash());
        }
    }

    public IEnumerator ActivateDash(float duration = 0.5f) {
        timeToNextDash = timeBetweenDashes;
        isActive = true;
        playerHealthSystem.isInvincible = true;

        yield return new WaitForSeconds(duration);
        isActive = false;
        playerHealthSystem.isInvincible = false;
        StartCoroutine(playerHealthSystem.GiveBreathingRoom(breatherSize));
    }
}
