using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyQuaternion : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rot.x += Time.deltaTime * 180.0f;
        rot.y += Time.deltaTime * 180.0f;
        rot.z += Time.deltaTime * 180.0f;

        Quaternion x = Quaternion.Euler(new Vector3(rot.x, 0, 0));
        Quaternion y = Quaternion.Euler(new Vector3(0, rot.y, 0));
        Quaternion z = Quaternion.Euler(new Vector3(0, 0, rot.z));

        transform.localRotation = Quaternion.identity * y;
    }
}
