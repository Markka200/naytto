using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class buttonclick : MonoBehaviour
{
    public Button ToSample;
    // Start is called before the first frame update
    void Start()
    {
        ToSample.onClick.AddListener(SampleScene);
        
    }

    void SampleScene()
    {
        SceneManager.LoadScene("SampleScene");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
