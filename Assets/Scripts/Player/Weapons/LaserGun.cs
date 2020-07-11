using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaserGun : PlayerWeapon
{
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float castRadius;
    [SerializeField] private float laserRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LineRenderer renderer;

    private bool isActive = false;

    internal override void Start(){
        base.Start();
    }

    internal override void Update() {
        base.Update();

        renderer.enabled = Input.GetKey(KeyCode.Mouse0) && isActive;
    }

    public override void Activate() {
        base.Activate();

        isActive = true;
    }

    public override void Fire(Vector2 direction) {

        if(bulletsLeft <= 0) {
            return;
        }

        RaycastHit2D[] greatestHits = Physics2D.CircleCastAll(firePoint.position, castRadius, direction, Mathf.Infinity, layerMask);

        foreach( var hit in greatestHits) {
            if (hit.collider) {
                var hitTarget = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (hitTarget) {
                    hitTarget.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }

        renderer.SetPosition(0, firePoint.position);
        renderer.SetPosition(1, firePoint.position + new Vector3(direction.x, direction.y, 0) * laserRange);

        bulletsLeft--;
        if(bulletsLeft <= 0)
        {
            renderer.enabled = false;
            isActive = false;
            Debug.Log("Switching weapons");
            onMagazineEmpty?.Invoke();
        }
    }
}