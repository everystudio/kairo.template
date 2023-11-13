using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDotDamage : MonoBehaviour
{
    [SerializeField] private Health targetHealth;

    [SerializeField] private float interval = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (interval <= timer)
        {
            timer = 0f;
            targetHealth.Damage(1, Vector3.zero, gameObject);
        }
    }

}
