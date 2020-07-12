using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : ShakeTransform
{
    public static CameraShaker instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
