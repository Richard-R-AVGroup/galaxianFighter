using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).GetComponent<ParticleEmitter>().emit = true;
    }

    void FixedUpdate()
    {
        transform.Translate(0, 0.1f, 0);
        transform.Rotate(0, 0, 1.5f);
    }
}
