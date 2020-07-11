using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject corpsePrefab;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
