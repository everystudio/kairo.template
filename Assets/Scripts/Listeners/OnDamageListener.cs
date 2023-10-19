using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDamageListener : MonoBehaviour, IDamageable
{
    [SerializeField] private Health[] targets;
    [SerializeField] private float delay;

    private UnityEvent<float> onHealthChanged = new UnityEvent<float>();
    public UnityEvent OnDamagedEvent = new UnityEvent();

    private void Awake()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].AddListener(this);
        }
    }

    public void OnDamaged(DamageInfo damageInfo)
    {
        StartCoroutine(DispatchAction(damageInfo.Causelocation, damageInfo));
    }

    private IEnumerator DispatchAction(Vector2 location, DamageInfo info)
    {
        yield return (delay == 0) ? null : new WaitForSeconds(delay);
        onHealthChanged.Invoke((info.MinHP > 0) ? (info.MinHP / info.MaxHP) : 0);
        OnDamagedEvent.Invoke();
    }

}
