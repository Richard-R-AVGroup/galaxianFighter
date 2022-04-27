using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void Loadlevel(string name)
    {
        Debug.Log("level requested for:" + name);
        SceneManager.LoadScene(name); 
    }

    public void QuitRequest()
    {
        Application.Quit();
    }
}