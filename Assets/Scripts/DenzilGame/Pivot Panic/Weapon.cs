using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float knockbackAmount;
    public float timeTillAttack;
    public float attackRadius;
    public float attackRange;
}
