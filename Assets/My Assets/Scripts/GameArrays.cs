using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArrays : MonoBehaviour {

    public GameObject[] enemyArray;
    public GameObject[] allyArray;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        enemyArray = GameObject.FindGameObjectsWithTag("EnemyTeam");
        allyArray = GameObject.FindGameObjectsWithTag("PlayerTeam");
    }
}
