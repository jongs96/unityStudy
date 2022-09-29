using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : Character2DProperty
{
    public LayerMask myMask;
    public float MoveSpeed = 0.1f;
    Vector2 dir = Vector2.zero;
    public LayerMask myEnemy;
    public LayerMask Land;

    public ScrollBackGround myBG = null;

    public bool IsStop
    {
        get;set;
    }       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool bJump = false;
    private void FixedUpdate()
    {
        if (IsStop) return;

        if (myAnim.GetBool("IsAir") || !myAnim.GetBool("IsAttacking"))
        {
            float delta = Time.fixedDeltaTime * MoveSpeed;
            if (!myCollider.enabled)
            {
                float halfX = ((BoxCollider2D)myCollider).size.x * 0.5f;
                RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + myCollider.offset + dir * halfX,
                    dir, delta, Land);
                if (hit.collider != null)
                {
                    transform.position = hit.point + -dir * halfX - myCollider.offset;
                }
                else
                {
                    transform.Translate(dir * delta);
                    myBG?.OnMove(dir);
                }
            }
            else
            {
                transform.Translate(dir * delta);
                myBG?.OnMove(dir);
            }
            
        }
        if(bJump)
        {
            myRigid.AddForce(Vector3.up * 400.0f);
            bJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStop) return;

        if (!myAnim.GetBool("IsAir"))
        {
            dir = Vector2.zero;
            dir.x = Input.GetAxis("Horizontal");
        }
        dir.Normalize();
        if(dir.x > 0.0f)
        {
            myRenderer.flipX = false;
            myAnim.SetBool("IsMoving", true);
        }
        else if (dir.x < 0.0f)
        {
            myRenderer.flipX = true;           
            myAnim.SetBool("IsMoving", true);
        }
        else
        {
            myAnim.SetBool("IsMoving", false);
        }
        
        
        if(!myAnim.GetBool("IsAttacking") && Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger("Attack");
        }

        if(!myCollider.isTrigger && !myAnim.GetBool("IsAir") && Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                myCollider.isTrigger = true;
                myAnim.SetBool("IsAir", true);
            }
            else
            {
                myCollider.enabled = false;
                bJump = true;
                //myRigid.AddForce(Vector3.up * 400.0f);
                myAnim.SetTrigger("Jump");
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.1f, Land);  
        if(hit.collider != null)
        {
            myAnim.SetBool("IsAir", false);
            if (myRigid.velocity.y < 0.0f) myCollider.enabled = true;
            //if (myRigid.velocity.y < 0.0f) hit.transform.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            myAnim.SetBool("IsAir", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //myAnim.SetBool("IsAir", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //myAnim.SetBool("IsAir", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            myCollider.isTrigger = false;
            myAnim.SetBool("IsAir", false);
        }
    }

    public void OnAttack()
    {
        Vector2 pos = new Vector2(0.5f, myCollider.offset.y);
        if(myRenderer.flipX)
        {
            pos.x = -pos.x;
        }
        Collider2D[] list = Physics2D.OverlapCircleAll((Vector2)transform.position + pos, 0.5f, myEnemy);
        foreach(Collider2D col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(10.0f);
        }
    }
}
