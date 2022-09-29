using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public MeshFilter myFilter;
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] vb = new Vector3[16];
        int[] ib = new int[18];
        //0
        vb[0] = new Vector3( 0, 1, 0);
        vb[1] = new Vector3(-1, 0, 1);
        vb[2] = new Vector3( 1, 0, 1);
        //1
        vb[3] = new Vector3( 0, 1, 0);
        vb[4] = new Vector3(-1, 0,-1);
        vb[5] = new Vector3(-1, 0, 1);
        //2
        vb[6] = new Vector3( 0, 1, 0);
        vb[7] = new Vector3( 1, 0, 1);
        vb[8] = new Vector3( 1, 0,-1);
        //3
        vb[9] = new Vector3( 0, 1, 0);
        vb[10] = new Vector3( 1, 0,-1);
        vb[11] = new Vector3(-1, 0,-1);
        //¹Ù´Ú
        vb[12] = new Vector3( 1, 0, 1);
        vb[13] = new Vector3(-1, 0, 1);
        vb[14] = new Vector3( 1, 0,-1);
        vb[15] = new Vector3(-1, 0,-1);
        //»ï°¢Çü 0
        ib[0] = 0;
        ib[1] = 1;
        ib[2] = 2;
        //»ï°¢Çü 1
        ib[3] = 3;
        ib[4] = 4;
        ib[5] = 5;
        //»ï°¢Çü 2
        ib[6] = 6;
        ib[7] = 7;
        ib[8] = 8;
        //»ï°¢Çü 3
        ib[9] = 9;
        ib[10] = 10;
        ib[11] = 11;
        //¹Ù´Ú »ï°¢Çü 0 
        ib[12] = 12;
        ib[13] = 13;
        ib[14] = 14;
        //¹Ù´Ú »ï°¢Çü 1 
        ib[15] = 13;
        ib[16] = 15;
        ib[17] = 14;

        Vector2[] uv = new Vector2[16];        
        uv[0] = new Vector2(0.5f, 0.5f);
        uv[1] = new Vector2(0,1);
        uv[2] = new Vector2(1,1);

        uv[3] = new Vector2(0.5f, 0.5f);
        uv[4] = new Vector2(0, 0);
        uv[5] = new Vector2(0, 1);

        uv[6] = new Vector2(0.5f, 0.5f);
        uv[7] = new Vector2(1, 1);
        uv[8] = new Vector2(1, 0);

        uv[9] = new Vector2(0.5f, 0.5f);
        uv[10] = new Vector2(1, 0);
        uv[11] = new Vector2(0, 0);

        uv[12] = new Vector2(0, 1);
        uv[13] = new Vector2(1, 1);
        uv[14] = new Vector2(0, 0);
        uv[15] = new Vector2(1, 0);


        Vector3[] normals = new Vector3[16];

        Vector3 n = Vector3.Cross(vb[1] - vb[0], vb[2] - vb[0]).normalized;
        normals[0] = n;
        normals[1] = n;
        normals[2] = n;

        n = Vector3.Cross(vb[4] - vb[3], vb[5] - vb[3]).normalized;
        normals[3] = n;
        normals[4] = n;
        normals[5] = n;

        n = Vector3.Cross(vb[7] - vb[6], vb[8] - vb[6]).normalized;
        normals[6] = n;
        normals[7] = n;
        normals[8] = n;

        n = Vector3.Cross(vb[10] - vb[9], vb[11] - vb[9]).normalized;
        normals[9] = n;
        normals[10] = n;
        normals[11] = n;

        n = Vector3.down;
        normals[12] = n;
        normals[13] = n;
        normals[14] = n;
        normals[15] = n;


        Mesh _mesh = new Mesh();
        _mesh.vertices = vb;
        _mesh.triangles = ib;
        _mesh.uv = uv;
        _mesh.normals = normals;
        

        myFilter.mesh = _mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
