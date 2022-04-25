using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour {

    public Text enemyCount;
    public Text allyCount;
    public Text healthCount;
    public Text killCount;
    public Canvas gameOver;
    public SpriteRenderer gameOverSprite;
    public Canvas youWon;
    public SpriteRenderer youWonSprite;
    public bool tutorial;

    void Awake()
    {
        gameOver.enabled = false;
        gameOverSprite.enabled = false;

        youWon.enabled = false;
        youWonSprite.enabled = false;
    }

	void Update () {

        GameObject[] enemyNum = GameObject.FindGameObjectsWithTag("EnemyTeam") as GameObject[];
        GameObject[] allyNum = GameObject.FindGameObjectsWithTag("PlayerTeam") as GameObject[];

        enemyCount.text = "Enemies: " + enemyNum.Length.ToString();
        allyCount.text = "Allies: " + (allyNum.Length-1).ToString();
        if (GameObject.Find("Player"))
        {
            healthCount.text = "Hp: " + GameObject.Find("Player").GetComponent<PlayerBehaviour>().health.ToString();
            killCount.text = "Kills:" + GameObject.Find("Player").GetComponent<PlayerBehaviour>().kills.ToString();

            if(enemyNum.Length == 0 && tutorial == false)
            {
                youWon.enabled = true;
                youWonSprite.enabled = true;
            }
        }
    }
}
