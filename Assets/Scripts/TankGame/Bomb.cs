using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject orgEffect = null;
    public float Speed = 5.0f;    
    public LayerMask CrashMask;
    public LayerMask StaticMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {             
    }

    IEnumerator Moving()
    {
        float TotalDist = 0.0f;
        while (TotalDist < 1000.0f)
        {
            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = transform.forward;
            float delta = Speed * Time.deltaTime;
            TotalDist += delta;            
            if (Physics.Raycast(ray, out RaycastHit hit, delta, CrashMask | StaticMask))
            {
                transform.position = hit.point;
                OnExplosion();
                if ((CrashMask & 1 << hit.transform.gameObject.layer) != 0)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            else
            {
                transform.Translate(Vector3.forward * delta);
            }
            yield return null;
        }
        Destroy(gameObject);
    }

    public void OnFire()
    {        
        //transform.parent = null;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Moving());
    }

    private void OnCollisionEnter(Collision collision)
    {        
    }

    private void OnCollisionStay(Collision collision)
    {        
    }

    private void OnCollisionExit(Collision collision)
    {        
    }

    private void OnTriggerEnter(Collider other)
    {
        return;
        if ((StaticMask & 1 << other.gameObject.layer) != 0)
        {
            OnExplosion();
        }
        else if ((CrashMask & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject);
            OnExplosion();
        }
    }

    void OnExplosion()
    {
        GameObject obj = Instantiate(orgEffect);
        obj.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
