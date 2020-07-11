using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : PlayerWeapon
{
    internal override void Start(){
        base.Start();
    }

    internal override void Update() {
        base.Update();
    }

    public override void Activate() {
        base.Activate();
    }

    public override void Fire(Vector2 direction) {
        if(timeToNextShot > 0f || bulletsLeft <= 0)
        {
            return;
        }

        timeToNextShot = timeBetweenShots;

        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = bulletSpeed;

        bulletsLeft--;
        if(bulletsLeft <= 0)
        {
            Debug.Log("Switching weapons");
            onMagazineEmpty?.Invoke();
        }

        Debug.Log("Bullets Left: " + bulletsLeft.ToString());
    }
}