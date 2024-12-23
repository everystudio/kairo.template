using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using anogame;
using System.Threading;
using System.Threading.Tasks;
using System;


/*
客の行動パターン
とりあえずお店に来る
目当てのものがあればそれを買う
ない場合は欲しい物があれば8割ぐらいの欲求力があれば買う
ない場合は帰る
*/


public class CustomerController : StateMachineBase<CustomerController>
{
    [SerializeField] private Transform homeTransform;

    [SerializeField] private Test test;

    //[SerializeField] private TestingShop testingShop;

    private Vector3 homePosition;
    private Plowland targetPlowland;
    public NodeMover nodeMover;

    private ItemController buyItemController;
    CancellationTokenSource cts = new CancellationTokenSource();

    public static UnityEvent<ItemController> OnBuyItem = new UnityEvent<ItemController>();


    private void Start()
    {
        buyItemController = null;
        homePosition = transform.position;
        targetPlowland = GameObject.FindObjectOfType<Plowland>();

        ChangeState(new CustomerController.Wait(this));
    }

    private class Wait : StateBase<CustomerController>
    {
        public Wait(CustomerController machine) : base(machine)
        {
        }
        public override void OnEnterState()
        {
            var _ = DelayWait();
        }

        private async Task DelayWait()
        {
            if (machine.buyItemController != null)
            {
                machine.buyItemController.transform.SetParent(null);
                Destroy(machine.buyItemController.gameObject);
                machine.buyItemController = null;
            }

            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });

            ChangeState(new CustomerController.Search(machine));
        }
    }

    private class Water : StateBase<CustomerController>
    {
        private Vector3Int targetGridPosition;

        public Water(CustomerController machine, Vector3Int targetGridPosition) : base(machine)
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
                Debug.Log("水をやった");
            }
            else
            {
                Debug.Log("水をやれなかった");
            }

            ChangeState(new CustomerController.Search(machine));
        }
    }

    private class Move : StateBase<CustomerController>
    {
        private Vector3Int targetGridPosition;
        // 次のステートがある場合はそれを開始する
        private StateBase<CustomerController> nextState;

        public Move(CustomerController machine, Vector3Int targetGridPosition, StateBase<CustomerController> nextState) : base(machine)
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
            /*
            foreach (var pos in list)
            {
                Debug.Log(pos);
            }
            */

            Debug.Log(machine.nodeMover);

            machine.nodeMover.MoveNode(list.ToArray(), (direction) =>
            {
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


    private class Search : StateBase<CustomerController>
    {
        public Search(CustomerController machine) : base(machine)
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
            /*
            if (machine.testingShop.GetTargetItemGrid(out Vector3Int targetGridPosition))
            {
                Debug.Log("見つけた");
                ChangeState(
                    new CustomerController.Move(
                        machine,
                        targetGridPosition,
                        new CustomerController.Buy(machine, targetGridPosition)));
            }
            else
            {
                Debug.Log("見つからなかった");
                Vector3Int homeGrid = machine.testingShop.GetGridPosition(machine.homePosition);
                ChangeState(new CustomerController.Move(machine, homeGrid, new CustomerController.Wait(machine)));
            }
            */

            List<Vector3Int> targetGridPositions = machine.targetPlowland.GetDryTilePositionList();

            if (0 < targetGridPositions.Count)
            {
                int index = UnityEngine.Random.Range(0, targetGridPositions.Count);
                //Debug.Log("見つけた");
                ChangeState(
                    new CustomerController.Move(
                        machine,
                        targetGridPositions[index],
                        new CustomerController.Water(machine, targetGridPositions[index])));
            }
            else
            {
                //Debug.Log("見つからなかった");
                // Waitステートに
                ChangeState(new CustomerController.Wait(machine));

            }
        }
    }

    /*

    private class Buy : StateBase<CustomerController>
    {
        private Vector3Int targetGridPosition;
        public Buy(CustomerController machine, Vector3Int targetGridPosition) : base(machine)
        {
            this.targetGridPosition = targetGridPosition;
        }
        public override void OnEnterState()
        {
            var _ = DelayBuy();
        }

        private async Task DelayBuy()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(500);
            });
            // targetGridPositionのshelfを取得
            var shelf = machine.testingShop.GetShelf(targetGridPosition);

            // shelfにお目当てのアイテムがあるか確認
            if (shelf.HasItem())
            {
                Debug.Log("あった");
                machine.BuyItem(shelf, () =>
                {
                    // お店の外に出る
                    Vector3Int homeGrid = machine.testingShop.GetGridPosition(machine.homePosition);
                    ChangeState(new CustomerController.Move(machine, homeGrid, new CustomerController.Wait(machine)));
                });

                // あれば買う
            }
            else
            {
                Debug.Log("なかった");
                // なければ帰る
                Vector3Int homeGrid = machine.testingShop.GetGridPosition(machine.homePosition);
                ChangeState(new CustomerController.Move(machine, homeGrid, new CustomerController.Wait(machine)));
            }
        }
    }
    */

    private void BuyItem(ShelfController shelf, Action onComplete)
    {
        buyItemController = shelf.GetItemController();
        shelf.RemoveItem();

        OnBuyItem?.Invoke(buyItemController);

        buyItemController.transform.SetParent(transform);
        buyItemController.transform.DOLocalMove(new Vector3(0, 1, 4), 1f).OnComplete(() =>
        {
            //buyItemController.transform.SetParent(null);
            //buyItemController = null;
            onComplete.Invoke();
        });
    }

    private void OnDestroy()
    {
        cts.Cancel();
    }

}
