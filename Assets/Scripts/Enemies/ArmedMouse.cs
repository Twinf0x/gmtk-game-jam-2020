using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedMouse : EnemyMouse
{
    private const float bulletSpeed = 3f;
    [SerializeField] internal Transform weapon;
    [SerializeField] internal Transform firePoint;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal float timeBetweenShots;
    [SerializeField] internal string fireSoundName;
    internal float timeToNextShot;

    private float weaponXRight;
    private float weaponXLeft;

    internal override void Start() {
        base.Start();
        weaponXRight = weapon.localPosition.x;
        weaponXLeft = weaponXRight * -1;
    }

    private void Update()
    {
        if(timeToNextShot > 0f)
        {
            timeToNextShot -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    internal override void FixedUpdate() {
        if (target == null) {
            return;
        }
        var movementdirection = target.position - transform.position;
        if (Random.Range(0, 2) > 0)
        {
            movementdirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        Move(movementdirection.normalized);
        var shootdirection = target.position - transform.position;

        float currentWeaponRotation = Mathf.Atan2(shootdirection.y, shootdirection.x) * Mathf.Rad2Deg;
        var temp = weapon.rotation.eulerAngles;
        weapon.rotation = Quaternion.Euler(temp.x, temp.y, currentWeaponRotation);

        Shoot(shootdirection.normalized);
        HandleCharacterAnimation(shootdirection);
    }

    private void HandleCharacterAnimation(Vector2 shootdirection) {
        if (shootdirection.x < 0) {
            characterRenderer.flipX = true;
            Vector3 weaponPosition = new Vector3(weaponXLeft, weapon.localPosition.y, weapon.localPosition.z);
            weapon.localPosition = weaponPosition;
            weapon.localScale = new Vector3(1, -1, 1);
        } else {
            characterRenderer.flipX = false;
            Vector3 weaponPosition = new Vector3(weaponXRight, weapon.localPosition.y, weapon.localPosition.z);
            weapon.localPosition = weaponPosition;
            weapon.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Move(Vector2 direction) {
       base.body.velocity = direction * movementSpeed;
    }

    private void Shoot(Vector2 direction) {
        if (timeToNextShot > 0f) {
            return;
        }
        if (fireSoundName.Length > 0) {
            AudioManager.instance.Play(fireSoundName, Random.Range(-0.2f, 0.2f));
        }

        timeToNextShot = timeBetweenShots;
        var bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.direction = direction;
        bullet.speed = bulletSpeed;
    }
}
