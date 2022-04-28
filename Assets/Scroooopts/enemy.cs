using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject spot;
    public float range = Mathf.Infinity;
    LayerMask mask;
    public GameObject visionpref;
    public bool VisionHu = true;
    public double currentduration;
    GameObject VisionPrefObject;

    public bool ShootingSystem = false;
    public float wait = 1;
    bool counting = false;
    Transform localscale;
    public GameObject CanvasMeter;
    public float MeterIncrease = 0.2f;

    public GameObject bullet;
    public GameObject bulletspawner;
    Rigidbody2D barrel;
    public double ShootPause = 2;
    bool shooting = false;
    public double shootduration;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Wall", "player");
        barrel = bulletspawner.GetComponent<Rigidbody2D>();
        localscale = CanvasMeter.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {


        if (VisionHu == false)                                                           // vihollinen katsoo pelaajaa kohti jos pelaaja on n‰hty
        {                                    
            Vector3 dir = player.transform.position - transform.position;           
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                    
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);                    

        }
        else if(GameObject.Find("Visionpref" + this.name) != null)                      // jos pelaaja on hukattu niin menn‰‰n sen viimeksi n‰hdylle sijainnille (visionpref)
        {
            Vector3 dir = VisionPrefObject.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        
    }

    void VisionFound()
    {
                
            GetComponent<AIDestinationSetter>().target = player.transform;  // vihollinen jahtaa pelaajaa
            if (GameObject.Find("Visionpref" + this.name) != null)
            {
                GameObject.Destroy(VisionPrefObject);
            }
        VisionHu = false;                                               // eli vanha visioni on poistettu
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
            StartCoroutine(Meter(wait, MeterIncrease));
            
        }
    }
    void VisionLost()
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
        localscale.localScale = new Vector3(localscale.localScale.x + increase, 1, 1); 
        if(localscale.localScale.x >= 16)
        {
            player.GetComponent<movement>().YouLost();
        }
        
        counting = false;
       
    }








}
