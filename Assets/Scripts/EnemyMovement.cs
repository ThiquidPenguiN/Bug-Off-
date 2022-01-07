using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private GameObject playerObject;
    private GameObject otherEnemyObject;
    private GameObject gameDataObject;
    private Collider thisCollider;
    private float distance = 15;
    public static bool spawnDifference = false;

    //world bounds, z-axis is different to keep object in frame 100%
    private float xPositive = 32.4f;
    private float xNegative = -32.4f;
    private float zPositive = 24.3f;
    private float zNegative = -24.3f;
    Vector3 objectPosition;

    void Start()
    {
        //initializion variables
        playerObject = GameObject.Find("Player");
        gameDataObject = GameObject.Find("GameData");
        otherEnemyObject = GameObject.Find("Enemy");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Chase player
        transform.LookAt(playerObject.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, Time.deltaTime * 10);
        Boundaries(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {


        //needed to control number of spawned objects
        spawnDifference = true;
        while (spawnDifference)
        {
            if (SpawnManager.spawnCorrected)
            {
                spawnDifference = false;
            }
        }

        //detect player collision
        if (other.tag == "Player")
        {
            if (gameDataObject.GetComponent<GameDataScript>() != null)
            {
                DisableCollider();
                gameDataObject.GetComponent<GameDataScript>().Damage();
                Destroy(this.gameObject);
            }

        }
        else if (other.tag == "Swatter")
        {
            //detect contact with swatter object

            if (gameDataObject.GetComponent<GameDataScript>() != null)
            {
                DisableCollider();
                gameDataObject.GetComponent<GameDataScript>().Score();
                Destroy(this.gameObject);

            }
        }

    }

    //keep distance from other enemey objects
    void KeepDistanceFromEnemy()
    {
        otherEnemyObject = GameObject.Find("Enemy");
        if (otherEnemyObject)
        {
            if (SpawnManager.currentSpawned > 1)
            {
                Vector3 otherEnemyPosition = otherEnemyObject.transform.position;
                transform.position = (transform.position - otherEnemyPosition).normalized * distance + otherEnemyPosition;
            }
        }

    }

    void DisableCollider()
    {
        thisCollider = GetComponent<Collider>();
        thisCollider.enabled = false;
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
