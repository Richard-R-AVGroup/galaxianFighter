using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public void Loadlevel(string name)
    {
        Debug.Log("level requested for:" + name);
        Application.LoadLevel(name);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }
}