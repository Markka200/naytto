using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    public float rotationSpeed = 0.2f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, +rotationSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player.gameObject)
        {

            player.GetComponent<movement>().YouWin();
        }
    }
}
