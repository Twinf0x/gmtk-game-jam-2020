using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;

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
