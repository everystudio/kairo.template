using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using anogame;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private Test test;

    [SerializeField] private TestingShop testingShop;

    public NodeMover nodeMover;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        var targetPosition = testingShop.GetTargetItemGrid();
        Debug.Log($"targetPosition: {targetPosition}");

        Vector3[] positions = test.GetPathPositions(transform.position, targetPosition);

        nodeMover.MoveNode(positions, () =>
        {
            Debug.Log("Arrived");
        });

    }

    void Update()
    {

    }
}
