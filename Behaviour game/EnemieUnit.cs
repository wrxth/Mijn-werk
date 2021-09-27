using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace steering
{


    public class EnemieUnit : MonoBehaviour
    {

        public enum BehaviorStatus
        {
            AGGRESIVE,
            DEFENSIVE,

        }

        private bool MouseControlAdded;



        private Steering Steerings;

        List<Ibehavior> Behaviors = new List<Ibehavior>();

        private BehaviorStatus bs;

        [SerializeField] private LayerMask Enemies;

        [SerializeField] private GameObject EnemyBase;
        [SerializeField] private Collider[] hitColliders;

        [SerializeField] private float AttackRange;
        [SerializeField] private int Damage;

        private float AtkTime;
        [SerializeField] private float AtkInterval;
        void Start()
        {
            EnemyBase = GameObject.FindGameObjectWithTag("PlayerBase");

            Steerings = GetComponent<Steering>();
            BehaviorManager.Instance.OnAggresive += AggresiveListener;
            BehaviorManager.Instance.OnDefensive += DefensiveListener;

            Behaviors.Add(new Seek(EnemyBase));
            Behaviors.Add(new AvoidObj(gameObject));
            Steerings.SetBehaviors(Behaviors, "Aggresive");
            MouseControlAdded = true;
        }


        void Update()
        {
            hitColliders = Physics.OverlapSphere(gameObject.transform.position, 10f, Enemies);

            if (bs == BehaviorStatus.AGGRESIVE && hitColliders.Length > 0)
            {
                Debug.Log("Ping");
                Behaviors.Clear();
                Behaviors.Add(new Seek(hitColliders[0].gameObject));
                Behaviors.Add(new AvoidObj(gameObject));
                Steerings.SetBehaviors(Behaviors, "Aggresive");
                MouseControlAdded = false;
                ITakeDamage id = hitColliders[0].GetComponent<ITakeDamage>();

                if (Vector3.Distance(gameObject.transform.position, hitColliders[0].transform.position) < AttackRange)
                {
                    AtkTime += Time.deltaTime;
                    if (AtkTime > AtkInterval)
                    {
                        id.TakeDamage(Damage);
                        AtkTime = 0;
                    }

                }
            }
            else if (bs == BehaviorStatus.AGGRESIVE && MouseControlAdded == false)
            {
                Behaviors.Clear();
                Behaviors.Add(new Seek(EnemyBase));
                Behaviors.Add(new AvoidObj(gameObject));
                Steerings.SetBehaviors(Behaviors, "Aggresive");
                MouseControlAdded = true;
            }
        }

        private void AggresiveListener()
        {
            if (gameObject.GetComponent<SelectionObj>() != null)
            {
                bs = BehaviorStatus.AGGRESIVE;
                Behaviors.Clear();
                Behaviors.Add(new Seek(EnemyBase));
                Behaviors.Add(new AvoidObj(gameObject));
                Steerings.SetBehaviors(Behaviors, "Aggresive");
                MouseControlAdded = true;


            }
        }
        private void DefensiveListener()
        {

        }


    }
}


