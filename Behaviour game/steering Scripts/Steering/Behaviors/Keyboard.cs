using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering  
{

    
    public class Keyboard : Behavior
    {
        [SerializeField] private bool IsRunning;

        [SerializeField] private float IdleTime;


        private Animator Animators;
        
        public Keyboard(Animator _me)
        {
            Animators = _me.GetComponent<Animator>();
        }
        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext _context)
        {
            Vector3 requestedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            IsRunning = Input.GetKey(KeyCode.LeftShift);
            if (IsRunning == false)
            {
                if (requestedDirection != Vector3.zero)
                {
                    PositionTarget = _context.Position + requestedDirection.normalized * _context.sd.MaxDesiredVelocity;
                    IdleTime = 0;
                    Animators.SetFloat("Speed", 1);
                }
                else
                {
                    Animators.SetFloat("Speed", 0);
                    IdleTime += Time.fixedDeltaTime;
                    PositionTarget = _context.Position;
                }
            }
            else
            {

                if (requestedDirection != Vector3.zero)
                {
                    PositionTarget = _context.Position + requestedDirection.normalized * _context.sd.MaxDesiredVelocity * 1.5f;
                    Animators.SetFloat("Speed", 1);
                }
                else
                {
                    Animators.SetFloat("Speed", 0);
                    PositionTarget = _context.Position;
                }
            }

            Animators.SetBool("Running",IsRunning);
            
            Animators.SetBool("HandsOnHips",IdleTime >5f);


            Debug.Log(IsRunning);
            Debug.Log(IdleTime);

            VelocityDesired = (PositionTarget - _context.Position).normalized * _context.sd.MaxDesiredVelocity;
            return VelocityDesired - _context.Velocity;
        }
    } 
}
