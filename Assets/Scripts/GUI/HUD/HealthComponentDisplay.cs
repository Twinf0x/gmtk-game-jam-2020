using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponentDisplay : MonoBehaviour
{
    [SerializeField] private Image iconDisplay;
    [SerializeField] private GameObject highlightOverlay;
    [HideInInspector] public PlayerHealthComponent displayedComponent;
    private HUDController controller;

    public void Initialize(HUDController controller, PlayerHealthComponent component)
    {
        this.controller = controller;
        displayedComponent = component;
        iconDisplay.sprite = displayedComponent.hudIcon;
        displayedComponent.onDeactivation.AddListener(IndicateDeactivation);
    }

    private void IndicateDeactivation()
    {
        iconDisplay.color = controller.GetDisabledColor();
    }

    public void ToggleHighlight()
    {
        highlightOverlay.SetActive(!highlightOverlay.activeInHierarchy);
    }
}
