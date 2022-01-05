using UnityEngine;

public class PlayerControl : MonoBehaviour


{
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    private int health = 6; //2 units = 1 full heart
    private int score = 100;
    private bool swatterPressed = false;
    private int countFrame = 0;
    private int waitFrames = 29;
    private BoxCollider swatterCollider;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //initializion variables
        swatterCollider = GameObject.FindGameObjectWithTag("Swatter").GetComponent<BoxCollider>();

        //make cursor invisible
        Cursor.visible = false;
        transform.Rotate(-45, 0, 0);

        DrawHearts(health);

    }

    // Update is called once per frame
    void Update()
    {
        //allow control of player if game is active
        if (!gameOver)
        {
            //Mouse movement for player
            float mouseHorizontal = Input.GetAxis("Mouse X");
            float mouseVertical = Input.GetAxis("Mouse Y");
            transform.Translate(mouseHorizontal, 0, mouseVertical, Space.World);

            //Swatter controls
            if (Input.GetKey(KeyCode.Space))
            {
                SwatterPressed();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                transform.Rotate(-45, 0, 0);
                swatterPressed = false;
                //enable-disable swatter collider to prevent cheese
                swatterCollider.enabled = true;
                countFrame = 0;
            }
        }

        //countdown from 100 enemies. @ 0 enemies, game over
        if (score <= 0)
        {
            Debug.Log("Victory!");
            PauseGame();
            gameOver = true;
        }
    }

    public void Damage()
    {
        health = health - 2;
        DrawHearts(health);
    }

    public void Score()
    {
        score--;
        Debug.Log(score.ToString());
    }

    //Swatter Logic for controls
    private void SwatterPressed()
    {
        if (!swatterPressed)
        {
            transform.Rotate(45, 0, 0);
            swatterPressed = true;
        }
        else
        {
            countFrame++;
        }
        //disable collider after several frames, to allow collision detection before disable
        if (countFrame > waitFrames)
        {
            //enable-disable swatter collider to prevent cheese
            swatterCollider.enabled = false;
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void DrawHearts(float currentHealth)
    {
        int heartPosDefaultX = -28;
        int heartPosOffsetX = 5;

        GameObject[] heartObjects;
        heartObjects = GameObject.FindGameObjectsWithTag("Heart");

        //destroy existing hearts for switch below
        if (heartObjects != null)
        {
            foreach (GameObject heart in heartObjects)
            {
                Destroy(heart);
            }
        }

        switch (currentHealth)
        {
            case 6:
                Instantiate(fullHeartPrefab, new Vector3(heartPosDefaultX, 5, 21), fullHeartPrefab.transform.rotation);
                Instantiate(fullHeartPrefab, new Vector3(heartPosDefaultX + (heartPosOffsetX), 5, 21), fullHeartPrefab.transform.rotation);
                Instantiate(fullHeartPrefab, new Vector3(heartPosDefaultX + (heartPosOffsetX * 2), 5, 21), fullHeartPrefab.transform.rotation);
                break;
            case 5:
                //something
                break;
            case 4:
                Instantiate(fullHeartPrefab, new Vector3(heartPosDefaultX, 5, 21), fullHeartPrefab.transform.rotation);
                Instantiate(fullHeartPrefab, new Vector3(heartPosDefaultX + (heartPosOffsetX), 5, 21), fullHeartPrefab.transform.rotation);
                break;
            case 3:
                //something
                break;
            case 2:
                Instantiate(fullHeartPrefab, new Vector3(heartPosDefaultX, 5, 21), fullHeartPrefab.transform.rotation);
                break;
            case 1:
                //something
                break;
            default:
                Debug.Log("Game Over");
                gameOver = true;
                PauseGame();
                break;
        }

    }
}
