using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System;

public class PlayerController : StateMachineBase<PlayerController>
{
    private PlayerInputActions inputActions;
    [SerializeField] private PlayerBuilding playerBuilding;
    private CameraMover cameraMover;

    public UIPanel currentPanel;

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

        override public async void OnEnterState()
        {
            Debug.Log("Idle");
            machine.inputActions.Building.Disable();
            machine.inputActions.Idle.Enable();

            MenuIconButton.OnClick.AddListener(OnMenuIconClick);

            if (machine.currentPanel == null)
            {
                machine.currentPanel = UIController.Instance.AddPanel("PanelIdle").GetComponent<UIPanel>();
            }
            await machine.currentPanel.Show(3f);

        }

        private void OnMenuIconClick(MenuIconModel arg0)
        {
            machine.ChangeState(new PlayerController.Building(machine, arg0));
        }

        override public void OnExitState()
        {
            MenuIconButton.OnClick.RemoveListener(OnMenuIconClick);
        }
    }

    private class Building : StateBase<PlayerController>
    {
        private MenuIconModel menuIcon;

        public Building(PlayerController machine, MenuIconModel arg0) : base(machine)
        {
            this.machine = machine;
            this.menuIcon = arg0;
        }

        override public async void OnEnterState()
        {
            Debug.Log("Building");
            machine.inputActions.Idle.Disable();
            machine.inputActions.Building.Enable();

            //UIController.Instance.AddPanel("PanelBuilding");
            machine.playerBuilding.Build(menuIcon);
            machine.playerBuilding.OnEndBuilding.AddListener(OnEndBuilding);
            await machine.currentPanel.Hide();
        }

        private async void OnEndBuilding(bool arg0, Vector3Int arg1)
        {
            await machine.currentPanel.Show();
            machine.ChangeState(new PlayerController.Idle(machine));
        }

        override public void OnExitState()
        {
            machine.playerBuilding.OnEndBuilding.RemoveListener(OnEndBuilding);
        }
    }
}
