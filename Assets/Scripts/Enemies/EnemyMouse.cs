using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouse : MonoBehaviour
{
    [SerializeField] internal Rigidbody2D body;
    [SerializeField] internal EnemyHealth health;
    [SerializeField] internal float movementSpeed;
   
    internal Transform target;

    internal void Start() {
        // call out loud
        movementSpeed = 2f;
    }

    internal virtual void FixedUpdate() {
        var playerdirection = target.position - transform.position;
        Move(playerdirection.normalized);
    }

    private void Move(Vector2 direction) {
        body.velocity = direction * movementSpeed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag != "Player")
        {
            return;
        }

        var playerHealth = collision.collider.gameObject.GetComponent<PlayerHealthSystem>();
        if(playerHealth == null)
        {
            return;
        }

        playerHealth.DeactivateRandomComponent();

        var ownHealth = gameObject.GetComponent<EnemyHealth>();
        ownHealth.Die();
    }
}
