using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePractice : MonoBehaviour
{
    float rotValue = 0.0f;
    float rotDir = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = 180.0f * Time.deltaTime;

        if(delta > 360.0f - rotValue)
        {
            delta = 360.0f - rotValue;
        }
        rotValue += delta;
        if(rotValue >= 360.0f)
        {
            rotValue = 0.0f;
            rotDir *= -1.0f;
        }

        transform.Rotate(Vector3.up * rotDir * delta);
    }
}
