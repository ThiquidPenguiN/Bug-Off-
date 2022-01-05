using UnityEngine;

public class PlayerControl : MonoBehaviour


{
    private float health = 3.0f;
    private int score = 100;
    private bool swatterPressed = false;
    private bool isReleased = true;
    private int countFrame = 0;
    private int waitFrames = 29;
    private BoxCollider swatterCollider;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //initializion variables
        swatterCollider = GameObject.FindGameObjectWithTag("Swatter").GetComponent<BoxCollider>();
        Debug.Log(health.ToString());

        //make cursor invisible
        Cursor.visible = false;
        transform.Rotate(-45, 0, 0);
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


        //if health falls below 3, gameover
        if (health <= 0)
        {
            Debug.Log("Game Over");
            PauseGame();
            gameOver = true;
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
        health--;
        Debug.Log(health.ToString());
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
            //enable-disable swatter collider to prevent cheese
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


}
