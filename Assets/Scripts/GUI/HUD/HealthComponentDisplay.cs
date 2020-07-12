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
        StartCoroutine(AnimateDeactivationColor());
        StartCoroutine(AnimateDeactivationSize());
    }

    private IEnumerator AnimateDeactivationColor()
    {
        var defaultColor = iconDisplay.color;
        int blinks = 5;

        while(blinks > 0)
        {
            iconDisplay.color = controller.GetDisabledColor();
            yield return new WaitForSeconds(0.1f);

            iconDisplay.color = defaultColor;
            yield return new WaitForSeconds(0.1f);

            blinks--;
        }

        iconDisplay.color = controller.GetDisabledColor();
    }

    private IEnumerator AnimateDeactivationSize()
    {
        float timer = 0.25f;
        float tempTimer = 0f;

        var defaultScale = transform.localScale;
        var maxScale = defaultScale * 1.2f;

        while(tempTimer < timer)
        {
            transform.localScale = Vector3.Lerp(defaultScale, maxScale, tempTimer / timer);
            tempTimer += Time.deltaTime;
            yield return null;
        }

        while(tempTimer > 0f)
        {
            transform.localScale = Vector3.Lerp(defaultScale, maxScale, tempTimer / timer);
            tempTimer -= Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void ToggleHighlight()
    {
        highlightOverlay.SetActive(!highlightOverlay.activeInHierarchy);
    }
}
