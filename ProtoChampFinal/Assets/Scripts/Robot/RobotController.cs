using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    public GameObject core;

    public Slider energyBar;

    public float targetEnergy; // Energy calculated by robot configuration
    private float energy = 0;

    public GameObject levelCamera;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.gameObject.SetActive(false);
        levelCamera.SetActive(true);
        levelCamera.transform.parent = core.transform; 
    }

    public void SetEnergy(int energy)
    {
        targetEnergy = energy;
        energyBar.maxValue = targetEnergy * 1.5f;
    }

    public void AddEnergy(float add)
    {
        energy += add;
        if (energy > targetEnergy * 1.5f)
            energy = targetEnergy * 1.5f;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        energy -= 0.02f;
        if (energy < 0)
            energy = 0;
    }

    void Update()
    {
        float y = Input.GetAxis("Mouse X") * Time.deltaTime * 200.0f;
        float x = Input.GetAxis("Mouse Y") * Time.deltaTime * 200.0f;
        core.transform.Rotate(new Vector3(0, y, 0), Space.World);
        //Camera.main.transform.LookAt(core.transform);
        levelCamera.transform.Rotate(new Vector3(-x, 0, 0), Space.Self);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            energy += 0.8f;
        }
        if (energy > targetEnergy * 1.5f)
            energy = targetEnergy * 1.5f;
        energyBar.value = energy;

        if (CouldMove())
            core.transform.Translate(Vector3.up * Time.deltaTime * 5, Space.Self);
    }

    public bool CouldMove()
    {
        return (energy >= targetEnergy);
    }
}
