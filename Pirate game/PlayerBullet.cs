using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Canon Ball", menuName = "create canonball data")]
public class PlayerBullet : ScriptableObject
{
    public float CanonBallSpeed;
    public int Damage;

    public LayerMask TargetLayer;

    public GameObject explosion;
}
