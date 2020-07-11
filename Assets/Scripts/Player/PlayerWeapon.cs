using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : PlayerHealthComponent
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int magazineSize;
    [SerializeField] private float firerate;
    [SerializeField] private float bulletSpeed;

    private float timeBetweenShots;
    private float timeToNextShot;

    private void Start()
    {
        timeBetweenShots = 1f / firerate;
        timeToNextShot = 0f;
    }

    private void Update()
    {
        if(timeToNextShot > 0f)
        {
            timeToNextShot -= Time.deltaTime;
        }
    }

    public virtual void Fire(Vector2 direction)
    {
        if(timeToNextShot > 0f)
        {
            return;
        }

        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = bulletSpeed;
        timeToNextShot = timeBetweenShots;
    }
}
