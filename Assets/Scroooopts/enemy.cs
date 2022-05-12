using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class enemy : MonoBehaviour
{
    [Header("")]
    public GameObject player;
    public GameObject spot;
    public float range = Mathf.Infinity;
    LayerMask mask;
    public GameObject visionpref;
    public bool VisionHu = true;   // vision hukattu
    public double currentduration;
    GameObject VisionPrefObject;

    [Header("shooting // potentially scrapped idea")]
    public bool ShootingSystem = false;
    public float wait = 1;
    bool counting = false;
    Transform localscale;
    

    public GameObject bullet;
    public GameObject bulletspawner;
    Rigidbody2D barrel;
    public double ShootPause = 2;
    bool shooting = false;
    public double shootduration;
    [Header("Detection Meter")]
    public GameObject CanvasMeter;
    public float MeterIncrease = 0.2f;
    [Header("Audio settings")]
    public AudioClip huh;
    private new AudioSource audio;
    bool Sounding = false;



    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Wall", "player");
        barrel = bulletspawner.GetComponent<Rigidbody2D>();
        localscale = CanvasMeter.GetComponent<RectTransform>();
        audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {


        if (VisionHu == false)                                                           // vihollinen katsoo pelaajaa kohti jos pelaaja on n‰hty 
        {                                    
            Vector3 dir = player.transform.position - transform.position;           
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                    
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            RaycastHit2D PlayerRay = Physics2D.Raycast(transform.position, dir, 20f, mask);
            if (PlayerRay.collider == null || PlayerRay.collider.name != player.name)                     // jos vihollisen ja pelaajan v‰liss‰ on sein‰, niin kutsutaan VisionLost()
            {
                VisionLost();

            }

        if (ShootingSystem)                                                                                                         
        {
            if (shooting == false)
            {

                Debug.Log("shoot");                         
                StartCoroutine(shoot());
                shooting = true;
            }
        }
        else if (counting == false)
        {
            counting = true;
            StartCoroutine(Meter(wait, MeterIncrease));                                             //n‰kymis mittari

        }

        }
        else if(GameObject.Find("Visionpref" + this.name) != null)                      // jos pelaaja on hukattu niin menn‰‰n sen viimeksi n‰hdylle sijainnille (visionpref)
        {
            Vector3 dir = VisionPrefObject.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        
    }

    void VisionFound()                                                        // (MeshVision kutsuu) jos pelaaja n‰hd‰‰n 
    {
        
        if (Sounding == false)
        {

            Sounding = true;
            StartCoroutine(playsound(huh));                             //"huh" ‰‰ni

        }
                
            GetComponent<AIDestinationSetter>().target = player.transform;  // vihollinen jahtaa pelaajaa
            if (GameObject.Find("Visionpref" + this.name) != null)
            {
                GameObject.Destroy(VisionPrefObject);
            }
        VisionHu = false;                                               // eli vanha muistettu paikka, jossa pelaaja n‰htiin on poistettu

    }
    void VisionLost()                                                               //Jos vihollinen ei n‰e en‰‰n pelaajaa, niin vihollinen menee pelaajan viimeksi n‰htyyn sijaintiin
    {

        if (VisionHu == false)
        {
           VisionPrefObject = Instantiate(visionpref, player.transform.position, new Quaternion()); // tehd‰‰n objecti viimeksi n‰hdylle pelaajan sijainnille
            
            VisionPrefObject.GetComponent<VisionPrefTrigger>().Enemy = this.gameObject;             // annetaan objectille triggeri joka saa vihollisen menem‰‰n ajanmukaan takaisin 
            VisionPrefObject.name = "Visionpref"+ this.name;                                        // ett‰ voi olla monta kappaletta t‰t‰ samaa scripti‰ toimimassa samaan aikaan, pit‰‰ vaihtaa objectin nimi 

            VisionHu = true;                                                                        // eli objecti on tehty

            GetComponent<AIDestinationSetter>().target = VisionPrefObject.transform;                // ett‰ vihollinen menee objectille
 
        }
        
    }
    public IEnumerator timer(double duration)                                // ajastin, jonka j‰lkeen vihollinen menee spot objectille
    {
        currentduration = duration;
        while (currentduration > 0)
        {
          
            yield return new WaitForSeconds(1.0f);
            currentduration--;
        }
        if (currentduration <= 0 )
        {
           
            GetComponent<AIDestinationSetter>().target = spot.transform;
            GameObject.Destroy(VisionPrefObject);
            
        }


    }  
    public IEnumerator shoot()
    {
        shootduration = ShootPause;
        while (shootduration > 0)
        {
            yield return new WaitForSeconds(1.0f);
            shootduration--;
        }
        if (shootduration <= 0)
        {
            Instantiate(bullet, barrel.position, transform.rotation);
            shooting = false;
        }
    }
    public IEnumerator Meter(float time, float increase) // nostaa tietyn ajan v‰lein DetectionMeterin x pituutta
    {
    
        yield return new WaitForSeconds(time);
        localscale.localScale = new Vector3(localscale.localScale.x + increase, localscale.localScale.y, localscale.localScale.z); 
        if(localscale.localScale.x >= 16)
        {
            player.GetComponent<player>().YouLost();
        }
        
        counting = false;
       
    }
    public IEnumerator playsound(AudioClip soundname)
    {
        audio.PlayOneShot(soundname);
        Debug.Log(soundname);
        yield return new WaitForSeconds(1.0f);
        Sounding = false;

    }








}
