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
        if (timeToNextShot <= 0f && bulletsLeft > 0) {
            if (fireSoundName.Length > 0) {
                AudioManager.instance.Play(fireSoundName);
            }
        }
        base.Fire(direction);
    }
}