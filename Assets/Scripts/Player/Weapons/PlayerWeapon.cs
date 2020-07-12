using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : PlayerHealthComponent
{
    [SerializeField] internal Transform firePoint;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal int magazineSize;
    [SerializeField] internal float firerate;
    [SerializeField] internal float bulletSpeed;
    [SerializeField] internal UnityEvent onMagazineEmpty;
    [SerializeField] internal SpriteRenderer weaponRenderer;
    [SerializeField] internal ShakeTransformEventData shakeData;

    internal float timeBetweenShots;
    internal float timeToNextShot;

    internal int bulletsLeft;

    internal virtual void Start()
    {
        timeBetweenShots = 1f / firerate;
        timeToNextShot = 0f;
    }

    internal virtual void Update()
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

    internal virtual void OnDeactivation()
    {

    }

    public virtual void Fire(Vector2 direction)
    {
        if(timeToNextShot > 0f || bulletsLeft <= 0)
        {
            return;
        }

        timeToNextShot = timeBetweenShots;

        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = bulletSpeed;

        CameraShaker.instance.AddShakeEvent(shakeData);

        bulletsLeft--;
        if(bulletsLeft <= 0)
        {
            Debug.Log("Switching weapons");
            onMagazineEmpty?.Invoke();
        }
    }
}
