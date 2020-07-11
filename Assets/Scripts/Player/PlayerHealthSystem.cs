using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private PlayerWeapon fallbackWeapon;

    private List<PlayerWeapon> activeWeaponComponents;
    private List<PlayerMovement> activeMovementComponents;
    private PlayerWeapon activeWeapon;

    private void Start()
    {
        activeMovementComponents = gameObject.GetComponents<PlayerMovement>().ToList();
        activeWeaponComponents = gameObject.GetComponents<PlayerWeapon>().Where(weapon => weapon != fallbackWeapon).ToList();
    }

    public void DeactivateRandomComponent()
    {
        if(activeWeaponComponents.Count > 0)
        {
            var weapon = activeWeaponComponents.ElementAt(Random.Range(0, activeWeaponComponents.Count));
            activeWeaponComponents.Remove(weapon);
            weapon.enabled = false;
        }

        if(activeMovementComponents.Count > 0)
        {
            var movement = activeMovementComponents.ElementAt(Random.Range(0, activeMovementComponents.Count));
            activeMovementComponents.Remove(movement);
            movement.enabled = false;
        }

        Die();
    }

    public PlayerWeapon GetRandomActiveWeapon()
    {
        if(activeWeaponComponents.Count < 0)
        {
            return fallbackWeapon;
        }

        if(activeWeaponComponents.Count == 1)
        {
            return activeWeaponComponents.First();
        }

        var weaponOptions = activeWeaponComponents.Where(weapon => weapon != activeWeapon).ToList();

        var index = Random.Range(0, weaponOptions.Count);
        activeWeapon = weaponOptions.ElementAt(index);
        return activeWeapon;
    }

    public void Die()
    {
        //TODO
    }
}
