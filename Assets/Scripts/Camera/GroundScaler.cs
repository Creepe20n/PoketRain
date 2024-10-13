using FastThings.FastCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class GroundScaler : MonoBehaviour
{
    //GPT Code
    public SpriteRenderer groundSprite;
    private Camera mainCamera;
    float groundX;
    private void Update() {
        if(mainCamera == null || groundSprite == null)
            return;

        // Get the width of the camera's viewport
        float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;

        // Calculate the scale factor
        float scaleFactor = cameraWidth / groundX;

        transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
    }
    void Start() {
        mainCamera = Camera.main;
        groundX = groundSprite.bounds.size.x;
    }
}
