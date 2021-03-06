﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaserGun : PlayerWeapon
{
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float castRadius;
    [SerializeField] private float laserRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private new LineRenderer renderer;

    private float timeBetweenBeams;
    private float timeToNextBeam;

    private bool isActive = false;

    internal override void Start(){
        base.Start();
        timeBetweenBeams = 1f / 500f;
        timeToNextBeam = 0f;
    }

    internal override void Update() {
        base.Update();
        renderer.enabled = Input.GetKey(KeyCode.Mouse0) && isActive;
        if(Input.GetKeyDown(KeyCode.Mouse0) && isActive && fireSoundName.Length > 0) {
            // Debug.Log("<b>LAAAZOOR!!!</b>");
            AudioManager.instance.Play(fireSoundName);
        } else if(!renderer.enabled) {
            AudioManager.instance.Stop(fireSoundName);
        }
        timeToNextBeam -= Time.deltaTime;
    }

    public override void Activate() {
        base.Activate();
        isActive = true;
        if (!renderer.enabled) {
            AudioManager.instance.Play(fireSoundName);
        }
    }

    internal override void OnDeactivation() {
        renderer.enabled = false;
        if (fireSoundName.Length > 0) {
            AudioManager.instance.Stop(fireSoundName);
        }
        isActive = false;
    }

    public override void Fire(Vector2 direction) {

        if(bulletsLeft <= 0 || timeToNextBeam > 0f) {
            return;
        }

        timeToNextBeam = timeBetweenBeams;

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

        CameraShaker.instance.AddShakeEvent(shakeData);

        bulletsLeft--;
        if(bulletsLeft <= 0)
        {
            renderer.enabled = false;
            isActive = false;
            onMagazineEmpty?.Invoke();
        }
    }
}