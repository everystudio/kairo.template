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

    // この初期化あまり良くないけど
    public void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        hitCount = 0;
        attackedInstanceIDs.Clear();
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

        if (!IsAttackedInstance(getInstanceID) && HasDamageableTag(health.tag))
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
    private void ApplyDamage(Collider2D other)
    {
        Health getHealth = other.GetComponent<Health>();
        if (getHealth != null)
        {
            float getDamage = RequestDamage(getHealth);
            if (getDamage != 0)
            {
                hitCount += 1;
                Vector3 hitLocation = collider.bounds.ClosestPoint(((other.transform.position - this.transform.position) * 0.5f) + this.transform.position);
                getHealth.Damage(getDamage, hitLocation, owner);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HasDamageableTag(other.tag))
        {
            //Debug.Log("Trigger:" + other.gameObject.name);
            ApplyDamage(other);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasDamageableTag(collision.gameObject.tag))
        {
            ApplyDamage(collision.collider);
        }
    }
}
