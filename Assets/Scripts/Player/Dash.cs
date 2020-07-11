using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PlayerMovement
{
    [SerializeField] private Rigidbody2D body;

    internal float timeBetweenDashes = 7f;
    internal float timeToNextDash;

    public bool active;

    public float speed = 3f;

    internal void Start() {
        timeToNextDash = 0f;
    }

    internal override void Update() {
        if (timeToNextDash > 0f){
            timeToNextDash -= Time.deltaTime;
            return;
        }
        if (Input.GetKey(KeyCode.Space)) {
            StartCoroutine(activateDash());
        }
    }



    public IEnumerator activateDash(float duration = 0.5f) {
        timeToNextDash = timeBetweenDashes;
        active = true;

        yield return new WaitForSeconds(duration);
        active = false;
    }
}
