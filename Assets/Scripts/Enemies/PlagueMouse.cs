using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMouse : EnemyMouse
{
    private const float bulletSpeed = 3f;
    [SerializeField] internal Transform firePoint;
    [SerializeField] internal GameObject bulletPrefab;
    internal float timeBetweenSpits;
    internal float timeToNextSpit;

    // Start is called before the first frame update
    void Start()
    {
        base.movementSpeed = 2f;
    }

    // Update is called once per frame
    internal override void FixedUpdate() {
        var playerdirection = base.target.position - transform.position;
        Move(playerdirection.normalized);

        if (base.body.position.x < 30) {
            Spit(playerdirection);
        }
    }

    private void Move(Vector2 direction) {
       base.body.velocity = direction * movementSpeed;
    }

    private void Spit(Vector2 direction) {
        if (timeToNextSpit > 0f) {
            return;
        }

        timeToNextSpit = timeBetweenSpits;
        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = transform.position - base.target.position;
        bullet.speed = bulletSpeed;
    }
}
