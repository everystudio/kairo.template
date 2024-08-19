using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class PlayerController : StateMachineBase<PlayerController>
{
    private PlayerInputActions inputActions;
    [SerializeField] private PlayerBuilding playerBuilding;
    private CameraMover cameraMover;

    private void Start()
    {
        inputActions = new PlayerInputActions();
        //playerBuilding = GetComponent<PlayerBuilding>();
        cameraMover = GetComponent<CameraMover>();

        playerBuilding.Initialize(inputActions);
        cameraMover.Initialize(inputActions);

        inputActions.Enable();
        inputActions.Player.Disable();

        ChangeState(new PlayerController.Idle(this));
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    private class Idle : StateBase<PlayerController>
    {
        public Idle(PlayerController machine) : base(machine)
        {
        }

        override public void OnEnterState()
        {
            Debug.Log("Idle");
            machine.inputActions.Building.Disable();
            machine.inputActions.Idle.Enable();

            UIController.Instance.AddPanel("PanelIdle");
        }
    }
}
