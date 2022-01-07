using UnityEngine;

public class PlayerControl : MonoBehaviour


{
    private bool swatterPressed = false;
    private int countFrame = 0;
    private int waitFrames = 29;
    private BoxCollider swatterCollider;

    //world bounds, z-axis is different to keep object in frame 100%
    private float xPositive = 32.4f;
    private float xNegative = -32.4f;
    private float zPositive = 21.0f;
    private float zNegative = -24.3f;
    Vector3 objectPosition;


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
            Boundaries(gameObject);
            
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

    private void Boundaries(GameObject someObject)
    {
        objectPosition = someObject.transform.position;

        if (someObject.transform.position.x > xPositive)
        {
            someObject.transform.position = new Vector3(xPositive, someObject.transform.position.y, someObject.transform.position.z);
        }

        if (someObject.transform.position.x < xNegative)
        {
            someObject.transform.position = new Vector3(xNegative, someObject.transform.position.y, someObject.transform.position.z);
        }

        if (someObject.transform.position.z > zPositive)
        {
            someObject.transform.position = new Vector3(someObject.transform.position.x, someObject.transform.position.y, zPositive);
        }

        if (someObject.transform.position.z < zNegative)
        {
            someObject.transform.position = new Vector3(someObject.transform.position.x, someObject.transform.position.y, zNegative);
        }

    }

}
