using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damageOnHit;
    [SerializeField] private string damageTag;

    private void Start()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        var hits = Physics2D.CircleCastAll(position, range, Vector2.zero);

        foreach(var hit in hits)
        {
            if (hit.collider.gameObject.tag != damageTag)
            {
                continue;
            }

            var enemy = hit.collider.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageOnHit);
                continue;
            }

            var player = hit.collider.gameObject.GetComponent<PlayerHealthSystem>();
            if (player != null)
            {
                player.DeactivateRandomComponent();
            }
        }
    }
}
