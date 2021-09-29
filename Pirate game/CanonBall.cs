using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour ,Ipooled
{

    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerBullet pb;
    public void OnObjectSpawn()
    {
        rb.AddForce(transform.forward * pb.CanonBallSpeed);
        Invoke("TurnOff",2f);
    }

    
    void Update()
    {
        RaycastHit hitForward;
        if (Physics.Raycast(transform.position, transform.forward, out hitForward, 1, pb.TargetLayer))
        {
            Instantiate(pb.explosion,gameObject.transform.position, gameObject.transform.rotation);
            ITakeDamage td = hitForward.collider.GetComponent<ITakeDamage>();

            if (td != null)
            {
                td.TakeDamage(pb.Damage);
            }
            Debug.Log("hit: " + hitForward.collider.name);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            
        }
    }

    private void TurnOff()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
