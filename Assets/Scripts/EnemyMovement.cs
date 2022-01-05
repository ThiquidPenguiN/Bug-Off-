using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private GameObject playerObject;
    private GameObject otherEnemyObject;
    private GameObject gameDataObject;
    private Collider thisCollider;
    private float distance = 15;
    public static bool spawnDifference = false;

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
}
