using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    

    Rigidbody2D rb;
    public float speed = 2;
    public float rotateSpeed = 0.001f;
    GameObject player;
    bool rotating = true;
    public float rotateTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        if(rotating)
        { 
        rb.velocity = transform.up * speed;
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle -90), rotateSpeed * Time.deltaTime);
            StartCoroutine("disablerotate");
        }

    }
    IEnumerator disablerotate()
    {
        yield return new WaitForSeconds(rotateTime);
        rotating = false;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            Destroy(this.gameObject);
        }
    }

}
