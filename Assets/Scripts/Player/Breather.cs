using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breather : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "EnemyBullet")
        {
            return;
        }

        var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if(enemyHealth == null)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            enemyHealth.Die();
        }
    }
}
