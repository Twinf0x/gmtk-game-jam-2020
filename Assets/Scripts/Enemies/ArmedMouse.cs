using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedMouse : EnemyMouse
{
    // Start is called before the first frame update
    void Start()
    {
        base.movementSpeed = 1f;
    }

    // Update is called once per frame
    internal override void FixedUpdate() {
        var playerdirection = transform.position - base.target.position;
        Move(playerdirection.normalized);
    }

    private void Move(Vector2 direction) {
       base.body.velocity = direction * movementSpeed;
    }

    private void Shoot(Vector2 direction) {

    }

}
