using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Smooth Follow")]
public class SmoothFollow2D : MonoBehaviour
{

    public Transform Target;

    public float MovementDamping = 0.2f;

    void Update()
    {
        if (!Target) { return; }

        Target = Target.transform;

        this.GetComponent<Camera>().orthographicSize = 3 + (Target.GetComponent<Rigidbody2D>().velocity.magnitude/2 * 0.5f);

        Vector3 currentPosition = this.transform.position;

        float cameraZ = currentPosition.z;

        currentPosition = Vector3.Slerp(currentPosition, Target.position, MovementDamping  );
        currentPosition.z = cameraZ;
        this.transform.position = currentPosition;
    }
}
