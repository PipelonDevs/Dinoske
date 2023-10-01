using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVals : ScriptableObject
{
    public float attackForce = 10.0f;
    public float attackCooldown = 1.0f;
    public CapsuleCollider2D attackHitbox;
    public LayerMask enemyLayer;
    public int damage = 10;
    
}
