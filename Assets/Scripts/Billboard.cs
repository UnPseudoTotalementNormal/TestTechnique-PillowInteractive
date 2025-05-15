using System;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Rotate the object to face the camera
/// </summary>
public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    private Transform billboardTransform;

    private void Awake()
    {
        mainCamera = Camera.main;
        billboardTransform = transform;
        Assert.IsNotNull(mainCamera, "something went wrong with main camera");
    }

    private void Update()
    {
        billboardTransform.LookAt(billboardTransform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}