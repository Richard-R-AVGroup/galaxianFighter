using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundPopulation : MonoBehaviour
{
    [SerializeField]
    private bool generateAsteroids;
    [SerializeField, Range(10, 50)]
    private int asteroidAmt;
    [SerializeField, Range(20, 50)]
    private int width;
    [SerializeField, Range(20, 50)]
    private int height;

    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        if (generateAsteroids == true)
        {
            for (int i = 0; i < asteroidAmt; i++)
            {
                Vector2 loc = new Vector2(this.transform.position.x + Random.Range(-width, width),          //width range
                                            this.transform.position.y + Random.Range(-height, height));     //height range
                GameObject go = Instantiate(asteroid, loc, asteroid.transform.rotation);
            }
        }
    }

    public void setAsteroids(bool active)
    {
        generateAsteroids = active;
    }
}
