using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace steering
{
    // gemaakt door: gabriel
    // de verschillende behaviors
    public enum BehaviorStatus
    {
        AGGRESSIVE,
        DEFENSIVE,
        GUARD_A,
        GUARD_B,
        LOYAL,
        WANDER
    }

    public class PlayUnit : MonoBehaviour ,Ipooled
    {
        [Header("Behavior tree vars")]
        [SerializeField] float evaluateTimer;

        // alle relevante ingame punten(locaties in feiten) voor een unit
        [SerializeField] private GameObject[] DefensePoints; 
        [SerializeField] private GameObject[] GuardA; 
        [SerializeField] private GameObject[] GuardB; 
        public GameObject[] Destinations;
        public GameObject HealLocation;

        // het type movement dat de unit mee bezig is example: chasing, fleeing , going to heal etc.
        public string CurrentMoveType;

        // vars het steering systeem
        [Header("behavior info")]
        public Steering Steerings;
        public List<Ibehavior> Behaviors = new List<Ibehavior>();
        public BehaviorStatus bs;

        // vars om andere npcs and units te herkenen en te onthouden
        [Header("other npc identifier")]
        [SerializeField] private LayerMask Enemies,Allies;

        public Collider[] FriendColliders;
        public Collider[] EnemieColliders;

        // vars van de stats van de units
        [Header("Stats")]
        // de base stats van een unit
        [SerializeField] private float BaseAttackRange;
        [SerializeField] private float BaseAtkInterval;
        [SerializeField] private float MaxWanderDistance;
        [SerializeField] private int BaseDamage;

        // de stats na buffs
        private float AtkTime;
        private float AtkInterval;
        private int Damage;        
        private float AttackRange;

        // herkenen of de unit buffed is
        public bool DamageBuffed = false;

        // willekeurige vars die nodig zijn voor het correct werken van het script
        [Header("Misc")]
        public bool IsPlayerUnit;

        private Health H;
        private Node TopNode;                               // de toplaag van de behavior tree

        [SerializeField] private TMP_Text BehaviorText;
        public void OnObjectSpawn()
        {

            if (IsPlayerUnit == true)
            {
                // buffs toevoege
                Damage = BaseDamage + UpgradeData.Instance.DamageBuff;
                AttackRange = BaseAttackRange + UpgradeData.Instance.AtkRange;
                AtkInterval = BaseAtkInterval + UpgradeData.Instance.AtkInterval;

                // guard point a 
                GuardA = GameObject.FindGameObjectsWithTag("GA");

                // guard point b 
                GuardB = GameObject.FindGameObjectsWithTag("GB");

                // aggresive locations
                Destinations = GameObject.FindGameObjectsWithTag("DesP");

                // defensepoints
                DefensePoints = GameObject.FindGameObjectsWithTag("defP");

                // heal locatie
                HealLocation = GameObject.FindGameObjectWithTag("heal");

            }
            else
            {
                // stats assignen, als de unit niet van de player is krijgt hij geen buffs hierdoor kan best hogere basestats hebben
                Damage = BaseDamage;
                AttackRange = BaseAttackRange;
                AtkInterval = BaseAtkInterval;

                // guard point a 
                GuardA = GameObject.FindGameObjectsWithTag("GA");
               

                // guard point b 
                GuardB = GameObject.FindGameObjectsWithTag("GB");


                // aggresive locations
                Destinations = GameObject.FindGameObjectsWithTag("DesE");

                // defensepoints
                DefensePoints = GameObject.FindGameObjectsWithTag("defE");

                // heal locatie
                HealLocation = GameObject.FindGameObjectWithTag("heal");
            }

            // references naar andere scripts
            H = gameObject.GetComponent<Health>();
            Steerings = GetComponent<Steering>();

            // de behavior tree die de unit gebruikt constructen
            ConstructBehaviorTree();
        }

        private void ConstructBehaviorTree()
        {
            // behavior tree nodes

            // general nodes
            HealNode healNode = new HealNode(this);
            HealthNode healthNode = new HealthNode(H, H.LowHealthPoint);

            EnemieDetectionNode enemieDetectionNode = new EnemieDetectionNode(this);
            ChaseNode chaseNode = new ChaseNode(this);

            OddsCheckNode oddsCheckNode = new OddsCheckNode(this);
            FleeNode fleeNode = new FleeNode(this);

            // behavior identiefier nodes
            IsAgressiveNode isAgressiveNode = new IsAgressiveNode(this);
            IsDefensiveNode isDefensiveNode = new IsDefensiveNode(this);
            IsLoyalNode isLoyalNode = new IsLoyalNode(this);
            IsGuardANode isGuardANode = new IsGuardANode(this);
            IsGuardBNode isGuardBNode = new IsGuardBNode(this);
            IsWanderNode isWanderNode = new IsWanderNode(this);

            // aggressive nodes
            HaveDestinationNode haveDestinationNode = new HaveDestinationNode(this);
            GoToDesNode goToDesNodeAgro = new GoToDesNode(this);

            // guard nodes
            Guardnode guardANode = new Guardnode(this,GuardA, gameObject);
            Guardnode guardBNode = new Guardnode(this,GuardB ,gameObject);

            GuardDistanceNode guardADistanceNode = new GuardDistanceNode(GuardA, MaxWanderDistance,gameObject);
            GuardDistanceNode guardBDistanceNode = new GuardDistanceNode(GuardB, MaxWanderDistance,gameObject);
            
            // defensive nodes
            int defensetarget = UnityEngine.Random.Range(0, DefensePoints.Length);
            DistanceNode distanceDefNode = new DistanceNode(DefensePoints[defensetarget], MaxWanderDistance,gameObject);
            ReturnToPoint returnToDefPointNode = new ReturnToPoint(DefensePoints[defensetarget], this);

            // loyal nodes
            ReturnToFriendNode returnToFriendNode = new ReturnToFriendNode(this);
            DistanceLoyalNode distanceLoyalNode = new DistanceLoyalNode(this, MaxWanderDistance);
            NonLoyalAllyNode nonLoyalAllyNode = new NonLoyalAllyNode(this, 0);

            // wander nodes
            wanderNode wanderNode = new wanderNode(this);

            // behavior tree logic

            // general sequences
            Sequence chaseSequence = new Sequence(new List<Node> { chaseNode });
            Sequence enemieDetectSequence = new Sequence(new List<Node> { enemieDetectionNode ,chaseSequence });

            Sequence goHealSequence = new Sequence(new List<Node> { enemieDetectionNode, healNode });
            Sequence checkHealthSequence = new Sequence(new List<Node> { healthNode, goHealSequence });

            Sequence fleeSequence = new Sequence(new List<Node> { fleeNode });
            Sequence outnumberdSequence = new Sequence(new List<Node> { oddsCheckNode, fleeSequence });

            // agressive sequences en selector
            Sequence goToDesSequence = new Sequence(new List<Node> { goToDesNodeAgro });
            Sequence haveDesSequence = new Sequence(new List<Node> { haveDestinationNode, goToDesSequence });

            Selector agressiveSelector = new Selector(new List<Node> { enemieDetectSequence, haveDesSequence });
            Sequence agressiveSequence = new Sequence(new List<Node> { isAgressiveNode, agressiveSelector });

            // defensive sequences en selector
            Sequence ToDefPointSequence = new Sequence(new List<Node> { returnToDefPointNode });
            Sequence DistanceCheckSequence = new Sequence(new List<Node> {distanceDefNode, ToDefPointSequence });

            Selector defensiveSelector = new Selector(new List<Node> { DistanceCheckSequence, enemieDetectSequence});
            Sequence defensiveSequence = new Sequence(new List<Node> { isDefensiveNode, defensiveSelector });

            // Loyal sequences en selector
            Sequence toLoyalTargetSequence = new Sequence(new List<Node> { returnToFriendNode });
            Sequence distanceToAllySequence = new Sequence(new List<Node> { nonLoyalAllyNode, distanceLoyalNode, toLoyalTargetSequence });

            Selector loyalSelector = new Selector(new List<Node> { distanceToAllySequence, enemieDetectSequence });
            Sequence loyalSequence = new Sequence(new List<Node> { isLoyalNode, loyalSelector });

            // Guard A sequences en selector
            Sequence pathASequence = new Sequence(new List<Node> { guardANode });
            Sequence PathADistanceSequence = new Sequence(new List<Node> { guardADistanceNode, pathASequence });

            Selector GuardASelector = new Selector(new List<Node> { PathADistanceSequence, enemieDetectSequence });
            Sequence GuardASequence = new Sequence(new List<Node> { isGuardANode, GuardASelector }); 
            
            // Guard B sequences en selector
            Sequence pathBSequence = new Sequence(new List<Node> { guardBNode });
            Sequence PathBDistanceSequence = new Sequence(new List<Node> { guardBDistanceNode, pathBSequence });

            Selector GuardBSelector = new Selector(new List<Node> { PathBDistanceSequence, enemieDetectSequence });
            Sequence GuardBSequence = new Sequence(new List<Node> { isGuardBNode, GuardBSelector });

            // wander sequences en selector
            Sequence wanderingSequence = new Sequence(new List<Node> { wanderNode });

            Selector wanderSelector = new Selector(new List<Node> { enemieDetectSequence, wanderingSequence });
            Sequence wanderSequence = new Sequence(new List<Node> { isWanderNode, wanderSelector });

            // bovenste laag van de behavior tree
            TopNode = new Selector(new List<Node> { checkHealthSequence, outnumberdSequence, agressiveSequence, defensiveSequence, loyalSequence ,GuardASequence, GuardBSequence, wanderSequence});
        }
        
        void Update()
        {
            // de ingame identifier van de behavior state  goed zetten
            switch (bs)
            {
                case BehaviorStatus.AGGRESSIVE:
                    BehaviorText.text = "Aggresive";
                    break;
                case BehaviorStatus.DEFENSIVE:
                    BehaviorText.text = "Defensive";
                    break;
                case BehaviorStatus.GUARD_A:
                    BehaviorText.text = "Guard A";
                    break;
                case BehaviorStatus.GUARD_B:
                    BehaviorText.text = "Guard B";
                    break;
                case BehaviorStatus.LOYAL:
                    BehaviorText.text = "Loyal";
                    break;
                case BehaviorStatus.WANDER:
                    BehaviorText.text = "Wander";
                    break;
                default:
                    break;
            }

            // om preformance te verbeteren een limiter door hoe vaak erdoor de behavior tree wordt gegaan
            evaluateTimer += Time.deltaTime;
            if (evaluateTimer > 1)
            {
                TopNode.Evalute();                 // door de behavior tree gaan
                evaluateTimer = 0;
            }
            
            // andere npcs herkenen en op de goed plek onthouden
            EnemieColliders = Physics.OverlapSphere(gameObject.transform.position,10f,Enemies);
            FriendColliders = Physics.OverlapSphere(gameObject.transform.position,10f, Allies);

            // het attack gedeelte van het scripr
            if (EnemieColliders.Length != 0)
            {
                Health id = EnemieColliders[0].GetComponent<Health>();        // reference naar het health script van de relevante tegenstander

                if (Vector3.Distance(gameObject.transform.position, EnemieColliders[0].transform.position) < AttackRange && id != null)
                {
                    // de attack interval
                    AtkTime += Time.deltaTime;
                    if (AtkTime > AtkInterval)
                    {
                        id.TakeDamage(Damage);               // de damage functie oproepen
                        AtkTime = 0;
                    }
                }
            }
        }

        // de damage buff toevoegen
        public void BuffDamage()
        {
            if (DamageBuffed == false)
            {
                Damage += 5;
                DamageBuffed = true;
            }
        }

    }
}
