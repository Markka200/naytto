using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;
using UnityEngine.Audio;
public class player : MonoBehaviour
{
    taser Taser;
    Animator animator;

    [Header("Button Settings")]
    [SerializeField] private Vector2 mousepos;
    Rigidbody2D rb;
    Vector2 movedir;
    public float moveSpeed = 5;

    [Header("Tase settings")]
    [SerializeField]
    public GameObject TaserArea;
    public GameObject TaseAmountText;
    private bool tasing;
    public int TaseAmount = 3;
    public GameObject lightning;

    [Header ("audio")]
    public AudioClip TaserSound;
    private new AudioSource audio;

    [Header("Losing")]
    public GameObject YouLostObject;
    // Start is called before the first frame update
    private void Awake()
    {
        Taser = TaserArea.GetComponent<taser>();
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        // wasd movement
        movedir.y = Input.GetAxis("Vertical");
        movedir.x = Input.GetAxis("Horizontal");                            // itse se liikkuminen
        rb.velocity = movedir * moveSpeed;
        if(Mathf.Abs(movedir.x) > Mathf.Abs(movedir.y))
        {
            animator.SetFloat("speed", Mathf.Abs(movedir.x));                  // juoksemis animaatoin pyytäminen
        }
        else
        {
            animator.SetFloat("speed", Mathf.Abs(movedir.y));
            
        }

        
        if (tasing == false)
        {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // hiirtä kohti katsominen
        }
        
        transform.up = mousepos - (Vector2)transform.position;
        
        
        
        if (Input.GetMouseButtonDown(0) && tasing == false && TaseAmount > 0)  //shoot
        {
            
            tasing = true;
            StartCoroutine(TaserM());
            
        }
    }
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<bullet>() != null) // toi ei   toimi clone bulletteihi
        {
            YouLost();             // häviäminen methodi

        }
    }
    public IEnumerator TaserM()         // jos taser alueella on vihollinen ja ei ole seinää välissä, niin kutsu vihollisesta taser metodi
    {
        
        if (Taser.enemy == true && Taser.EnemyCollision.GetComponent<enemy>().VisionHu == true)                   
        {
            LayerMask mask = LayerMask.GetMask("Enemy", "Wall");
            RaycastHit2D WallTest = Physics2D.Raycast(transform.position, Taser.EnemyCollision.transform.position - transform.position , 20f, mask);  // onko seinä välissä
            
            if (WallTest.transform.gameObject == Taser.EnemyCollision)
            {
                mousepos = Taser.EnemyCollision.transform.position;                                         // pelaaja pakotetaan katsomaan vihollista
                lightning.GetComponent<LineRenderer>().enabled = true;                                      // sähkö efekti pelaajan ja vihollisen väliin
                lightning.GetComponent<LightningBoltScript>().EndObject = Taser.EnemyCollision;
                lightning.GetComponent<LightningBoltScript>().enabled = true;
                audio.PlayOneShot(TaserSound);                                                              // taser ääni
                StartCoroutine(Taser.EnemyCollision.GetComponentInParent<MeshVision>().Tased());
                TaseAmountText.GetComponent<TMPro.TextMeshProUGUI>().text = "Tases left:" + (TaseAmount - 1);          // Mahdollisten taser käyttöjen vähentäminen
                TaseAmount += -1;
                yield return new WaitForSeconds(2.0f);                                                                  
                lightning.GetComponent<LineRenderer>().enabled = false;
                lightning.GetComponent<LightningBoltScript>().enabled = false;
                tasing = false; 
            }
            else
            {
                Debug.Log("Wall in the way");
                tasing = false;
            }
        }
        else
        {
            Debug.Log("miss");
            tasing = false;
          
        }
        
        
    }

    public void YouLost()                                                               // häviäminen
    {
        YouLostObject.SetActive(true);                                                  // UI:n häviämis tavaroiden näyttäminen
        this.enabled = false;                                                           // pelaajan liikkumisen poistaminen

    }
    public void YouWin()
    {


        this.enabled = false;                                                           // pelaajan liikkumisen poistaminen
    }


}
