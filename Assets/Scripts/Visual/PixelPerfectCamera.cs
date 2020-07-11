using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

    Camera cam;
    public float pixelsPerUnit;
    public float pPUScale;

    [SerializeField] private float referenceScreenHeight;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
        cam.orthographicSize = (Screen.height / (pPUScale * pixelsPerUnit));
        if (referenceScreenHeight > 0) {
            cam.orthographicSize *= (referenceScreenHeight / Screen.height);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}