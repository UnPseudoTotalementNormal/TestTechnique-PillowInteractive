using System;
using UnityEngine;
using UnityEngine.Assertions;

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