using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeathListener : MonoBehaviour, IKillable
{
    [SerializeField] private Health[] targets;
    [SerializeField] private float delay;
    public UnityEvent OnDeadEvent = new UnityEvent();

    private void Awake()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].AddListener(this);
        }
    }
    public void OnDeath(Health health)
    {
        StartCoroutine(DispatchAction(health));
    }
    private IEnumerator DispatchAction(Health health)
    {
        yield return (delay == 0) ? null : new WaitForSeconds(delay);
        OnDeadEvent.Invoke();
    }
}
