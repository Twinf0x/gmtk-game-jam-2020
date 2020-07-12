using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShakeEventData", menuName = "")]
public class ShakeTransformEventData : ScriptableObject
{
    public float amplitude = 1f;
    public float frequency = 1f;
    public float duration = 1f;

    public AnimationCurve blendOverLifetime = new AnimationCurve(
        new Keyframe(0.0f, 0.0f, Mathf.Deg2Rad * 0.0f, Mathf.Deg2Rad * 720.0f),
        new Keyframe(0.2f, 1.0f),
        new Keyframe(1.0f, 0.0f));

    public void Init(float amplitude, float frequency, float duration, AnimationCurve blendOverLifetime)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.duration = duration;

        this.blendOverLifetime = blendOverLifetime;
    }
}
