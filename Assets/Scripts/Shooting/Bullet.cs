using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] internal GameObject hitEffectPrefab;
    [SerializeField] internal Rigidbody2D body;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float speed;

    internal void FixedUpdate()
    {
        body.MovePosition(new Vector2(transform.position.x, transform.position.y) + (direction * speed * Time.fixedDeltaTime));
    }

    internal virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(hitEffectPrefab != null)
        {
            var hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 5f);
        }

        Destroy(gameObject);
    }
}
