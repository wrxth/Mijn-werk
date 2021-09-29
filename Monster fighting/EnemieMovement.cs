using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemieMovement : MonoBehaviour
{
    private CharacterController Control;
    [SerializeField] private GameObject Player;
    [SerializeField] private float DistanceToPlayer;
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float StopDistance = 2f;

    [SerializeField] private float TurnSmoothTime = 0.1f;
    [SerializeField] private float SmoothVelocity;
    private bool Running;
    [SerializeField] private Animator Ani;
    void Start()
    {
        Control = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        Vector3 dir = Player.transform.position - gameObject.transform.position;

        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref SmoothVelocity, TurnSmoothTime);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        

        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) > StopDistance)
        {
            Control.Move(moveDir.normalized * Speed * Time.deltaTime);
            Running = true;
        }
        else
        {
            Running = false;
        }

        if (Ani.GetCurrentAnimatorStateInfo(0).IsName("Idle01") || Ani.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        Ani.SetBool("running", Running);
    }
}
