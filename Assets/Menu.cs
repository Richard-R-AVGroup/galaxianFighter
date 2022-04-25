using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private Vector2 startPos;
    private Vector2 offPos;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        setActive(false);
        setStartPos(transform.position);
        setOffPos(transform.position);
        changeLocation(false);
        this.transform.position = offPos;
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

    //Setters
    public void setStartPos(Vector2 pos)
    {
        this.startPos = pos;
    }

    public void setOffPos(Vector2 pos)
    {
        this.offPos = pos + new Vector2(0, 6);
    }

    public void setActive(bool active)
    {
        this.active = active;
    }

    public void changeLocation(bool active)
    {
        if (!active)
            this.transform.position = Vector2.Lerp(getStartPos(), getOffPos(), 0.001f);
        else
            this.transform.position = Vector2.Lerp(getOffPos(), getStartPos(), 0.001f);
    }

}
