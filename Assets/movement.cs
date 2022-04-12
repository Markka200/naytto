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
    public GameObject bulletspawner;
    Rigidbody2D barrel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        barrel = bulletspawner.GetComponent<Rigidbody2D>();    
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
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }




        // Debug.DrawRay(transform.position, transform.up * 20, Color.green,5) ;

    }
    void shoot()
    {
        
        Instantiate(bullet,barrel.position , transform.rotation);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == bullet.name) // toi ei   toimi clone bulletteihi
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
