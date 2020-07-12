using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] internal GameObject hitEffectPrefab;
    [SerializeField] internal Rigidbody2D body;
    [SerializeField] internal float damageOnHit;
    [SerializeField] internal string damageTag;
    [SerializeField] internal string soundName;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float speed;

    internal void FixedUpdate()
    {
        body?.MovePosition(new Vector2(transform.position.x, transform.position.y) + (direction * speed * Time.fixedDeltaTime));
    }

    internal virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (soundName.Length > 0) {
            AudioManager.instance.Play(soundName);
        }
        if (hitEffectPrefab != null)
        {
            var hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 5f);
        }

        foreach(var contact in collision.contacts)
        {
            if(contact.collider.gameObject.tag != damageTag)
            {
                continue;
            }

            var enemy = contact.collider.gameObject.GetComponent<EnemyHealth>();
            if(enemy != null)
            {
                enemy.TakeDamage(damageOnHit);
                continue;
            }

            var player = contact.collider.gameObject.GetComponent<PlayerHealthSystem>();
            if(player != null)
            {
                player.DeactivateRandomComponent();
            }
        }

        Destroy(gameObject);
    }
}
