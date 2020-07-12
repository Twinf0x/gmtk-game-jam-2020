using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerWeapon fallbackWeapon;
    [SerializeField] private PlayerController controller;
    [SerializeField] private GameOverScreenController gameOverScreenController;
    [SerializeField] private GameObject breatherObject;
    [SerializeField] private GameObject weaponDamagedHint;
    [SerializeField] private GameObject movementDamagedHint;

    [Header("Post Processing")]
    [SerializeField] private Volume postProcessing;
    [HideInInspector] private ChromaticAberration chromaticAberration;
    [SerializeField] private float normalAbberation = 0f;
    [HideInInspector] private float currentAbberation = 0f;
    [SerializeField] private float maxAbberation = 0.3f;
    [SerializeField] private float hitAbberation = 0.5f;

    [Header("Screen Shake")]
    [SerializeField] private ShakeTransform cameraShaker;
    [SerializeField] private ShakeTransformEventData shakeData;

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

        chromaticAberration = postProcessing.sharedProfile.components.Where(component => component is ChromaticAberration).First() as ChromaticAberration;
        currentAbberation = normalAbberation;
        chromaticAberration.intensity.value = currentAbberation;
    }

    public void DeactivateRandomComponent()
    {
        if (isInvincible)
        {
            return;
        }

        StartCoroutine(GiveBreathingRoom(breatherMaxSize));
        StartCoroutine(AdjustChromaticAbberationOnHit());
        cameraShaker.AddShakeEvent(shakeData);

        if (activeWeaponComponents.Count > 0)
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

    private IEnumerator AdjustChromaticAbberationOnHit(float timer = 0.33f)
    {
        var targetAbberation = currentAbberation + hitAbberation;
        var duration = timer / 2f;
        var passedTime = 0f;

        while(passedTime < duration)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(currentAbberation, targetAbberation, passedTime / duration);
            passedTime += Time.deltaTime;
            yield return null;
        }

        AdjustCurrentAbberation();

        while(passedTime > 0f)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(currentAbberation, targetAbberation, passedTime / duration);
            passedTime -= Time.deltaTime;
            yield return null;
        }

        chromaticAberration.intensity.value = currentAbberation;
    }

    private void AdjustCurrentAbberation()
    {
        int activeComponentAmount = activeWeaponComponents.Count + activeMovementComponents.Count;
        float healthPercentage = activeComponentAmount / 10f;

        currentAbberation = Mathf.Lerp(normalAbberation, maxAbberation, healthPercentage);
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
