using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected List<IDamageable> iDamageableInterfaces = new List<IDamageable>();
    protected List<IInvulnerable> iInvulnerableInterfaces = new List<IInvulnerable>();
    protected List<IHealable> iHealableInterfaces = new List<IHealable>();
    protected List<IKillable> iKillableInterfaces = new List<IKillable>();
    [SerializeField]
    protected float minHP;
    public float MinHP
    {
        get { return minHP; }
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

    private bool isDead;
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
        minHP = maxHP;
        collider = this.gameObject.GetComponentInChildren<Collider>();

        if (startInvulnerable)
        {
            invulnerable = true;
        }
    }

    public void Revive()
    {
        minHP = maxHP;
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
        minHP = Mathf.Clamp(minHP + amount, 0, maxHP);

        for (int i = 0; i < iHealableInterfaces.Count; i++)
        {
            iHealableInterfaces[i].OnHealed(minHP, maxHP);
        }
    }


    public void Kill()
    {
        Damage(minHP, this.transform.position, null);
    }

    public void Damage(float amount, Vector3 hitlocation, GameObject cause)
    {
        if (invulnerable)
        {
            return;
        }

        minHP -= amount;
        if (!isDead)
        {
            for (int i = 0; i < iDamageableInterfaces.Count; i++)
            {
                iDamageableInterfaces[i].OnDamaged(new DamageInfo(minHP, maxHP, hitlocation, (cause != null) ? cause.transform.position : Vector3.zero, (cause != null) ? cause.name : "", amount));
            }
        }

        if (minHP <= 0 && !isDead)
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
            iHealableInterfaces[i].OnHealed(minHP, maxHP);
        }
    }
    public void DecreaseHealth(int amount)
    {
        maxHP -= amount;

        for (int i = 0; i < iHealableInterfaces.Count; i++)
        {
            iHealableInterfaces[i].OnHealed(minHP, maxHP);
        }
    }

    public bool IsDamaged { get { return minHP < maxHP; } }
}
