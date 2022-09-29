using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public enum TYPE
    {
        RPG,FPS
    }
    public TYPE myType = TYPE.RPG;
    public LayerMask CrashMask = default;
    public Transform myCam = null;
    public float rotSpeed = 5.0f;
    public float zoomSpeed = 3.0f;
    public Vector2 RotateRange = new Vector2(-70, 80);
    Vector2 curRot = Vector2.zero;
    Vector2 desireRot = Vector2.zero;
    public float SmoothRotSpeed = 3.0f;
    public Vector2 ZoomRange = new Vector2(1.5f, 10.0f);
    public float SmoothDistSpeed = 3.0f;
    float desireDist = 0.0f;
    float curCamDist = 0.0f;
    float OffsetDist = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //���콺Ŀ�� �����
        //Cursor.visible = false;
        //���콺Ŀ�� ����
        //Cursor.lockState = CursorLockMode.Confined;
        desireRot.x = curRot.x = transform.localRotation.eulerAngles.x;
        desireDist = curCamDist = -myCam.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(myType == TYPE.FPS || Input.GetMouseButton(1))
        {
            desireRot.x += -Input.GetAxisRaw("Mouse Y") * rotSpeed;
            desireRot.x = Mathf.Clamp(desireRot.x, RotateRange.x, RotateRange.y);            
            
            if (Input.GetKey(KeyCode.LeftControl))
            {
                desireRot.y += Input.GetAxisRaw("Mouse X") * rotSpeed;                
            }
            else
            {
                curRot.y = 0.0f;                
                transform.parent.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * rotSpeed);
            }
            
            curRot = Vector2.Lerp(curRot, desireRot, Time.deltaTime * SmoothRotSpeed);

            Quaternion x = Quaternion.Euler(new Vector3(curRot.x, 0, 0));
            Quaternion y = Quaternion.Euler(new Vector3(0, curRot.y, 0));
            transform.localRotation = y * x;
        }
        else
        {
            if (!Input.GetKey(KeyCode.LeftControl)) transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, 0, 0));
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > Mathf.Epsilon || Input.GetAxisRaw("Mouse ScrollWheel") < -Mathf.Epsilon)
        {
            desireDist += Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
            desireDist = Mathf.Clamp(desireDist, ZoomRange.x, ZoomRange.y);            
        }
        curCamDist = Mathf.Lerp(curCamDist, desireDist, Time.deltaTime * SmoothDistSpeed);

        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = -transform.forward;
        float checkDist = Mathf.Min(curCamDist, desireDist);
        if (Physics.Raycast(ray, out RaycastHit hit, checkDist + OffsetDist + 0.1f, CrashMask))
        {
            curCamDist = Vector3.Distance(transform.position, hit.point + myCam.forward * OffsetDist);            
        }        

        myCam.transform.localPosition = new Vector3(0, 0, -curCamDist);
    }
}
