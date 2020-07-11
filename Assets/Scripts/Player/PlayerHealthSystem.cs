using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private PlayerHealthComponent[] movementComponents;
    [SerializeField] private PlayerWeapon[] weaponComponents;

    public PlayerWeapon GetRandomActiveWeapon()
    {
        var index = Random.Range(0, weaponComponents.Length);

        return weaponComponents[index];
    }
}
