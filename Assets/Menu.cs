using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private Vector2 startPos;
    private Vector2 onPos;
    private Vector2 offPos;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
        
    }

    //Getters
    public Vector2 getStartPos()
    {
        return startPos;
    }

    public Vector2 getOffPos()
    {
        return offPos;
    }

    public bool getActive()
    {
        return active;
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void setOffPos()
    {
        this.offPos = GetComponentInParent<Transform>().position + new Vector3(0, 6, 0);
    }

    public void setOnPos()
    {
        this.onPos = GetComponentInParent<Transform>().position;
    }

    public void setActive(bool active)
    {
        this.active = active;
    }

    //
    public void changeLocation(bool active)
    {
        if (!active)
            this.transform.position = Vector2.Lerp(getStartPos(), getOffPos(), 0.01f * Time.deltaTime);
        else
            this.transform.position = Vector2.Lerp(getOffPos(), getStartPos(), 0.01f * Time.deltaTime);
    }


}
