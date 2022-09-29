using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNumber : MonoBehaviour
{    
    public void Initialize(int n)
    {
        StartCoroutine(Activating(n));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Activating(int n)
    {
        TMPro.TMP_Text Label = GetComponentInChildren<TMPro.TMP_Text>();
        Label.text = n.ToString();
        yield return new WaitForSeconds(0.5f);

        Color col = Label.color;

        while(col.a > 0.0f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 50.0f);
            col.a -= Time.deltaTime;
            Label.color = col;
            yield return null;
        }

        Destroy(gameObject);
    }
}
