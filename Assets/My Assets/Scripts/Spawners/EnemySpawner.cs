using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject obj;
    private int spawning;

    void Start()
    {
        spawning = 0;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] objNumber = GameObject.FindGameObjectsWithTag("EnemyTeam") as GameObject[];

        if (objNumber.Length < 10 && spawning == 0)
        {
            StartCoroutine(Spawn());
            spawning = 1;
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 10f));

        Instantiate(obj, this.transform.position, this.transform.rotation);
        spawning = 0;
    }
}