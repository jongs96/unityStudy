using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyShader : MonoBehaviour
{
    public Renderer testRenderer;
    public Material OutLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DisApearing());
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Material[] mats = testRenderer.materials;
            mats[1] = null;
            testRenderer.materials = mats;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Material[] mats = testRenderer.materials;
            mats[1] = OutLine;
            testRenderer.materials = mats;
        }
    }

    IEnumerator DisApearing()
    {
        Material mat = testRenderer.material;
        while(mat.GetFloat("_DissolveAmount") < 1.0f)
        {
            mat.SetFloat("_DissolveAmount", mat.GetFloat("_DissolveAmount") + Time.deltaTime);
            yield return null;
        }
    }
}
