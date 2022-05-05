using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class coin : MonoBehaviour
{

    public float rotationSpeed = 0.2f;
    public GameObject player;
    public int NextScene;

    public AudioClip hallelujah;
    private new AudioSource audio;
    float volume;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("player");
        audio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, +rotationSpeed);              // tämä objecti pyörii

        RaycastHit2D PlayerRay = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 15f,mask);    // raycast joka hiljentää ääntä mitä pidemmällä pelaaja on
        if (PlayerRay.collider != null)                                                          
        {
            volume = 1 - (PlayerRay.distance / 10); 
            audio.volume = volume;
        }
           
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player.gameObject)   // kun pelaaja koskee 
        {

            player.GetComponent<player>().YouWin();
            SceneManager.LoadScene(NextScene);          // seuraava scene
        }
    }

}
