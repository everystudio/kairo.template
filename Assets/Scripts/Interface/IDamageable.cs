using UnityEngine;

public struct DamageInfo
{
    float minHP;
    public float MinHP
    {
        get { return minHP; }
    }

    float maxHP;
    public float MaxHP
    {
        get { return maxHP; }
    }

    Vector3 hitLocation;
    public UnityEngine.Vector3 Hitlocation
    {
        get { return hitLocation; }
    }

    Vector3 causeLocation;
    public UnityEngine.Vector3 Causelocation
    {
        get { return causeLocation; }
    }

    string attackerName;
    public string AttackerName
    {
        get { return attackerName; }
    }

    float amount;
    public float Amount
    {
        get { return amount; }
    }

    public DamageInfo(float minHP, float maxHP, Vector3 hitLoc, Vector3 causeLoc, string attackerName, float amount)
    {
        this.minHP = minHP;
        this.maxHP = maxHP;
        hitLocation = hitLoc;
        causeLocation = causeLoc;
        this.attackerName = attackerName;
        this.amount = amount;
    }
}

public interface IDamageable
{
    void OnDamaged(DamageInfo damageInfo);
}