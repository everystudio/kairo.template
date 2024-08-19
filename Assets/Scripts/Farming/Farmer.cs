using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System.Threading.Tasks;
using System.Threading;

public class Farmer : StateMachineBase<Farmer>
{
    private Plowland targetPlowland;
    private NodeMover nodeMover;
    private Animator animator;

    CancellationTokenSource cts = new CancellationTokenSource();

    private void Start()
    {
        targetPlowland = GameObject.FindObjectOfType<Plowland>();
        animator = GetComponent<Animator>();
        nodeMover = GetComponent<NodeMover>();

        ChangeState(new Farmer.Wait(this));
    }

    private class Wait : StateBase<Farmer>
    {
        public Wait(Farmer machine) : base(machine)
        {
        }
        public override void OnEnterState()
        {
            var _ = DelayWait();
        }
        private async Task DelayWait()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });

            ChangeState(new Farmer.Search(machine));
        }
    }

    private class Search : StateBase<Farmer>
    {
        public Search(Farmer machine) : base(machine)
        {
        }
        public override void OnEnterState()
        {
            var _ = DelaySearch(machine.cts.Token);
        }
        private async Task DelaySearch(CancellationToken cancellationToken)
        {
            // キャンセレーショントークンが止まっている場合は止める
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            List<Vector3Int> targetGridPositions = machine.targetPlowland.GetDryTilePositionList();

            if (0 < targetGridPositions.Count)
            {
                int index = UnityEngine.Random.Range(0, targetGridPositions.Count);
                //Debug.Log("見つけた");
                ChangeState(
                    new Farmer.Move(
                        machine,
                        targetGridPositions[index],
                        new Farmer.Water(machine, targetGridPositions[index])));
            }
            else
            {
                //Debug.Log("見つからなかった");
                // Waitステートに
                ChangeState(new Farmer.Wait(machine));

            }
        }
    }

    private class Move : StateBase<Farmer>
    {
        private Vector3Int targetGridPosition;
        // 次のステートがある場合はそれを開始する
        private StateBase<Farmer> nextState;

        public Move(Farmer machine, Vector3Int targetGridPosition, StateBase<Farmer> nextState) : base(machine)
        {
            this.targetGridPosition = targetGridPosition;
            this.nextState = nextState;
        }
        public override void OnEnterState()
        {
            Debug.Log($"targetPosition: {targetGridPosition}");

            //Vector3[] positions = machine.test.GetPathPositions(machine.transform.position, targetGridPosition);
            machine.targetPlowland.RefreshWalkableNodeList();
            Vector3[] positions = machine.targetPlowland.GetPathPositions(machine.transform.position, targetGridPosition);

            // positionsの先頭に現在の位置を追加する
            var list = new List<Vector3>(positions);
            list.Insert(0, machine.transform.position);

            Debug.Log(machine.nodeMover);

            machine.nodeMover.MoveNode(list.ToArray(), (direction) =>
            {
                Vector2 isometricDirectionX = new Vector2(1f, 0.5f).normalized;
                Vector2 isometricDirectionY = new Vector2(-1f, 0.5f).normalized;
                Vector3 movement = new Vector3(
                    direction.x * isometricDirectionX.x + direction.y * isometricDirectionY.x,
                    direction.x * isometricDirectionX.y + direction.y * isometricDirectionY.y,
                    0f).normalized;

                // 
                movement = direction;
                //Debug.Log(movement);

                machine.animator.SetFloat("x", direction.x);
                machine.animator.SetFloat("y", direction.y);
            }, () =>
            {
                Debug.Log("Arrived");
                if (nextState != null)
                {
                    ChangeState(nextState);
                }
            });
        }
    }

    private class Water : StateBase<Farmer>
    {
        private Vector3Int targetGridPosition;
        public Water(Farmer machine, Vector3Int targetGridPosition) : base(machine)
        {
            this.machine = machine;
            this.targetGridPosition = targetGridPosition;
        }
        public override void OnEnterState()
        {
            var _ = DelayWater();
        }

        private async Task DelayWater()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(500);
            });
            Debug.Log(targetGridPosition);

            if (machine.targetPlowland.IsPlowed(targetGridPosition) == false)
            {
                // 耕す
                if (machine.targetPlowland.Plow(targetGridPosition))
                {
                    Debug.Log("耕した");
                }
                else
                {
                    Debug.Log("耕せなかった");
                }
            }

            if (machine.targetPlowland.Water(targetGridPosition))
            {
                Vector3 direction = (machine.transform.position - targetGridPosition).normalized;

                machine.animator.SetFloat("x", direction.x);
                machine.animator.SetFloat("y", direction.y);

                Debug.Log("水をやった");
            }
            else
            {
                Debug.Log("水をやれなかった");
            }

            ChangeState(new Farmer.Search(machine));
        }
    }


}




