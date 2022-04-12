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

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Wall", "player");
        
    }

    // Update is called once per frame
    void Update()
    {



       /* RaycastHit2D osuma = Physics2D.Raycast(transform.position, player.transform.position - transform.position, range, mask);

        if (osuma.collider.gameObject.name == player.name)
        {
             //VisionFound();
        }
        else
        { */
        
       // }
       
        if(VisionHu == false)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        }
        else if(GameObject.Find("Visionpref" + this.name) != null) 
        {
            Vector3 dir = VisionPrefObject.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        // Debug.DrawRay(transform.position, (player.transform.position - transform.position) ,Color.green, 2);
        
    }

    void VisionFound()
    {
   
            GetComponent<AIDestinationSetter>().target = player.transform;
            if (GameObject.Find("Visionpref" + this.name) != null)
            {
                GameObject.Destroy(VisionPrefObject);
            }
            VisionHu = false; // eli vanha visioni on poistettu
    }
    void VisionLost()
    {

        if (VisionHu == false)
        {
           VisionPrefObject = Instantiate(visionpref, player.transform.position, new Quaternion());
            
            VisionPrefObject.GetComponent<VisionPrefTrigger>().Enemy = this.gameObject;
            VisionPrefObject.name = "Visionpref"+ this.name;

            VisionHu = true; // eli on instantiatettu

            GetComponent<AIDestinationSetter>().target = VisionPrefObject.transform;
        }
        
    }
    public IEnumerator timer(double duration)
    {
        currentduration = duration;
        while (currentduration > 0)
        {
            Debug.Log(currentduration);
            yield return new WaitForSeconds(1.0f);
            currentduration--;
        }
        if (currentduration <= 0 )
        {
           
            GetComponent<AIDestinationSetter>().target = spot.transform;
            GameObject.Destroy(VisionPrefObject);
        }

        
    }






}
