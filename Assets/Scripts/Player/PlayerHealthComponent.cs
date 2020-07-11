using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerHealthComponent : MonoBehaviour
{
    public Sprite hudIcon;
    public UnityEvent onDeactivation;
}
