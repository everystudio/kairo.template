using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    private new Collider2D collider;

    [SerializeField]
    private string[] damageableTags;
    [SerializeField] private float damage;

    protected bool canDealDamage = true;
    private GameObject owner;
    private IDamageCallback callbackInterface;
    private List<int> attackedInstanceIDs = new List<int>();

    private int hitCount = 0;

    private void Start()
    {
        // いったん自分自身をオーナーにする
        owner = this.gameObject;
        collider = GetComponent<Collider2D>();
    }
    /*
    // この初期化あまり良くないけど
    public void OnEnable()
    {
        Refresh();
    }
    */

    public void Disable()
    {
        canDealDamage = false;
    }

    public void Refresh()
    {
        canDealDamage = true;
        hitCount = 0;
        attackedInstanceIDs.Clear();
    }

    public void SetDamageableTags(string[] tags)
    {
        damageableTags = tags;
    }


    public void SetCallBack(IDamageCallback callbackInterface)
    {
        this.callbackInterface = callbackInterface;
    }

    private bool HasDamageableTag(string targetTag)
    {
        // タグがなにもない場合は全部にダメージが通る
        if (damageableTags.Length == 0)
        {
            return true;
        }

        for (int i = 0; i < damageableTags.Length; i++)
        {
            if (targetTag == damageableTags[i])
            {
                return true;
            }
        }

        return false;
    }

    public float RequestDamage(Health health)
    {
        if (!canDealDamage || health.IsDead)
        {
            return 0;
        }

        int getInstanceID = health.gameObject.GetInstanceID();

        if (!IsAttackedInstance(getInstanceID) && HasDamageableTag(health.DamageableTag))
        {
            attackedInstanceIDs.Add(getInstanceID);

            if (callbackInterface != null)
            {
                DamageInfo damageInfo = new DamageInfo
                    (
                    health.MinHP,
                    health.MaxHP,
                    transform.position,
                    health.transform.position,
                    health.gameObject.name,
                    damage
                    );

                callbackInterface.OnDamageDone(health, damageInfo);
            }

            OnDamageRequested(health);

            return damage;
        }

        return 0;
    }

    private bool IsAttackedInstance(int instanceId)
    {
        // 現在は１個にしかインタラクションしない
        // ここで弾くのは適当ではないので、後で直す
        if (0 < hitCount)
        {
            return true;
        }

        foreach (var id in attackedInstanceIDs)
        {
            if (id == instanceId)
            {
                return true;
            }
        }
        return false;
    }

    public virtual void OnDamageRequested(Health health) { }
    private void ApplyDamage(Health health)
    {
        if (health != null)
        {
            float getDamage = RequestDamage(health);
            if (getDamage != 0)
            {
                hitCount += 1;
                Vector3 hitLocation = collider.bounds.ClosestPoint(((health.transform.position - this.transform.position) * 0.5f) + this.transform.position);
                health.Damage(getDamage, hitLocation, owner);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Health health))
        {
            //Debug.Log("Trigger:" + other.gameObject.name);
            if (HasDamageableTag(health.DamageableTag))
            {
                ApplyDamage(health);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            //Debug.Log("Collision:" + collision.gameObject.name);
            if (HasDamageableTag(health.DamageableTag))
            {
                ApplyDamage(health);
            }
        }
    }
}
