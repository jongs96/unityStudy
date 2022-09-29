using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform myTarget = null;
    public Vector3 followingPos = Vector3.zero;
    public float desireDist = 5.0f;
    float Dist = 5.0f;
    public float Height = 1.5f;
    public float ZoomSpeed = 5.0f;
    public float SmoothZoomSpeed = 5.0f;
    public Vector2 ZoomRange = new Vector2(3.0f, 10.0f);
    public Vector2 RotationRange = new Vector2(0.0f, 80.0f);

    public float SmoothRotateSpeed = 10.0f;
    Vector3 myAngle = Vector3.zero;
    public float SmoothMoveSpeed = 5.0f;
    Vector3 desireAngle = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        followingPos = myTarget.position;
        Dist = desireDist;
        desireAngle = myAngle = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0.0f)
        {
            desireDist -= Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSpeed;
            desireDist = Mathf.Clamp(desireDist, ZoomRange.x, ZoomRange.y);
        }
        Dist = Mathf.Lerp(Dist, desireDist, Time.deltaTime * SmoothZoomSpeed);

        if(Input.GetAxisRaw("LookUp") != 0.0f)
        {
            desireAngle.x += Input.GetAxisRaw("LookUp");
            desireAngle.x = Mathf.Clamp(desireAngle.x, RotationRange.x, RotationRange.y);            
        }
        myAngle = Vector3.Slerp(myAngle, desireAngle, Time.deltaTime * SmoothRotateSpeed);
        transform.localRotation = Quaternion.Euler(myAngle);

        followingPos = Vector3.Lerp(followingPos, myTarget.position, Time.deltaTime * SmoothMoveSpeed);
        transform.position = followingPos + Vector3.up*Height + -Camera.main.transform.forward * Dist;
    }
}
