using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public float RotSpeed = 90.0f;
    public float TopRotSpeed = 45.0f;
    public float CannonRotSpeed = 30.0f;
    public Transform myTop = null;
    public Transform myCannon = null;
    public Transform myMuzzle = null;
    public Bomb myBomb = null;
    public GameObject orgBomb = null;

    public Vector3 rot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -RotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myTop.Rotate(Vector3.up * -TopRotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myTop.Rotate(Vector3.up * TopRotSpeed * Time.deltaTime);
        }

        rot = myCannon.localRotation.eulerAngles;
        if (rot.x > 180.0f) rot.x -= 360.0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rot.x -= CannonRotSpeed * Time.deltaTime;            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rot.x += CannonRotSpeed * Time.deltaTime;            
        }
        rot.x = Mathf.Clamp(rot.x, -60.0f, 10.0f);
        myCannon.localRotation = Quaternion.Euler(rot);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            myBomb?.OnFire();
            //reload
            GameObject obj = Instantiate(Resources.Load("Prefabs\\Bomb"),myMuzzle.position,myMuzzle.rotation,myMuzzle) as GameObject;//Instantiate(orgBomb);
            //obj.transform.SetParent(myMuzzle);
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = Quaternion.identity;
            /*
            if (obj != null)
            {
                myBomb = obj.GetComponent<Bomb>();
            }
            else
            {
                myBomb = null;
            }
            */
            myBomb = obj?.GetComponent<Bomb>() ?? null;
        }
    }
}
