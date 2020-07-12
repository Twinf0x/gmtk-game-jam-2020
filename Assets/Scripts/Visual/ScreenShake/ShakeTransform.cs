using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShakeTransform : MonoBehaviour
{
    private List<ShakeEvent> shakeEvents = new List<ShakeEvent>();

    public void AddShakeEvent(ShakeTransformEventData data)
    {
        shakeEvents.Add(new ShakeEvent(data));
    }
    public void AddShakeEvent(float amplitude, float frequency, float duration, AnimationCurve blendOverLifetime)
    {
        ShakeTransformEventData data = ShakeTransformEventData.CreateInstance<ShakeTransformEventData>();
        data.Init(amplitude, frequency, duration, blendOverLifetime);

        AddShakeEvent(data);
    }

    private void LateUpdate()
    {
        Vector3 positionOffset = Vector3.zero;

        foreach (var shakeEvent in shakeEvents.Reverse<ShakeEvent>())
        {
            shakeEvent.Update();

            positionOffset += shakeEvent.value;

            if (!shakeEvent.IsAlive())
            {
                shakeEvents.Remove(shakeEvent);
            }
        }

        positionOffset.z = -10f; //So camera can display everything in 2d mode

        transform.localPosition = positionOffset;
    }
}
