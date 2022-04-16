using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControls : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    private GUIStyle guiStyle = new GUIStyle();

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();

        guiStyle.fontSize = 50;
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Gyroscope attitude : " + gyro.attitude, guiStyle);
        GUILayout.Label("Gyroscope gravity : " + gyro.gravity, guiStyle);
        GUILayout.Label("Gyroscope rotationRate : " + gyro.rotationRate, guiStyle);
        GUILayout.Label("Gyroscope rotationRateUnbiased : " + gyro.rotationRateUnbiased, guiStyle);
        GUILayout.Label("Gyroscope updateInterval : " + gyro.updateInterval, guiStyle);
        GUILayout.Label("Gyroscope userAcceleration : " + gyro.userAcceleration, guiStyle);
    }
}
