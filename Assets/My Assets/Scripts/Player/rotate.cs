using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

    private GameObject target;
    public int speed;

    // Update is called once per frame
    void Update()
    {
        findTarget();
        if (target)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            rotToTarget();
        }else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    void rotToTarget()
    {
        Vector3 dir = target.transform.position - transform.position;

        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle + -134);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
    }

    void findTarget()
    {
        GameObject[] gos;

        gos = GameObject.FindGameObjectsWithTag("EnemyTeam");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - transform.position;
            float dist = diff.sqrMagnitude;
            if (dist < distance)
            {
                closest = go;
                distance = dist;
            }
            target = closest;
        }
    }
}
