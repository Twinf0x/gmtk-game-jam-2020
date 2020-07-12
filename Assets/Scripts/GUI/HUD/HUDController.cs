using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [SerializeField] private PlayerController playerController;

    private List<HealthComponentDisplay> componentDisplays;
    private HealthComponentDisplay highlightedDisplay = null;

    private void Start()
    {
        componentDisplays = new List<HealthComponentDisplay>();
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

        componentDisplays.Add(display);
    }

    public void AdjustHighlighting()
    {
        if(highlightedDisplay != null)
            highlightedDisplay.ToggleHighlight();

        highlightedDisplay = componentDisplays.Where(display => display.displayedComponent == playerController.currentWeapon).First();

        if (highlightedDisplay != null)
            highlightedDisplay.ToggleHighlight();
    }

    public Color GetDisabledColor()
    {
        return disabledColor;
    }
}