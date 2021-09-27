using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace steering
{
    [RequireComponent(typeof(Steering))]
    public class SimpleBrain : MonoBehaviour
    {
        public enum BehaviorMode
        {
            KEYBOARD,
            MOUSE,
            SEEK,
            SEEK_AVOID_OBJ,
            FLEE,
            PURSUE,
            EVADE,
            WANDER,
            WANDER_2,
            FOLLOW_PATH,
            HIDE,
            ARRIVE,
            NOT_SET = 0
        }

        [Header("manuel")]
        public BehaviorMode bm;
        public GameObject Target;
        public GameObject[] Targets;

        public Animator Ani;

        [Header("private")]
        private Steering Steerings;

        public SimpleBrain()
        {
            bm = BehaviorMode.NOT_SET;
            Target = null;
        }
        void Start()
        {
            if (bm == BehaviorMode.KEYBOARD || bm == BehaviorMode.MOUSE)
            {
                Target = null;
            }
            else if(Target == null)
            {
                Debug.Log("ik heb geen target: " + name);
            }

            Steerings = GetComponent<Steering>();

            List<Ibehavior> behaviors = new List<Ibehavior>();

            
            switch (bm)
            {
                case BehaviorMode.KEYBOARD:
                    behaviors.Add(new Keyboard(Ani));
                    Steerings.SetBehaviors(behaviors, "Keyboard");
                    break;
                case BehaviorMode.MOUSE:
                    behaviors.Add(new Mouse(gameObject));
                    Steerings.SetBehaviors(behaviors, "Mouse");
                    break;
                case BehaviorMode.SEEK:
                    behaviors.Add(new Seek(Target));
                    Steerings.SetBehaviors(behaviors, "Seek");
                    break;;
                case BehaviorMode.SEEK_AVOID_OBJ:
                    behaviors.Add(new Seek(Target));
                    behaviors.Add(new AvoidObj(gameObject));
                    Steerings.SetBehaviors(behaviors, "Seek and avoid");
                    break;
                case BehaviorMode.FLEE:
                    behaviors.Add(new Flee(Target));
                    Steerings.SetBehaviors(behaviors, "Flee");
                    break;
                case BehaviorMode.PURSUE:
                    behaviors.Add(new Pursue(Target));
                    Steerings.SetBehaviors(behaviors, "Pursue");
                    break;
                case BehaviorMode.EVADE:
                    behaviors.Add(new Evade(Target));
                    Steerings.SetBehaviors(behaviors, "Evade");
                    break;
                case BehaviorMode.WANDER:
                    behaviors.Add(new Wander());
                    Steerings.SetBehaviors(behaviors, "Wander");
                    break;
                case BehaviorMode.WANDER_2:
                    behaviors.Add(new Wander2());
                    Steerings.SetBehaviors(behaviors, "Wander2");
                    break;
                case BehaviorMode.ARRIVE:
                    behaviors.Add(new Arrive(Target));
                    Steerings.SetBehaviors(behaviors, "Arrive");
                    break;
                case BehaviorMode.FOLLOW_PATH:
                    behaviors.Add(new FollowPath(Targets,Random.Range(0,Targets.Length)));
                    Steerings.SetBehaviors(behaviors, "follow path");
                    break;
                case BehaviorMode.HIDE:
                    behaviors.Add(new Hide(Target));
                    Steerings.SetBehaviors(behaviors, "Hide");
                    break;
                
                    
                default:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
