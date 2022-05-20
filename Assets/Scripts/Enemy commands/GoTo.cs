using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GoTo : MonoBehaviour
{

    public int timer = 5;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject NextCheckpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
            StartCoroutine(GoToNextCheckpoint(NextCheckpoint));
        
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
         
   
     
    }
    
    IEnumerator GoToNextCheckpoint(GameObject checkpoint)
    {
        yield return new WaitForSeconds(timer);
        if (Enemy.GetComponent<AIDestinationSetter>().target == NextCheckpoint.transform || Enemy.GetComponent<AIDestinationSetter>().target == this.transform)
        {
            Enemy.GetComponent<AIDestinationSetter>().target = checkpoint.transform;
        }
    }
}
