using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject corpsePrefab;
    [SerializeField] private int score;
    [SerializeField] internal string deathSoundName;
    private float currentHealth;

    internal void Start()
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

    public void Die()
    {
        if (deathSoundName.Length > 0) {
            AudioManager.instance.Play(deathSoundName);
        }
        ScoreController.instance.AddKill(score);
        Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
