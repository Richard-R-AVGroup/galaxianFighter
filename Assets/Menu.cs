using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private Vector2 onPos;
    private Vector2 offPos;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        onPos = GetComponentInParent<Transform>().position;
        offPos = GetComponentInParent<Transform>().position + new Vector3(0, 6, 0);

        
    }

    public void changeLocation(bool active)
    {
        if (!active)
            this.transform.position = Vector2.Lerp(onPos, offPos, 0.01f * Time.deltaTime);
        else
            this.transform.position = Vector2.Lerp(offPos, onPos, 0.01f * Time.deltaTime);
    }


}
