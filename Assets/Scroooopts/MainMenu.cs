using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToScene0()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToScene2()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Debug.Log("en tii� miten quit");
    }
}
