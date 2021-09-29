using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy data", menuName = "create enemy data")]
public class EnemyData : ScriptableObject
{


    public float MaxDistanceFromPlayer;

    public float TimeBetweenShots;
    public int AttackRange;
    public LayerMask PlayerLayer;
}
