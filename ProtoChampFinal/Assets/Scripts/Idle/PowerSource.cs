using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    // Start is called before the first frame update
    public float powerFromSun; 
    public float powerFromPlayer;

    [SerializeField] float sunPowerRate =1f;
    [SerializeField] float playerPowerRate = 3f;
    private GameObject player;
    void Start()
    {
        powerFromSun = sunPowerRate;
        powerFromPlayer = 3f;
        player= GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       powerFromPlayer = playerPowerRate *player.GetComponent<Player>().isPedaling; // 
    }
}
