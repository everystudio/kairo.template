using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageableTag
{
    protected List<IDamageable> iDamageableInterfaces = new List<IDamageable>();
    protected List<IInvulnerable> iInvulnerableInterfaces = new List<IInvulnerable>();
    protected List<IHealable> iHealableInterfaces = new List<IHealable>();
    protected List<IKillable> iKillableInterfaces = new List<IKillable>();

    public string damageableTag;
    public string DamageableTag => damageableTag;

    [SerializeField]
    protected float currentHP;
    public float MinHP
    {
        get { return currentHP; }
    }

    [SerializeField]
    protected float maxHP;
    public float MaxHP
    {
        get { return maxHP; }
    }

    [SerializeField]
    protected bool canBecomeInvulnerable = false;

    [SerializeField]
    private bool startInvulnerable;

    protected bool invulnerable;
    public void SetInvulnerable(bool state)
    {
        invulnerable = state;
    }

    [SerializeField] private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
    }

    private new Collider collider;
    public UnityEngine.Collider Collider
    {
        get { return collider; }
    }

    protected virtual void Initialize()
    {
        currentHP = maxHP;
        collider = this.gameObject.GetComponentInChildren<Collider>();

        if (startInvulnerable)
        {
            invulnerable = true;
        }
    }

    public void Revive()
    {
        Revive(maxHP);
    }

    public void Revive(float heal)
    {
        Heal(heal);
        isDead = false;
    }

    public void AddListener(IDamageable iDamageable)
    {
        if (iDamageable != null)
        {
            iDamageableInterfaces.Add(iDamageable);
        }
    }
    public void AddListener(IInvulnerable iInvulnerable)
    {
        if (iInvulnerable != null)
        {
            iInvulnerableInterfaces.Add(iInvulnerable);
        }
    }
    public void AddListener(IHealable iHealable)
    {
        if (iHealable != null)
        {
            iHealableInterfaces.Add(iHealable);
        }
    }
    public void AddListener(IKillable iKillable)
    {
        if (iKillable != null)
        {
            iKillableInterfaces.Add(iKillable);
        }
    }


    public void SetInvulnerable(float time, bool state = true)
    {
        if (canBecomeInvulnerable == false)
        {
            Debug.LogFormat("The stats component of {0} does not have canBecomeInvulnerable set as true.", this.transform.root);
            iInvulnerableInterfaces = new List<IInvulnerable>(GetComponentsInChildren<IInvulnerable>());
            canBecomeInvulnerable = true;
        }

        invulnerable = state;

        for (int i = 0; i < iInvulnerableInterfaces.Count; i++)
        {
            iInvulnerableInterfaces[i].BecameInvulnerable(state);
        }

        if (time == 0)
        {
            return;
        }

        StartCoroutine(RecoverVolnerabilityCoroutine(time));
    }

    private IEnumerator RecoverVolnerabilityCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        invulnerable = false;

        for (int i = 0; i < iInvulnerableInterfaces.Count; i++)
        {
            iInvulnerableInterfaces[i].BecameInvulnerable(false);
        }
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);

        for (int i = 0; i < iHealableInterfaces.Count; i++)
        {
            iHealableInterfaces[i].OnHealed(currentHP, maxHP);
        }
    }


    public void Kill()
    {
        Damage(currentHP, this.transform.position, null);
    }

    public void Damage(float amount, Vector3 hitlocation, GameObject cause)
    {
        Debug.Log("Damage;" + "invulnerable" + invulnerable + "isDead" + isDead + "amount" + amount + "hitlocation" + hitlocation + "cause" + cause + "this" + this + "this.transform.position" + this.transform.position + "this.transform.position");

        if (invulnerable)
        {
            return;
        }

        currentHP -= amount;
        if (!isDead)
        {
            for (int i = 0; i < iDamageableInterfaces.Count; i++)
            {
                iDamageableInterfaces[i].OnDamaged(new DamageInfo(currentHP, maxHP, hitlocation, (cause != null) ? cause.transform.position : Vector3.zero, (cause != null) ? cause.name : "", amount));
            }
        }

        if (currentHP <= 0 && !isDead)
        {
            for (int i = 0; i < iKillableInterfaces.Count; i++)
            {
                iKillableInterfaces[i].OnDeath(this);
            }

            isDead = true;
        }

    }

    public void IncreaseHealth(int amount)
    {
        maxHP += amount;

        for (int i = 0; i < iHealableInterfaces.Count; i++)
        {
            iHealableInterfaces[i].OnHealed(currentHP, maxHP);
        }
    }
    public void DecreaseHealth(int amount)
    {
        maxHP -= amount;

        for (int i = 0; i < iHealableInterfaces.Count; i++)
        {
            iHealableInterfaces[i].OnHealed(currentHP, maxHP);
        }
    }

    public bool IsDamaged { get { return currentHP < maxHP; } }
}

