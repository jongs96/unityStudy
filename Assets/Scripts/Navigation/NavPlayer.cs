using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayer : CharacterProperty
{
    public LayerMask pickMask;
    public NavMeshAgent myNav;
    Coroutine coMove = null;
    NavMeshPath myPath = null;
    public float MoveSpeed = 2.0f;
    public float RotSpeed = 360.0f;
    // Start is called before the first frame update
    void Start()
    {
        myPath = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickMask))
            {
                if (!myAnim.GetBool("IsJumping"))
                {
                    //if (coMove != null) StopCoroutine(coMove);
                    //coMove = StartCoroutine(Moving(hit.point));

                    if(NavMesh.CalculatePath(transform.position, hit.point, 1 << NavMesh.GetAreaFromName("Walkable"), myPath))
                    {
                        if (coMove != null) StopCoroutine(coMove);
                        coMove = StartCoroutine(Moving(myPath.corners));
                    }
                }                
            }    
        }        

        for(int i = 0; i < myPath.corners.Length - 1; ++i)
        {
            Debug.DrawLine(myPath.corners[i], myPath.corners[i + 1], Color.red);
        }
    }

    IEnumerator Moving(Vector3[] poslist)
    {
        if (poslist.Length <= 1) yield break;
        Coroutine coRot = null;
        int nextPos = 1;
        myAnim.SetBool("IsMoving", true);
        while(nextPos < poslist.Length)
        {
            Vector3 dir = poslist[nextPos] - poslist[nextPos - 1];
            float dist = dir.magnitude;
            dir.Normalize();

            if (coRot != null) StopCoroutine(coRot);
            coRot = StartCoroutine(CharacterMovement.Rotating(transform, poslist[nextPos], RotSpeed));

            while(dist > Mathf.Epsilon)
            {
                float delta = Time.deltaTime * MoveSpeed;
                if(delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
                yield return null;
            }
            nextPos++;
        }
        myAnim.SetBool("IsMoving", false);
    }

    IEnumerator Moving(Vector3 pos)
    {
        //myNav.destination = pos;
        myNav.SetDestination(pos);
        myAnim.SetBool("IsMoving", true);
        while(myNav.pathPending)
        {
            yield return null;
        }

        switch(myNav.pathStatus)
        {
            case NavMeshPathStatus.PathComplete:
                Debug.Log("갈수있다.");
                break;
            case NavMeshPathStatus.PathInvalid:
                Debug.Log("갈수없다.");
                break;
            case NavMeshPathStatus.PathPartial:
                Debug.Log("끝까진 못간다.");
                break;
        }        

        while (myNav.remainingDistance > myNav.stoppingDistance)
        {
            if(myNav.isOnOffMeshLink)
            {
                myNav.isStopped = true;
                yield return StartCoroutine(CharacterMovement.Rotating(transform,myNav.currentOffMeshLinkData.endPos, myNav.angularSpeed));
                myNav.enabled = false;                                
                myAnim.SetTrigger("Jump");
                myAnim.SetBool("IsJumping", true);
                while(myAnim.GetBool("IsJumping"))
                {
                    yield return null;
                }
                myNav.enabled = true;
                myNav.CompleteOffMeshLink();
                myNav.Warp(transform.position);
                myNav.isStopped = false;
                myNav.SetDestination(pos);
                while (myNav.pathPending)
                {
                    yield return null;
                }
            }
            yield return null;
        }
        myAnim.SetBool("IsMoving", false);
    }    
}
