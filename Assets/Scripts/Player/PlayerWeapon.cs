using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : PlayerHealthComponent
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int magazineSize;
    [SerializeField] private float firerate;
    [SerializeField] private float bulletImpulse;

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

        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(direction * bulletImpulse, ForceMode2D.Impulse);
        timeToNextShot = timeBetweenShots;
    }
}
