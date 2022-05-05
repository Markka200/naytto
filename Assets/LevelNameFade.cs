using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelNameFade : MonoBehaviour
{
    public GameObject TextObject;
    TMPro.TextMeshProUGUI Text_;
    Image Image_;
    // Start is called before the first frame update
    void Start()
    {
        Text_ = TextObject.GetComponent<TMPro.TextMeshProUGUI>();
        Image_ = GetComponent<Image>();
        StartCoroutine(ReduceA());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ReduceA()
    {
      
        while (Text_.color.a > 0)
        {
            Text_.color = new Color(Text_.color.r, Text_.color.g, Text_.color.b, Text_.color.a - 0.05f);
            yield return new WaitForSeconds(0.05f);
        }

        while (Image_.color.a > 0)
        {
            Image_.color = new Color(Image_.color.r, Image_.color.g, Image_.color.b, Image_.color.a - 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        this.gameObject.SetActive(false);


    }
   
}
