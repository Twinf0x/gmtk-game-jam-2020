using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerWeapon fallbackWeapon;
    [SerializeField] private PlayerController controller;
    [SerializeField] private GameOverScreenController gameOverScreenController;
    [SerializeField] private GameObject breatherObject;
    [SerializeField] private GameObject weaponDamagedHint;
    [SerializeField] private GameObject movementDamagedHint;

    [Header("Settings")]
    [SerializeField] private float breatherTime = 0.5f;
    [SerializeField] private Vector3 breatherMaxSize = new Vector3(10f, 10f, 1f);

    [HideInInspector] public List<PlayerWeapon> activeWeaponComponents;
    [HideInInspector] public List<PlayerMovement> activeMovementComponents;
    [HideInInspector] public bool isInvincible = false;

    private void Start()
    {
        Debug.Log("Health System Initialize");
        activeMovementComponents = gameObject.GetComponents<PlayerMovement>().ToList();
        activeWeaponComponents = gameObject.GetComponents<PlayerWeapon>().Where(weapon => weapon != fallbackWeapon).ToList();
    }

    public void DeactivateRandomComponent()
    {
        if (isInvincible)
        {
            return;
        }

        StartCoroutine(GiveBreathingRoom(breatherMaxSize));
        if(activeWeaponComponents.Count > 0)
        {
            var weapon = activeWeaponComponents.ElementAt(Random.Range(0, activeWeaponComponents.Count));
            activeWeaponComponents.Remove(weapon);
            weapon.enabled = false;
            weapon.onDeactivation?.Invoke();

            if(weapon == controller.currentWeapon)
            {
                StartCoroutine(controller.SwitchWeapons(controller.reloadTime));
            }

            StartCoroutine(TurnOnForSeconds(weaponDamagedHint, 1f));

            return;
        }

        if(activeMovementComponents.Count > 0)
        {
            var movement = activeMovementComponents.ElementAt(Random.Range(0, activeMovementComponents.Count));
            activeMovementComponents.Remove(movement);
            movement.enabled = false;
            movement.onDeactivation?.Invoke();

            StartCoroutine(TurnOnForSeconds(movementDamagedHint, 1f));
            return;
        }

        Die();
    }

    public IEnumerator GiveBreathingRoom(Vector3 maxSize)
    {
        breatherObject.SetActive(true);
        var breatherDefaultSize = new Vector3(1f, 1f, 1f);
        var timer = breatherTime;
        while(timer > 0f)
        {
            breatherObject.transform.localScale = Vector3.Lerp(breatherDefaultSize, maxSize, 1f - (timer / breatherTime));
            timer -= Time.deltaTime;
            yield return null;
        }
        breatherObject.SetActive(false);
    }

    private IEnumerator TurnOnForSeconds(GameObject target, float timer)
    {
        target.SetActive(true);
        yield return new WaitForSeconds(timer);
        target.SetActive(false);
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

    public PlayerWeapon GetWeaponAtIndex(int index)
    {
        return activeWeaponComponents.ElementAt(index);
    }

    public void Die()
    {
        gameOverScreenController.ShowScreen();
        Destroy(controller.gameObject);
    }
}
