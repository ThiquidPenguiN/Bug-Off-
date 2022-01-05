using UnityEngine;

public class PlayerControl : MonoBehaviour


{
    private bool swatterPressed = false;
    private int countFrame = 0;
    private int waitFrames = 29;
    private BoxCollider swatterCollider;


    // Start is called before the first frame update
    void Start()
    {
        //initializion variables
        swatterCollider = GameObject.FindGameObjectWithTag("Swatter").GetComponent<BoxCollider>();

        //make cursor invisible
        Cursor.visible = false;
        transform.Rotate(-45, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {
        //allow control of player if game is active
        if (!GameDataScript.gameOver)
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



}
