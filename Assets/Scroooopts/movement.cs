using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
   
    private Vector2 mousepos;
    Rigidbody2D rb;
    Vector2 movedir;
    public float moveSpeed = 5;
    public float rotationspeed = 10;
    public GameObject bullet;
 

    public GameObject YouLostObject;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        // wasd movement
        movedir.y = Input.GetAxis("Vertical");
        movedir.x = Input.GetAxis("Horizontal");
        rb.velocity = movedir * moveSpeed;
        // look at mouse
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousepos - (Vector2)transform.position;
        // calls shoot





    }
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<bullet>() != null) // toi ei   toimi clone bulletteihi
        {
            YouLost();             // häviäminen methodi

        }
    }

    public void YouLost()                                                               // häviäminen
    {
        YouLostObject.SetActive(true);                                                  // UI:n häviämis tavaroiden näyttäminen
        this.enabled = false;                                // pelaajan liikkumisen poistaminen

    }
    public void YouWin()
    {


        this.enabled = false;
    }

}
