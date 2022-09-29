using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public MeshFilter myFilter;
    public float ViewAngle = 90.0f;
    public float ViewDistance = 3.0f;
    public int DetailCount = 100;
    Vector3[] myDirs = null;
    public LayerMask crashmask = default;
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] vb = new Vector3[DetailCount + 1];
        int[] ib = new int[(DetailCount - 1) * 3];
        myDirs = new Vector3[DetailCount];
        Vector3 dir = transform.forward * ViewDistance;
        //Quaternion * Vector = rotated vector
        //Quaternion * Quaternion = two truns combined Quaternion
        myDirs[0] = Quaternion.AngleAxis(-ViewAngle / 2.0f, Vector3.up) * dir;

        float angleGap = ViewAngle / (float)(DetailCount - 1);
        
        vb[0] = Vector3.zero;
        for (int i = 1; i<vb.Length; ++i)
        {
            vb[i] = vb[0] + myDirs[i - 1];
            if(i<DetailCount) myDirs[i] = Quaternion.AngleAxis(angleGap, Vector3.up) * myDirs[i - 1];

            if (i >= 2)
            {
                ib[(i - 2) * 3] = 0;
                ib[(i - 2) * 3 + 1] = i - 1;
                ib[(i - 2) * 3 + 2] = i;
            }
        }

        Mesh _mesh = new Mesh();
        _mesh.vertices = vb;
        _mesh.triangles = ib;
        myFilter.mesh = _mesh;
    }
    private void FixedUpdate()
    {
        Vector3[] vb = myFilter.mesh.vertices;
        for (int i = 0; i < myDirs.Length; ++i)
        {
            if(Physics.Raycast(transform.position, transform.rotation * myDirs[i], out RaycastHit hit, ViewDistance, crashmask))
            {
                vb[i + 1] = vb[0] + myDirs[i].normalized * hit.distance;
            }
            else
            {
                vb[i + 1] = vb[0] + myDirs[i].normalized * ViewDistance;
            }
        }
        myFilter.mesh.vertices = vb;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
