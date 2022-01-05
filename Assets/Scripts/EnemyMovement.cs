using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private GameObject playerObject;
    private GameObject otherEnemyObject;
    private GameObject playerControlObject;
    private Collider thisCollider;
    private float distance = 15;
    public static bool spawnDifference = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerControlObject = GameObject.Find("PlayerParent");
        otherEnemyObject = GameObject.Find("Enemy");

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //Chase player
        transform.LookAt(playerObject.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, Time.deltaTime  * 10);   
    }

    private void OnTriggerEnter(Collider other)
    {
        thisCollider = GetComponent<Collider>();
        thisCollider.enabled = false;

        spawnDifference = true;
        while (spawnDifference)
        {
            if (SpawnManager.spawnCorrected)
            {
                spawnDifference = false;
            }
        }

        if (other.tag == "Player")
        {
            if (playerControlObject.GetComponent<PlayerControl>() != null)
            {
                playerControlObject.GetComponent<PlayerControl>().Damage();
                Destroy(this.gameObject);
            }

        }

        if (other.tag == "Swatter")
        {
            if (playerControlObject.GetComponent<PlayerControl>() != null)
            {
                playerControlObject.GetComponent<PlayerControl>().Score();
            }
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {

    }

    //needed to control number of spawned objects
    void OnCollisionEnter(Collision collision)
    {

    }

    void KeepDistanceFromEnemy()
    {
        otherEnemyObject = GameObject.Find("Enemy");
        //keep distance from other enemey objects
        if (otherEnemyObject)
        {
            if (SpawnManager.currentSpawned > 1)
            {
                Vector3 otherEnemyPosition = otherEnemyObject.transform.position;
                transform.position = (transform.position - otherEnemyPosition).normalized * distance + otherEnemyPosition;
            }
        }

    }
}
