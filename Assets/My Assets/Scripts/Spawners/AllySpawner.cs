using UnityEngine;
using System.Collections;

public class AllySpawner : MonoBehaviour
{

    public GameObject fighter;
    public GameObject cruiser;
    private int spawning;
    private float spawn;

    void Start()
    {
        spawning = 0;
        spawn = Random.Range(0,100);
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] objNumber = GameObject.FindGameObjectsWithTag("PlayerTeam") as GameObject[];

        if (objNumber.Length < 10 && spawning == 0)
        {
            StartCoroutine(Spawn());
            spawn = Random.Range(0, 100);
            spawning = 1;
        }
    }

    IEnumerator Spawn()
    {
        if (spawn < 90)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 10f));

            Instantiate(fighter, this.transform.position, this.transform.rotation);
            spawning = 0;
        }

        if (spawn >= 90)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 10f));
            Instantiate(cruiser, this.transform.position, this.transform.rotation);
            spawning = 0;
        }
    }
}