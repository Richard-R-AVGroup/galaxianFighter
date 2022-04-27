using UnityEngine;
using System.Collections;

public class AllyFrigBehaviour : MonoBehaviour {

    //Personal float and int Vars
    private int health;
    private float thrust;
    private float turnSpeed;
    private float trackingspeed;
    public float skill;
    float facingTarget;
    float gun1Facing;
    float gun2Facing;
    private float tarDist1;
    private float tarDist2;

    //Targeting and Rigidbody
    private Rigidbody2D rb;
    private Vector3 forDir;
    private Vector3 gun1Dir;
    private Vector3 gun2Dir;
    private Quaternion ogRot;

    //Associated GameObjects
    public GameObject gun1;
    public GameObject gun2;
    public GameObject bullet;
    private GameObject target1;
    private GameObject target2;
    private GameObject moveTar;
    public GameObject[] gos;

    //Gun Timers
    private float gun1Timer = 5;
    private float gun2Timer = 5;

    //Behaviour
    private bool attack;
    //private bool flee;            //TODO
    private bool rearrange;

    // Use this for initialization
    void Start()
    {
        health = 40;
        thrust = 60f;
        turnSpeed = 30f;
        skill = Random.Range(0.0f, 4f);
        attack = true;
        //flee = false;                         //TODO
        rearrange = false;
        trackingspeed = 30f;
        findMoveTarget();
        rb = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame

    void Update()
    {
        //movement AI
        if (moveTar)
        {
            movement();
            shooting();
            gun1Turret();
            gun2Turret();
            behaviour();
            if (target1)
                tarDist1 = Vector3.Distance(target1.transform.position, this.transform.position);
            else
            {
                gun1.transform.rotation = Quaternion.RotateTowards(gun1.transform.rotation, ogRot, (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 100) + turnSpeed * Time.deltaTime);
                findTarget1(gun1);
            }
            if (target2)
                tarDist2 = Vector3.Distance(target2.transform.position, this.transform.position);
            else
            {
                gun2.transform.rotation = Quaternion.RotateTowards(gun2.transform.rotation, ogRot, (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 100) + turnSpeed * Time.deltaTime);
                findTarget2(gun2);
            }
            StartCoroutine(moveTarget());
            StartCoroutine(turretTarget());
        }
        else
        {
            findMoveTarget();
        }

        //Destruction
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

        if (c.gameObject.tag == "PlayerTeam" && c.gameObject.name == "AllyFrigate")
        {
            rearrange = true;
            c.GetComponent<AllyFrigBehaviour>().rearrange = false;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "PlayerTeam" && c.gameObject.name != "AllyFighter")
        {
            rearrange = false;
        }
    }

    //All Movement Stuff
    void movement()
    {
        if (attack == true)
        {
            forDir = this.transform.TransformDirection(Vector3.right);

            Vector3 dir = moveTar.transform.position - transform.position;

            facingTarget = Vector2.Angle(dir, transform.right);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            if (rearrange == true)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation * Quaternion.Euler(Vector3.forward *50), (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 100) + turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 100) + turnSpeed * Time.deltaTime);
            }

            rb.AddForce(forDir * thrust * Time.deltaTime);
        }
/*
        if (flee == true)
        {
            attack = false;
            forDir = this.transform.TransformDirection(Vector3.right);

            Vector3 dir = transform.position - moveTar.transform.position;

            facingTarget = Vector2.Angle(dir, transform.right);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, -angle);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, (this.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 100) + turnSpeed * Time.deltaTime);

            rb.AddForce(forDir * thrust * Time.deltaTime);

            if (tarDist1 > 14)
            {
                flee = false;
                findMoveTarget();
                attack = true;
            }
        }
        */
    }

    //Behaviour Flags
    void behaviour()
    {
        if (health <= 10 && attack == true)
        {
            attack = false;
            //flee = true;                      //TODO
        }
    }

    //All Shooting Stuff
    void shooting()
    {
        //Gun1
        if (gun1Facing >= 0 && gun1Facing <= 4 + skill && gun1Timer >= 1f + skill)
        {
            if (tarDist1 < 10)
            {
                gun1Timer = 0;
                Instantiate(bullet, gun1.transform.position, gun1.transform.rotation * Quaternion.Euler(0, 0, 90));
            }
        }
        else if(gun1Timer < 1f + skill)
        {
            gun1Timer += Time.deltaTime;
        }
        //Gun2
        if (gun2Facing >= 0 && gun2Facing <= 4 + skill && gun2Timer >= 1f + skill)
        {
            if (tarDist2 < 10)
            {
                gun2Timer = 0;
                Instantiate(bullet, gun2.transform.position, gun2.transform.rotation * Quaternion.Euler(0, 0, 90));
            }
        }
        else if (gun2Timer < 1f + skill)
        {
            gun2Timer += Time.deltaTime;
        }
    }

    void gun1Turret()
    {
        if (target1 && tarDist1 <= 10)
        {
            gun1Dir = gun1.transform.TransformDirection(Vector3.right);

            Vector3 dir = target1.transform.position - gun1.transform.position;

            gun1Facing = Vector2.Angle(dir, gun1.transform.right);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            gun1.transform.rotation = Quaternion.RotateTowards(gun1.transform.rotation, rotation, 1 + trackingspeed * Time.deltaTime);
        }
        else
        {
            target1 = null;
        }
    }

    void gun2Turret()
    {
        if (target2 && tarDist2 <= 10)
        {
            gun2Dir = gun2.transform.TransformDirection(Vector3.right);
            Vector3 dir = target2.transform.position - gun2.transform.position;

            gun2Facing = Vector2.Angle(dir, gun2.transform.right);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            gun2.transform.rotation = Quaternion.RotateTowards(gun2.transform.rotation, rotation, trackingspeed * Time.deltaTime);
        }
        else
        {
            target2 = null;
        }
    }

    void findMoveTarget()
    {
        GameObject closestMove = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        gos = GameObject.Find("GameArrays").GetComponent<GameArrays>().enemyArray;
        foreach (GameObject go in gos)
        {
            if (go)
            {
                Vector3 diff = go.transform.position - transform.position;
                float dist = diff.sqrMagnitude;
                if (dist < distance)
                {
                    distance = dist;
                    closestMove = go;
                }
                moveTar = closestMove;
                gos = null;
            }
        }
    }

    void findTarget1(GameObject obj)
    {
        GameObject closestMove = null;
        float distance = 50f;
        Vector3 position = obj.transform.position;
        gos = GameObject.Find("GameArrays").GetComponent<GameArrays>().enemyArray;
        foreach (GameObject go in gos)
        {
            if (go)
            {
                Vector3 diff = go.transform.position - obj.transform.position;
                float dist = diff.sqrMagnitude;
                if (dist < distance)
                {
                    distance = dist;
                    closestMove = go;
                }
                else
                {
                    target1 = null;
                }
                target1 = closestMove;
                gos = null;
            }
        }
    }

    void findTarget2(GameObject obj)
    {
        GameObject closestMove = null;
        float distance = 50f;
        Vector3 position = obj.transform.position;
        gos = GameObject.Find("GameArrays").GetComponent<GameArrays>().enemyArray;
        foreach (GameObject go in gos)
        {
            if (go)
            {
                Vector3 diff = go.transform.position - obj.transform.position;
                float dist = diff.sqrMagnitude;
                if (dist < distance)
                {
                    distance = dist;
                    closestMove = go;
                }
                else
                {
                    target2 = null;
                }
                target2 = closestMove;
                gos = null;
            }
        }
    }

    IEnumerator moveTarget()
    {
        yield return new WaitForSeconds(6);
        findMoveTarget();
        StartCoroutine(moveTarget());
    }

    IEnumerator turretTarget()
    {
        yield return new WaitForSeconds(3);
        findTarget1(gun1);
        findTarget2(gun2);
        StartCoroutine(turretTarget());
    }
}
