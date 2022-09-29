using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    // Start is called before the first frame update
    public float Day = 100.0f;
    float Speed = 0.0f;
    void Start()
    {
        Speed = 360.0f / Day;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f,Speed * Time.deltaTime,0.0f);
    }
}
