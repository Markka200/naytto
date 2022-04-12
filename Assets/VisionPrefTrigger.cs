using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionPrefTrigger : MonoBehaviour
{

    public GameObject Enemy;   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Enemy)
        {
            StartCoroutine(Enemy.GetComponent<enemy>().timer(5));

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
