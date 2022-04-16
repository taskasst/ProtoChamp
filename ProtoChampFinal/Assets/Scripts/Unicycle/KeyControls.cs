using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControls : MonoBehaviour
{
    // Key press variables
    private KeyCode[] sequence = new KeyCode[]
    {
        KeyCode.W,
        KeyCode.D,
        KeyCode.X,
        KeyCode.Z,
        KeyCode.A
    };

    private int sequenceIndex = 0;
    private bool sequenceStarted = false;

    // Movement variables
    public float currentSpeed = 0f;
    public float maxSpeed = 10f;
    private float minSpeed;
    public float deceleration = 0.995f;
    public float speedGrowth = 1f;
    public float maxTimeBetween = 3f;
    private float currTime = 0f;

    // Controller variables
    public float contSpeedGrowth = 1f;
    private bool rotating = false;
    private float prevAngle = 0.0f;
    private int frameCount = 0;

    private GameManager gameManager;

    private void Start()
    {
        minSpeed = -maxSpeed;
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        float addSpeed = 0f;

        // Keyboard controls
        if (!sequenceStarted)
        {
            // Check which key has been pressed if sequence hasn't started
            if (Input.GetKeyDown(KeyCode.W))
            {
                sequenceStarted = true;
                sequenceIndex = 0;
                Debug.Log("W Start");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                sequenceStarted = true;
                sequenceIndex = 1;
                Debug.Log("D Start");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                sequenceStarted = true;
                sequenceIndex = 2;
                Debug.Log("X Start");
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                sequenceStarted = true;
                sequenceIndex = 3;
                Debug.Log("Z Start");
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                sequenceStarted = true;
                sequenceIndex = 4;
                Debug.Log("A Start");
            }
        }
        else
        {
            // Make sure they don't take too long to continue the rotation
            currTime += Time.deltaTime;

            // Check if the next key has been pressed if the sequence has started
            if (Input.GetKeyDown(sequence[GetMod(sequenceIndex + 1, 5)]))
            {
                addSpeed = (maxTimeBetween - currTime) / maxTimeBetween * speedGrowth;
                currTime = 0f;
                // Sequence forward
                Debug.Log(sequence[GetMod(sequenceIndex + 1, 5)] + " Forward");
                if (++sequenceIndex == sequence.Length)
                {
                    sequenceIndex = 0;
                    // sequence typed
                }
            }
            else if (Input.GetKeyDown(sequence[GetMod(sequenceIndex - 1, 5)]))
            {
                addSpeed = -(maxTimeBetween - currTime) / maxTimeBetween * speedGrowth;
                currTime = 0f;
                // Sequence backward
                Debug.Log(sequence[GetMod(sequenceIndex - 1, 5)] + " Backward");
                if (--sequenceIndex < 0)
                {
                    sequenceIndex = sequence.Length - 1;
                    // sequence typed
                }
            }
            else if (Input.anyKeyDown || currTime > maxTimeBetween)
            {
                currTime = 0f;
                // Sequence has messed up
                Debug.Log("Messed up");
                sequenceIndex = 0;
                sequenceStarted = false;
            }
        }

        // Controller controls
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (rotating)
        {
            if (Mathf.Abs(horz) < 0.99 && Mathf.Abs(vert) < 0.99)
            {
                rotating = false;
                return;
            }

            float angle = Mathf.Atan2(vert, horz) * Mathf.Rad2Deg;
            Debug.Log(angle - prevAngle);
            if (Mathf.Abs(angle - prevAngle) < 20f)
            {
                addSpeed = (angle - prevAngle) * -contSpeedGrowth;
            }
            prevAngle = angle;
        }
        else
        {
            if (Mathf.Abs(horz) > 0.99 || Mathf.Abs(vert) > 0.99)
            {
                prevAngle = Mathf.Atan2(vert, horz) * Mathf.Rad2Deg;
                rotating = true;
            }
        }
        

        // Move if the game has started
        if (gameManager.myState == GameManager.gameState.gameStart)
        {
            Movement(addSpeed, false);
        }
    }

    private int GetMod(int k, int n)
    {
        return ((k %= n) < 0) ? k + n : k;
    }

    public void Movement(float add, bool airConsole)
    {
        // Accelerate
        if (sequenceStarted || airConsole || rotating)
            currentSpeed += add;

        // Don't go over or under max/min speed
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        else if (currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
        }

        // Move
        //transform.Translate(transform.forward * currentSpeed * Time.deltaTime);

        // Natural deceleration
        currentSpeed *= deceleration;

        this.GetComponent<PedalController>().SetSpeed(currentSpeed);
        GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>().SetSpeed(currentSpeed);
    }
}
