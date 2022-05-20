using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taser : MonoBehaviour
{
    [Header("")]
    
    public bool enemy = false;
    [HideInInspector]
    public GameObject EnemyCollision;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 7)
        {
            enemy = true;
            EnemyCollision = collision.gameObject;
            
        }
   

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 7)
        {
            enemy = false;
            EnemyCollision = collision.gameObject;

        }


    }


}
