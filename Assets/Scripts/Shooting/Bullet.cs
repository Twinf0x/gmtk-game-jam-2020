using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private Rigidbody2D body;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float speed;

    private void FixedUpdate()
    {
        body.MovePosition(new Vector2(transform.position.x, transform.position.y) + (direction * speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hitEffectPrefab != null)
        {
            var hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 5f);
        }

        Destroy(gameObject);
    }
}
