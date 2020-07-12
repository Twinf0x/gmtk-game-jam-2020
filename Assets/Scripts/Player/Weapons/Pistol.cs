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
        base.Fire(direction);
        if (fireSoundName.Length > 0) {
            AudioManager.instance.Play(fireSoundName, Random.Range(-0.2f, 0.2f));
        }
    }
}