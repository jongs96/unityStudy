using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : CharacterMovement, IBattle
{
    public enum STATE { CREATE, NORMAL, BATTLE, DEATH }
    public STATE myState = STATE.CREATE;
    public LayerMask CrashMask;
    public LayerMask AttackMask;
    public CharacterStat myInfo;
    public Transform myTargetPosition = null;

    IBattle myTarget = null;

    public Transform HeadPos
    {
        get;
    }
    public bool IsLive
    {
        get
        {
            if(Mathf.Approximately(myInfo.CurHP,0.0f))
            {
                return false;
            }
            return true;
        }
    }
    public void OnDamage(float dmg)
    {
        myInfo.CurHP -= dmg;
        if (Mathf.Approximately(myInfo.CurHP, 0.0f))
        {
            ChangeState(STATE.DEATH);            
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
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
        if (myTarget != null)
        {
            if (myTarget.IsLive) myTarget.OnDamage(50.0f);
        }
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.CREATE:
                break;
            case STATE.NORMAL:                
                myTarget = null;
                StopAllCoroutines();
                break;
            case STATE.BATTLE:
                myTargetPosition.gameObject.SetActive(true);
                FollowTarget(myTarget.transform, myInfo.MoveSpeed, myInfo.RotSpeed, OnAttack);
                break;
            case STATE.DEATH:
                StopAllCoroutines();
                myAnim.SetTrigger("Death");
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.NORMAL:                
                myInfo.curAttackDelay += Time.deltaTime;
                InputProcess();
                break;
            case STATE.BATTLE:
                if(!myAnim.GetBool("IsAttacking")) myInfo.curAttackDelay += Time.deltaTime;
                InputProcess();
                if (myTarget != null && !myTarget.IsLive)
                {
                    myTargetPosition.gameObject.SetActive(false);
                    ChangeState(STATE.NORMAL);
                }
                if(myTarget!= null && myTargetPosition.gameObject.activeSelf) myTargetPosition.position = myTarget.HeadPos.position;
                break;
            case STATE.DEATH:
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.NORMAL);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();        
    }    

    void InputProcess()
    {        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, CrashMask))
            {
                if (myState == STATE.BATTLE)
                {                       
                    ChangeState(STATE.NORMAL);
                }
                if (myAnim.GetBool("IsAttacking"))
                {
                    myAnim.SetBool("IsAttacking", false);
                    myAnim.SetTrigger("Move");
                }
                myTargetPosition.gameObject.SetActive(true);
                myTargetPosition.position = hit.point;
                MoveToPosition(hit.point, myInfo.MoveSpeed, myInfo.RotSpeed,() => myTargetPosition.gameObject.SetActive(false));
            }
        }
        if (Input.GetMouseButtonDown(1) && !myAnim.GetBool("IsAttacking"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, AttackMask))
            {
                myTarget = hit.transform.GetComponent<IBattle>();
                if(myTarget != null) ChangeState(STATE.BATTLE);
            }
        }
    }
}
