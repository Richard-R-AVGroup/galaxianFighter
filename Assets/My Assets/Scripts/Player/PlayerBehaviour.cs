using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private Rigidbody2D rb;

    public GameObject Bullet1;
    public GameObject gun1;
    public GameObject gun2;
    public GameObject target;

    public ParticleSystem afterburner;

    private float thrust;
    private float turnSpeed;
    public float timer;
    private Vector3 forwardDir;
    public int health;
    public int kills;

	// Use this for initialization
	void Start () {

        rb = this.gameObject.GetComponent<Rigidbody2D>();

        thrust = 170f;
        turnSpeed = 100f;
        health = 5;
        kills = 0;
        timer = 1;
        
	}

    void Update()
    {
        //Destruction
        if(health <= 0)
        {
            GameObject.Find("Counters").GetComponent<Counter>().gameOver.enabled = true;
            GameObject.Find("Counters").GetComponent<Counter>().gameOverSprite.enabled = true;
            Destroy(this.gameObject);
        }

        //Text Counters
    }
	
	void FixedUpdate () {
        Controller();
	}

    void Controller()
    {
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.right * thrust * Time.deltaTime);
            //this.afterburner.Play();
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.rb.AddForce(-transform.right * thrust * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * ((-this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 10) + turnSpeed * Time.deltaTime));
            Debug.Log("Turning Speed: " + ((-this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 10) + turnSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * ((-this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 10) + turnSpeed * Time.deltaTime));
            Debug.Log("Turning Speed: " + ((-this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 10) + turnSpeed * Time.deltaTime));
        }

        

        //Shooting
        if (Input.GetKey(KeyCode.Space) && timer >= 1.0f)
        {
            timer = 0;
            Instantiate(Bullet1, gun1.transform.position, gun1.transform.rotation);
            Instantiate(Bullet1, gun2.transform.position, gun2.transform.rotation);
        }
        else if (timer < 1f)
        {
            timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "EnemyBullet")
        {
            health--;
            Destroy(c.gameObject);
        }
    }
}
