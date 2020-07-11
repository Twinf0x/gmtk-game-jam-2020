using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private PlayerWeapon fallbackWeapon;
    [SerializeField] private PlayerController controller;

    private List<PlayerWeapon> activeWeaponComponents;
    private List<PlayerMovement> activeMovementComponents;

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

            if(weapon == controller.currentWeapon)
            {
                Debug.Log("deactivated active weapon");
                controller.SwitchWeapons();
            }

            Debug.Log("Active Weapons: " + activeWeaponComponents.Count.ToString());
            return;
        }

        if(activeMovementComponents.Count > 0)
        {
            var movement = activeMovementComponents.ElementAt(Random.Range(0, activeMovementComponents.Count));
            activeMovementComponents.Remove(movement);
            movement.enabled = false;
            Debug.Log("Active Movement: " + activeMovementComponents.Count.ToString());
            return;
        }

        Die();
    }

    public PlayerWeapon GetRandomActiveWeapon()
    {
        if(activeWeaponComponents.Count <= 0)
        {
            return fallbackWeapon;
        }

        if(activeWeaponComponents.Count == 1)
        {
            return activeWeaponComponents.First();
        }

        var weaponOptions = activeWeaponComponents.Where(weapon => weapon != controller.currentWeapon).ToList();

        var index = Random.Range(0, weaponOptions.Count);
        return weaponOptions.ElementAt(index);
    }

    public void Die()
    {
        //TODO
    }
}
