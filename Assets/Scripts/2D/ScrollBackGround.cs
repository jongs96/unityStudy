using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    public enum TYPE
    {
        NORMAL, OFFSET
    }
    public TYPE myType = TYPE.OFFSET;
    public float Speed = 0.1f;
    public SpriteRenderer myRenderer = null;

    public Transform[] Bglist = new Transform[3];
    public Vector2 Size = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        OnMove(Vector2.right);
        /*
        Vector2 offset = GetComponent<SpriteRenderer>().material.mainTextureOffset;
        offset.x += Time.deltaTime * Speed;
        GetComponent<SpriteRenderer>().material.mainTextureOffset = offset;
        */
    }

    public void OnMove(Vector2 dir)
    {
        switch(myType)
        {
            case TYPE.OFFSET:
                myRenderer.material.mainTextureOffset += dir * Time.deltaTime * Speed;
                break;
            case TYPE.NORMAL:
                foreach(Transform tr in Bglist)
                {
                    tr.Translate(dir * Time.deltaTime * Speed);
                    if( tr.localPosition.x <= -Size.x * (float)(Bglist.Length - 1))
                    {
                        tr.Translate(new Vector3(Size.x * (float)Bglist.Length * transform.localScale.x, 0, 0));
                    }
                    if(tr.localPosition.x >= Size.x * (float)(Bglist.Length - 1))
                    {
                        tr.Translate(new Vector3(-Size.x * (float)Bglist.Length * transform.localScale.x, 0, 0));
                    }
                }
                break;
        }        
    }
}
