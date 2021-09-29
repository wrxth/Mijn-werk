using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieAttack : MonoBehaviour
{
    [SerializeField] private Animator Ani;

    [SerializeField] private float AttackDistance;
    [SerializeField] private GameObject Player;
    [SerializeField] private int SelectedAttack;
    [SerializeField] private ParticleSystem FlameBreath;

    private bool FlameAttack;
    private bool ClawAttack;
    private bool BasicAttack;

    private bool Attackable;
    void Start()
    {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Idle01") || Ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        //{
        //    SelectedAttack = 0;
        //}
         //if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < AttackDistance)
         //{
         //   SelectedAttack = Random.Range(1, 4);
         //}
     

        if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Flame Attack"))
        {
            if(!FlameBreath.isPlaying) FlameBreath.Play();

            //Debug.Log("ping");
            FlameAttack = true;
            ClawAttack = false;
            BasicAttack = false;
        }
        else
        {
            if (FlameBreath.isPlaying) FlameBreath.Stop();
        }

        if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack"))
        {
            FlameAttack = false;
            ClawAttack = true;
            BasicAttack = false;
            Attackable = true;
        }
        else if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack"))
        {
            FlameAttack = false;
            ClawAttack = false;
            BasicAttack = true;
            Attackable = true;
        }
        else
        {
            Attackable = false;
        }

        Ani.SetInteger("attack", SelectedAttack);
        Ani.SetBool("Flamed", FlameAttack);
        Ani.SetBool("Basic", BasicAttack);
        Ani.SetBool("Claw", ClawAttack);
    }

    private IEnumerator Attack()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < AttackDistance)
        {
            int previousNumber = SelectedAttack;
            SelectedAttack = Random.Range(1, 4);
            if (previousNumber == SelectedAttack)
            {
                StopCoroutine(Attack());
                StartCoroutine(Attack());
                //Debug.Log("same number");
            }
        }
        yield return new WaitForSeconds(3f);

        StartCoroutine(Attack());
    }
}
