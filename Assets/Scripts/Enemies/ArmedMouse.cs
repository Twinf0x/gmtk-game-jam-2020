using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedMouse : EnemyMouse
{
    private const float bulletSpeed = 3f;
    [SerializeField] internal Transform firePoint;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal float timeBetweenShots;
    internal float timeToNextShot;

    private void Update()
    {
        if(timeToNextShot > 0f)
        {
            timeToNextShot -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    internal override void FixedUpdate() {
        var movementdirection = target.position - transform.position;
        if (Random.Range(0, 2) > 0)
        {
            movementdirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        Move(movementdirection.normalized);
        var shootdirection = target.position - transform.position;
        Shoot(shootdirection.normalized);
    }

    private void Move(Vector2 direction) {
       base.body.velocity = direction * movementSpeed;
    }

    private void Shoot(Vector2 direction) {
        if (timeToNextShot > 0f) {
            return;
        }

        timeToNextShot = timeBetweenShots;
        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = bulletSpeed;
    }
}
