using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public GameObject player;
    public AudioClip zap;
    private new AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {

            StartCoroutine(playsound(zap));
            player.GetComponent<player>().YouLost();

        }
    }
    public IEnumerator playsound(AudioClip soundname)
    {
        audio.PlayOneShot(soundname);
        Debug.Log(soundname);
        yield return new WaitForSeconds(1.0f);
        

    }
}
