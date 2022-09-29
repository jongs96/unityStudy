using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : UIProperty
{      
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void Initialize(Transform target,Color color)
    {
        myImage.color = color;
        StartCoroutine(Following(target, SceneData.Inst.myMinimap.sizeDelta));
    }

    IEnumerator Following(Transform target, Vector2 Size)
    {
        while(target != null)
        {
            Vector3 pos = SceneData.Inst.myMinimapCamera.WorldToViewportPoint(target.position);
            myRT.anchoredPosition = pos * Size;
            yield return null;
        }
        Destroy(gameObject);
    }
}
