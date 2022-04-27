using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorial : MonoBehaviour {

    public Text objText;
    private int tutStage;
    //private int multObj;
    private bool firstKey;
    private bool secKey;
    private bool clear;
    public GameObject target;
    public GameObject enemy;

	// Use this for initialization
	void Start () {
        tutStage = 0;
        firstKey = false;
        secKey = false;
        //multObj = 0;
        clear = true;
	}

    // Update is called once per frame
    void Update()
    {

        if (tutStage == 0)
        {
            objText.text = "Press the W key to move forward.";
            if (Input.GetKeyDown(KeyCode.W))
            {
                tutStage++;
            }
        }

        if (tutStage == 1)
        {
            objText.text = "You can also press the S to slow down and reverse.";
            if (Input.GetKeyDown(KeyCode.S))
            {
                tutStage++;
            }
        }

        if (tutStage == 2)
        {
            objText.text = "Press the A key to turn left, and the D key to turn right.";
            if (Input.GetKeyDown(KeyCode.A) && firstKey == false)
            {
                firstKey = true;
            }
            if (Input.GetKeyDown(KeyCode.D) && secKey == false)
            {
                secKey = true;
            }
            if (firstKey == true && secKey == true)
            {
                firstKey = false;
                secKey = false;
                tutStage++;
            }
        }

        if (tutStage == 3)
        {
            objText.text = "Press Space to fire your lasers.";
            if (Input.GetKeyUp(KeyCode.Space))
            {
                tutStage++;
                objText.text = "Your lasers will take about one second to recharge before you can fire again.";
                StartCoroutine(timedAdv());
            }
        }

        if (tutStage == 5)
        {
            GameObject drone;
            drone = Instantiate(enemy, GameObject.Find("Player").transform.position + new Vector3(6, Random.Range(-4, 4), 0), new Quaternion(0, 0, 180f, 0)) as GameObject;

            objText.text = "We are spawning a drone for u to destroy, take it out.";
            target = drone;
            target.gameObject.GetComponent<EnemyFighterBehaviour>().tut = true;

            tutStage++;
        }

        if (!target && tutStage == 6)
        {
            StartCoroutine(timedAdv());
            objText.text = "Good Job!";
            tutStage++;
        }

        if (tutStage == 8)
        {
            objText.text = "Watch Out! Looks like an enemy scout has found you! Use what you have learned to kill him before he kills you!";
            GameObject enemyScout;
            enemyScout = Instantiate(enemy, GameObject.Find("Player").transform.position + new Vector3(8, Random.Range(-9, 9), 0), new Quaternion(0, 0, 180f, 0)) as GameObject;
            target = enemyScout;
            StartCoroutine(timedSkill(target));
            StartCoroutine(timedAdv());
            tutStage++;
        }

        if (tutStage == 10)
        {
            if (clear == true)
            {
                objText.text = "";
                clear = false;
            }
            

            if (GameObject.Find("Player").GetComponent<PlayerBehaviour>().health <= 0)
            {
                objText.text = "Oh No! Looks like he got you. You can either try again, or take your chances in Story Mode.";
            }

            if (!target)
            {
                objText.text = "Awesome work! You'll be an Ace in no time. Now lets put your training to the test.";
                GameObject.Find("Counters").GetComponent<Counter>().youWon.enabled = true;
                GameObject.Find("Counters").GetComponent<Counter>().youWonSprite.enabled = true;
            }
        }
        Debug.Log(tutStage);
    }

    IEnumerator timedAdv()
    {
        yield return new WaitForSeconds(5);
        
        tutStage++;
    }

    IEnumerator timedSkill(GameObject target)
    {
        yield return new WaitForSeconds(1);

        target.GetComponent<EnemyFighterBehaviour>().skill = 4;
    }
}
