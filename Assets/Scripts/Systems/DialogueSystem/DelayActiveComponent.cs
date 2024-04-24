using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
// asyncを使って実装する

public class DelayActiveComponent : MonoBehaviour
{
    [SerializeField] private float delayTime = 1.0f;
    [SerializeField] private List<MonoBehaviour> components = new List<MonoBehaviour>();

    private async void Start()
    {
        await Task.Delay((int)(delayTime * 1000));

        foreach (var component in components)
        {
            component.enabled = true;
        }
    }

}
