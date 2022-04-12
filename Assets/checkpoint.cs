using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public GameObject Enemy;
    bool coll;
    public float rotationSpeed = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
   private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == Enemy.gameObject)
        {
            
            coll = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Enemy.gameObject)
        {
            coll = false;
            Debug.Log("collider exit");

        }   
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (coll == true)
        {
            Enemy.transform.Rotate(0, 0, +rotationSpeed);
            
        }
    }
 








}
