using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace anogame
{
    public class NodeMover : MonoBehaviour
    {
        public float speed = 1f;
        [SerializeField] private int currentIndex = 0;
        [SerializeField] private Vector3[] nodes;
        public void MoveNode(UnityEngine.Vector3[] nodes, Action onArrived = null)
        {
            currentIndex = 0;
            this.nodes = nodes;
            // 初期位置強制移動
            transform.position = nodes[currentIndex];
            StartCoroutine(Move(onArrived));
        }

        private IEnumerator Move(Action onArrived = null)
        {
            while (true)
            {
                if (currentIndex >= nodes.Length)
                {
                    break;
                }
                var node = nodes[currentIndex];
                var pos = node;
                var targetPos = new UnityEngine.Vector3(pos.x, pos.y, 0);
                var diff = targetPos - transform.position;
                if (diff.magnitude < 0.1f)
                {
                    currentIndex++;
                    continue;
                }
                transform.position += diff.normalized * speed * Time.deltaTime;
                yield return null;
            }

            onArrived?.Invoke();
        }



    }
}
