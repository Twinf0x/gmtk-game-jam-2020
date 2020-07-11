using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthComponentDisplay componentDisplay;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Update()
    {
        ammoText.text = $"{(componentDisplay.displayedComponent as PlayerWeapon).bulletsLeft}";
    }
}
