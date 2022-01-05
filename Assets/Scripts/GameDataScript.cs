using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{

    private int health = 6; //2 units = 1 full heart
    private int score = 100;
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public static bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawHearts(health);

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
    void PauseGame()
    {
        Time.timeScale = 0;
    }


}
