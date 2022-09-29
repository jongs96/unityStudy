using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyLerp : MonoBehaviour
{
    //보간법 - 선형(Lerp), 구형(SLerp)
    //선형보간 (1.0f - t) * A + t * B;

    // Start is called before the first frame update
    public Vector3 A = new Vector3(-5, 1, 0);
    public Vector3 B = new Vector3(5, 1, 0);

    public Vector3 rot1 = new Vector3(0, 0, 0);
    public Vector3 rot2 = new Vector3(0, 180, 0);
    public float playTime = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime * 0.1f;
        float t = Mathf.Lerp(0.0f, 1.0f, playTime);
        transform.position = Vector3.Lerp(A, B, t);
        transform.rotation = Quaternion.Euler(Vector3.Slerp(rot1, rot2, t));
    }
}
