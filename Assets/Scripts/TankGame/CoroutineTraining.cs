using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTraining : MonoBehaviour
{    
    public float dist = 2.0f;
    public float Speed = 3.0f;
    public float rotSpeed = 180.0f;    
    // Start is called before the first frame update    
    void Start()
    {
        StartCoroutine(UpDown());
        //StartCoroutine(Rotating());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Rotating(float rotDir)
    {
        float totalRot = 0.0f;        
        while (true)
        {
            float delta = rotSpeed * Time.deltaTime;

            if (delta > 360.0f - totalRot)
            {
                delta = 360.0f - totalRot;
            }

            totalRot += delta;
            //if(totalRot >= 360.0f - Mathf.Epsilon && totalRot <= 360.0f + Mathf.Epsilon)
            if (Mathf.Approximately(totalRot, 360.0f))
            {
                //otalRot = 0.0f;
                //rotDir *= -1.0f;
                break;
            }
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }

    IEnumerator UpDown()
    {
        Vector3 dir = Vector3.up;
        float rotDir = 1.0f;
        while (true)
        {
            float delta = Speed * Time.deltaTime;
            if (delta >= dist)
            {
                delta = dist;
                transform.Translate(dir * delta);
                dir *= -1.0f;
                dist = 2.0f;                
                yield return StartCoroutine(Rotating(rotDir));
                rotDir *= -1.0f;
            }
            else
            {
                dist -= delta;
                transform.Translate(dir * delta);
            }
            yield return null;
        }        
    }
}
