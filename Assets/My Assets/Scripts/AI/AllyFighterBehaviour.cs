using UnityEngine;
using System.Collections;

public class AllyFighterBehaviour : MonoBehaviour {

    private int health;
    private float thrust;
    private float turnSpeed;
    public float skill;
    float facingTarget;
    private float tarDist;

    private Rigidbody2D rb;
    private Vector3 forDir;
    private GameObject target;
    public GameObject[] gos;

    public GameObject gun1;
    public GameObject gun2;
    public GameObject bullet;

    private float timer = 5;

    // Use this for initialization
    void Start()
    {
        health = 5;
        thrust = 160f;
        turnSpeed = 100f;
        skill = Random.Range(0.0f, 4f);
        target = null;

        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (target)
        {
            movement();
            shooting();
            tarDist = Vector3.Distance(target.transform.position, this.transform.position);
            StartCoroutine(targetUpdate());
        }
        else
        {
            findTarget();
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    //Collision
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "EnemyBullet")
        {
            health--;
            Destroy(c.gameObject);
        }
    }
    //All Movement Stuff
    void movement()
    {
        forDir = this.transform.TransformDirection(Vector3.right);

        Vector3 dir = target.transform.position - transform.position;

        facingTarget = Vector2.Angle(dir, transform.right);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude/100) + turnSpeed * Time.deltaTime);

        rb.AddForce(forDir * thrust * Time.deltaTime);
    }
    //All Shooting Stuff
    void shooting()
    {
        if (target)
        {
            if (facingTarget >= 0 && facingTarget <= 4 + skill && timer >= 1f + skill && tarDist < 6 && tarDist > 0)
            {
                timer = 0;
                Instantiate(bullet, gun1.transform.position, gun1.transform.rotation);
                Instantiate(bullet, gun2.transform.position, gun2.transform.rotation);
            }
            else if (timer < 1f + skill)
            {
                timer += Time.deltaTime;
            }
        }
    }

    void findTarget()
    {
        gos = GameObject.Find("GameArrays").GetComponent<GameArrays>().enemyArray;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (go)
            {
                Vector3 diff = go.transform.position - transform.position;
                float dist = diff.sqrMagnitude;
                if (dist < distance)
                {
                    closest = go;
                    distance = dist;
                }
                target = closest;
                gos = null;
            }
        }
    }

    IEnumerator targetUpdate()
    {
        yield return new WaitForSeconds(3);

        target = null;
        findTarget();

        StartCoroutine(targetUpdate());
    }
}