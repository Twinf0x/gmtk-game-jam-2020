using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : PlayerHealthComponent
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int magazineSize;
    [SerializeField] private float firerate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private UnityEvent onMagazineEmpty;

    private float timeBetweenShots;
    private float timeToNextShot;

    private int bulletsLeft;

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

    public virtual void Activate()
    {
        bulletsLeft = magazineSize;
    }

    public virtual void Fire(Vector2 direction)
    {
        if(timeToNextShot > 0f || bulletsLeft <= 0)
        {
            return;
        }

        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = bulletSpeed;
        timeToNextShot = timeBetweenShots;

        bulletsLeft--;
        if(bulletsLeft <= 0)
        {
            Debug.Log("Switching weapons");
            onMagazineEmpty?.Invoke();
        }

        Debug.Log("Bullets Left: " + bulletsLeft.ToString());
    }
}
