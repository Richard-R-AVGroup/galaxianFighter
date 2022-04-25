using UnityEngine;
using System.Collections;

public class asteroidBehaviour : MonoBehaviour
{

    private Transform tran;
    private float rotDirection;
    private float speed;


    // Use this for initialization
    void Start()
    {
        rotDirection = Mathf.Ceil(Random.Range(0.0f, 2f));
        tran = this.gameObject.GetComponent<Transform>();
        speed = Random.Range(0f, 6f);

        tran.localScale =  new Vector3(0,0,Mathf.RoundToInt(Random.Range(1, 10)));

        tran.position = new Vector3(this.tran.position.x,this.tran.position.y,Random.Range(0,300));

        for (int i = 0; i < tran.position.z; i++)
        {
            this.tran.localScale = new Vector3(this.tran.localScale.x + 0.0083f, this.tran.localScale.y + 0.008333f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (rotDirection == 1)
        {
            transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
        }
        if (rotDirection == 2)
        {
            transform.Rotate(new Vector3(0, 0, -1), speed * Time.deltaTime);
        }

    }
}
