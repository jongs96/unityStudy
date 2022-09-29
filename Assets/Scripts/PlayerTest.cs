using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 dir;
    float dist;
    float Speed = 2.0f;
    void Start()
    {
        Vector3 target = new Vector3(3, 1, 3);
        dir = target - transform.position;
        dist = dir.magnitude;
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (dist > 0.0f)
        {
            float delta = Speed * Time.deltaTime;
            if(delta > dist)
            {
                delta = dist;
            }
            //transform.position += dir * delta;
            transform.Translate(dir * delta);

            dist -= delta;
        }
        */

        dir = Vector3.zero;//new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //dir += -transform.right;            
            transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            //dir += transform.right;            
            transform.Rotate(Vector3.up * 90.0f * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //world
            //dir += transform.forward;
            //local
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //world
            //dir += -transform.forward;
            //local
            dir += -Vector3.forward;
        }
        //transform.position += dir.normalized * Speed * Time.deltaTime;
        //transform.localPosition += dir.normalized * Speed * Time.deltaTime;
        transform.Translate(dir.normalized * Speed * Time.deltaTime,Space.Self);
    }
}
