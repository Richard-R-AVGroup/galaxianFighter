using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {

    private float speed;

	// Use this for initialization
	void Start () {
        speed = 0.15f;
        StartCoroutine(KillMe());
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.timeScale != 0)
            movement();
	}

    void movement()
    {
        transform.Translate(-Vector3.up * speed);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            Destroy(c.gameObject);
            Destroy(this.gameObject);
        }

        if (c.gameObject.tag == "EnemyBullet")
        {
            Destroy(c.gameObject);
            Destroy(this.gameObject);
        }
    }

    IEnumerator KillMe()
    {
        yield return new WaitForSeconds(1.8f);

        Destroy(this.gameObject);
    }
}
