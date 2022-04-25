using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    [SerializeField] 
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }



    // Update is called once per frame
    void Update()
    {
        checkKeys();
        checkState();
    }

    private void checkKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == true)
                paused = false;
            else
                paused = true;
        }
    }

    /**
     *  check if the pause boolean is true or false and change the game state accordingly
     */
    private void checkState()
    {
        if (!paused)
        {
            GameObject.Find("PauseMenu").GetComponent<Menu>().changeLocation(true);
            Time.timeScale = 1;
        }
        else
        {
            GameObject.Find("PauseMenu").GetComponent<Menu>().changeLocation(false);
            Time.timeScale = 0;
        }
    }
}
