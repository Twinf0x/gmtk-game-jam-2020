using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakeEvent
{
    private float duration;
    private float timeRemaining;
    private Vector3 noiseOffset;
    private ShakeTransformEventData data;

    public Vector3 value;

    public ShakeEvent(ShakeTransformEventData data)
    {
        this.data = data;

        duration = data.duration;
        timeRemaining = duration;

        float scramble = 32.0f;

        noiseOffset.x = Random.Range(0.0f, scramble);
        noiseOffset.y = Random.Range(0.0f, scramble);
        noiseOffset.z = 0f;
    }

    public void Update()
    {
        timeRemaining -= Time.deltaTime;

        float noiseOffsetDelta = Time.deltaTime * data.frequency;

        noiseOffset.x += noiseOffsetDelta;
        noiseOffset.y += noiseOffsetDelta;

        value.x = Mathf.PerlinNoise(noiseOffset.x, 0.0f);
        value.y = Mathf.PerlinNoise(noiseOffset.y, 1.0f);
        value.z = 0f;

        value -= Vector3.one * 0.5f;

        value *= data.amplitude;

        float agePercent = 1.0f - (timeRemaining / duration);
        value *= data.blendOverLifetime.Evaluate(agePercent);
    }

    public bool IsAlive()
    {
        return timeRemaining > 0.0f;
    }
}
