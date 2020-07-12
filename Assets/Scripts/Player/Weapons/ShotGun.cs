using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : PlayerWeapon
{
    [SerializeField] private int bulletAmount = 3;
    [SerializeField] private float bulletSpread = 45f;

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
        if (fireSoundName.Length > 0) {
            AudioManager.instance.Play(fireSoundName, Random.Range(-0.2f, 0.2f));
        }

        timeToNextShot = timeBetweenShots;

        var degreesPerStep = bulletSpread / bulletAmount;
        var offset = bulletSpread / 2;

        for(int i = 0; i < bulletAmount; i++)
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            var bulletComponent = bullet.GetComponent<Bullet>();
            var bulletDirection = Quaternion.AngleAxis(offset, Vector3.forward) * direction;
            offset -= degreesPerStep;

            bulletComponent.direction = bulletDirection;
            bulletComponent.speed = bulletSpeed;
        }

        CameraShaker.instance.AddShakeEvent(shakeData);

        bulletsLeft--;
        if(bulletsLeft <= 0)
        {
            onMagazineEmpty?.Invoke();
        }
    }
}