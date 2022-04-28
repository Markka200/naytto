using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookdirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public int rotation;
    public GameObject Enemy;
    bool coll;
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

            Enemy.transform.rotation = Quaternion.Lerp(Enemy.transform.rotation, Quaternion.Euler(0, 0, rotation), 2 * Time.deltaTime);

        }
    }

}
