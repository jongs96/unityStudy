using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulCheck : MonoBehaviour
{
    public Transform Ball = null;
    public Transform[] Baselist = new Transform[4];
    Vector3 leftDir = Vector3.zero;
    Vector3 rightDir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rightDir = Vector3.Cross(Vector3.up, Baselist[1].position - Baselist[0].position).normalized;
        leftDir = Vector3.Cross(Baselist[3].position - Baselist[0].position, Vector3.up).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        bool Foul = false;
        Vector3 dir = (Ball.position - Baselist[1].position).normalized;
        if (Vector3.Dot(dir, rightDir) > 0.0f) Foul = true;
        dir = (Ball.position - Baselist[3].position).normalized;
        if (Vector3.Dot(dir, leftDir) > 0.0f) Foul = true;

        if(Foul)
        {
            Baselist[2].Rotate(Vector3.up * 360.0f * Time.deltaTime);
        }
    }
}
