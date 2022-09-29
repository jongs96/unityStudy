using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CharacterMovement, IBattle
{
    public CharacterStat myInfo;
    public enum STATE
    {
        Create, Normal, Battle, DEATH
    }
    public STATE myState = STATE.Create;
    public AIPerception mySenser = null;

    Vector3 StartPos = Vector3.zero;
    public Transform myHeadPos;
    #region 전투시스템
    public Transform HeadPos
    {
        /*
        get
        {
            return myHeadPos;
        }
        */
        get => myHeadPos;
    }
    public bool IsLive
    {
        get
        {
            if (Mathf.Approximately(myInfo.CurHP, 0.0f))
            {
                return false;
            }
            return true;
        }
    }

    HpBar myHpBar = null;   
    public void OnDamage(float dmg)
    {
        myInfo.CurHP -= dmg;
        myHpBar.mySlider.value = myInfo.CurHP / myInfo.TotalHP;
        StartCoroutine(Damaging());
        if (Mathf.Approximately(myInfo.CurHP, 0.0f))
        {
            ChangeState(STATE.DEATH);            
        }
        else
        {
            myAnim.SetTrigger("Damage");            
        }
    }

    IEnumerator Damaging()
    {
        Renderer render = GetComponentInChildren<Renderer>();       
        render.material.SetColor("_Color", new Color(1.0f,0.0f,0.0f));
        yield return new WaitForSeconds(1.0f);
        render.material.SetColor("_Color", Color.white);
    }

    #endregion
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                StopAllCoroutines();
                myAnim.SetBool("IsMoving", false);
                StartCoroutine(GoingToRndPos());
                break;
            case STATE.Battle:
                StopAllCoroutines();
                FollowTarget(mySenser.myTarget.transform, myInfo.MoveSpeed, myInfo.RotSpeed, OnAttack);
                break;
            case STATE.DEATH:
                if (myHpBar != null) Destroy(myHpBar.gameObject);
                StopAllCoroutines();
                myAnim.SetTrigger("Death");
                mySenser.enabled = false;
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                break;
        }
    }
    
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:                
                break;
            case STATE.Battle:
                if(!myAnim.GetBool("IsAttacking")) myInfo.curAttackDelay += Time.deltaTime;
                if(mySenser.myTarget != null && !mySenser.myTarget.IsLive)
                {
                    mySenser.OnLostTarget();
                }
                break;
        }
    }

    IEnumerator GoingToRndPos()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 3.0f));
        Vector3 rndPos = StartPos;
        rndPos.x += Random.Range(-5.0f, 5.0f);
        rndPos.z += Random.Range(-5.0f, 5.0f);
        MoveToPosition(rndPos, myInfo.MoveSpeed, myInfo.RotSpeed, () => StartCoroutine(GoingToRndPos()));
    }

    void OnAttack()
    {        
        if (!myAnim.GetBool("IsAttacking"))
        {            
            if (myInfo.curAttackDelay >= myInfo.AttackDelay)
            {
                myInfo.curAttackDelay = 0.0f;
                myAnim.SetTrigger("Attack");
            }
        }
    }

    public void AttackTarget()
    {        
        if(mySenser.myTarget != null)
        {
            if(mySenser.myTarget.IsLive) mySenser.myTarget.OnDamage(30.0f);
        }
    }
    #region 몬스터사라지게
    public void DisAppear()
    {
        StartCoroutine(DisAppaering());
    }  

    IEnumerator DisAppaering()
    {
        yield return new WaitForSeconds(2.0f);
        float dist = 1.0f;
        while(dist > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;
            dist -= delta;
            transform.Translate(Vector3.down * delta);
            yield return null;
        }
        Destroy(gameObject);
    }
    #endregion

    bool Changerable()
    {
        return myState != STATE.DEATH;
    }

    // Start is called before the first frame update
    void Start()
    {        
        StartPos = transform.position;
        mySenser.FindTarget += () => { if (Changerable()) ChangeState(STATE.Battle); };        
        mySenser.LostTarget += () => { if (Changerable()) ChangeState(STATE.Normal); };

        GameObject obj = Instantiate(Resources.Load("Prefabs/HpBar")) as GameObject;
        myHpBar = obj.GetComponent<HpBar>();
        myHpBar.myTarget = myHeadPos;
        obj.transform.SetParent(SceneData.Inst.myHpBars);

        obj = Instantiate(Resources.Load("Prefabs/MinimapIcon")) as GameObject;
        MinimapIcon scp = obj.GetComponent<MinimapIcon>();
        scp.transform.SetParent(SceneData.Inst.myMinimap);
        scp.Initialize(transform, Color.red);

        ChangeState(STATE.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
