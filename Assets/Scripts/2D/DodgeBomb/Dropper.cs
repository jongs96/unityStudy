using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dropper : MonoBehaviour
{
    public enum STATE
    {
        Create, Active, DeActive
    }
    public STATE myState = STATE.Create;
    public Vector2 MoveRange = new Vector2(-5, 5);
    public float MoveSpeed = 3.0f;
    float MoveDir = 1.0f;
    float MoveDist = 0.0f;

    public event UnityAction ItemDestroy = null;

    public void SetActive(bool act)
    {
        if(act)
        {
            ChangeState(STATE.Active);
        }
        else
        {
            ChangeState(STATE.DeActive);
        }
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.Create:
                break;
            case STATE.Active:
                if(MoveDir > 0.0f)
                {
                    MoveDist = Mathf.Abs(MoveRange.y - transform.localPosition.x);
                }
                else
                {
                    MoveDist = Mathf.Abs(MoveRange.x - transform.localPosition.x);
                }
                StartCoroutine(Dropping(2.0f));
                break;
            case STATE.DeActive:
                ItemDestroy?.Invoke();
                StopAllCoroutines();
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Active:
                float delta = MoveSpeed * Time.deltaTime;
                if(delta >= MoveDist)
                {
                    delta = MoveDist;                    
                }
                MoveDist -= delta;
                transform.Translate(Vector3.right * MoveDir * delta);
                if(Mathf.Approximately(MoveDist, 0.0f)) 
                {
                    MoveDir *= -1.0f;
                    MoveDist = Mathf.Abs(MoveRange.y - MoveRange.x);
                }
                break;
        }
    }

    IEnumerator Dropping(float delay)
    {
        while(true)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Item"),transform.position,Quaternion.identity) as GameObject;
            obj.transform.Translate(Vector3.forward * 0.1f);

            DodgeBomb.Item scp = obj.GetComponent<DodgeBomb.Item>();
            ItemDestroy += scp.ItemDestroy;

            if (Random.Range(0,10) >= 7)
            {
                scp.Inialize(DodgeBomb.Item.TYPE.Coin, this);
            }
            else
            {
                scp.Inialize(DodgeBomb.Item.TYPE.Bomb, this);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
