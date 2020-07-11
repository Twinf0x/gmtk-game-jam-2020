using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color disabledColor;
    [SerializeField] private GameObject componentDisplayPrefab;
    [SerializeField] private Transform movementGroupParent;
    [SerializeField] private Transform weaponGroupParent;

    [Header("References")]
    [SerializeField] private PlayerHealthSystem healthSystem;

    private void Start()
    {
        Debug.Log("HUD Initialize");
        AddComponentDisplays();
    }

    private void AddComponentDisplays()
    {
        foreach(var component in healthSystem.activeMovementComponents)
        {
            AddComponentDisplay(component, movementGroupParent);
        }

        foreach(var component in healthSystem.activeWeaponComponents)
        {
            AddComponentDisplay(component, weaponGroupParent);
        }
    }

    private void AddComponentDisplay(PlayerHealthComponent component, Transform parent)
    {
        var displayObject = Instantiate(componentDisplayPrefab, parent);
        var display = displayObject.GetComponent<HealthComponentDisplay>();

        display.Initialize(this, component);
    }

    public Color GetDisabledColor()
    {
        return disabledColor;
    }
}
