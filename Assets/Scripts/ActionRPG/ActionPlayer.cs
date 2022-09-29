using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPlayer : CharacterProperty
{
    public LayerMask EnemyMask = default;
    Vector2 desireDir = Vector2.zero;
    Vector2 curDir = Vector2.zero;
    public Transform myHitPosition;
    bool IsAir = false;
    bool IsComboable = false;
    int ClickCount = 0;

    public SkillIcon mySkill;
    public SkillIcon mySkill2;
    // Start is called before the first frame update
    void Start()
    {
        mySkill.myAction.AddListener(JumpAttack);
        mySkill2.myAction.AddListener(JumpAttack);
        GameObject obj = Instantiate(Resources.Load("Prefabs/MinimapIcon")) as GameObject;
        MinimapIcon scp = obj.GetComponent<MinimapIcon>();
        scp.transform.SetParent(SceneData.Inst.myMinimap);
        scp.Initialize(transform, Color.blue);
    }

    private void FixedUpdate()
    {
        if(!IsAir) myRigid.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        desireDir.x = Input.GetAxis("Horizontal");
        desireDir.y = Input.GetAxis("Vertical");

        curDir.x = Mathf.Lerp(curDir.x, desireDir.x, Time.deltaTime * 10.0f);
        curDir.y = Mathf.Lerp(curDir.y, desireDir.y, Time.deltaTime * 10.0f);

        myAnim.SetFloat("x", curDir.x);
        myAnim.SetFloat("y", curDir.y);

        if(!myAnim.GetBool("IsAttacking") && Input.GetKeyDown(KeyCode.F1))
        {
            mySkill.OnAction();
        }

        if(!myAnim.GetBool("IsAttacking"))
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //myAnim.SetInteger("ComboIndex", 0);
                myAnim.SetTrigger("ComboAttack");
            }
        }
        /*
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsComboable)
                {
                    int i = myAnim.GetInteger("ComboIndex");                    
                    myAnim.SetInteger("ComboIndex", i + 1);
                    myAnim.SetTrigger("ComboAttack");
                }                
            }
        }
        */

        if(IsComboable)
        {
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                ClickCount++;
            }
        }

        if(!IsAir && Input.GetKeyDown(KeyCode.Space))
        {
            IsAir = true;
            myRigid.AddForce(Vector3.up * 300.0f);
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPosition.position, 0.5f, EnemyMask);
        foreach(Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(35.0f);
        }
    }

    public void OnSkill()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 2.0f, EnemyMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(50.0f);
        }
    }

    public void JumpAttack()
    {
        myAnim.SetTrigger("JumpAttack");
    }

    public void OnComboCheck(bool v)
    {
        IsComboable = v;
        if(v)
        {
            //ComboCheckStart
            ClickCount = 0;
        }
        else
        {
            //ComboCheckEnd
            if(ClickCount != 1)
            {
                myAnim.SetTrigger("ComboStop");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsAir = true;
        }
    }
}
