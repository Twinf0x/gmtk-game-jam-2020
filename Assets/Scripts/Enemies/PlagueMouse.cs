using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueMouse : EnemyMouse
{
    private const float bulletSpeed = 1f;
    [SerializeField] internal Transform firePoint;
    [SerializeField] internal GameObject bulletPrefab;
    [SerializeField] internal float timeBetweenSpits;
    [SerializeField] internal string spitSoundName;
    internal float timeToNextSpit;
    internal int bulletAmount = 6;
    internal float bulletSpread = 360f;

    internal Vector2 movementdirection = Vector2.zero;

    // Update is called once per frame
    internal override void FixedUpdate() {
        if (target == null) {
            return;
        }
        if (timeToNextSpit > 0f)
        {
            Move(movementdirection);
            timeToNextSpit -= Time.fixedDeltaTime;
            return;
        }

        movementdirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        var playerdirection = base.target.position - transform.position;
        Spit(playerdirection.normalized);
        if (playerdirection.x < 0) {
            characterRenderer.flipX = true;
        } else {
            characterRenderer.flipX = false;
        }
    }

    private void Move(Vector2 direction) {
       base.body.velocity = direction * movementSpeed;
    }

    private void Spit(Vector2 direction) {
        if (timeToNextSpit > 0f) {
            return;
        }
        if (spitSoundName.Length > 0) {
            AudioManager.instance.Play(spitSoundName, Random.Range(-0.2f, 0.2f));
        }

        timeToNextSpit = timeBetweenSpits;

        var degreesPerStep = bulletSpread / bulletAmount;
        var offset = bulletSpread / 2;

        for (int i = 0; i < bulletAmount; i++)
        {
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            var bulletComponent = bullet.GetComponent<Bullet>();
            var bulletDirection = Quaternion.AngleAxis(offset, Vector3.forward) * direction;
            offset -= degreesPerStep;

            bulletComponent.direction = bulletDirection;
            bulletComponent.speed = bulletSpeed;
            Destroy(bullet, 2f);
        }
    }
}
