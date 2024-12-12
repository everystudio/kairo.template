using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;
using System;
using UnityEngine.InputSystem;

public class PlayerController : StateMachineBase<PlayerController>
{
    private PlayerInputActions inputActions;
    [SerializeField] private PlayerBuilding playerBuilding;
    private CameraMover cameraMover;
    private Plowland plowland;

    public PanelIdle panelIdle;

    public StateBase<PlayerController> StateCurrent => stateCurrent;

    private void Start()
    {
        Debug.Log("PlayerController Start");
        inputActions = new PlayerInputActions();
        //playerBuilding = GetComponent<PlayerBuilding>();
        cameraMover = GetComponent<CameraMover>();

        playerBuilding.Initialize(inputActions);
        cameraMover.Initialize(inputActions);

        inputActions.Enable();
        inputActions.Player.Disable();
        inputActions.CameraMove.Enable();

        plowland = GameObject.FindObjectOfType<Plowland>();


        ChangeState(new PlayerController.Idle(this));
    }

    private void OnDestroy()
    {
        Debug.Log("PlayerController OnDestroy");
        inputActions.Disable();

        ChangeState(new PlayerController.CloseGame(this));
    }

    private class Idle : StateBase<PlayerController>
    {
        private bool isMenuIconClicked = false;
        public Idle(PlayerController machine) : base(machine)
        {
        }

        override public async void OnEnterState()
        {
            Debug.Log("Idle");
            machine.inputActions.Building.Disable();
            machine.inputActions.Idle.Enable();

            machine.inputActions.Idle.Cancel.performed -= OnCancel;
            MenuIconButton.OnClick.RemoveListener(OnMenuIconClick);

            machine.inputActions.Idle.Cancel.performed += OnCancel;
            MenuIconButton.OnClick.AddListener(OnMenuIconClick);

            if (machine.panelIdle == null)
            {
                machine.panelIdle = UIController.Instance.AddPanel("PanelIdle").GetComponent<PanelIdle>();
            }
            await machine.panelIdle.Show(3f);
        }

        public override void OnUpdateState()
        {
            Vector2 cursorPosition = machine.inputActions.Idle.CursorPosition.ReadValue<Vector2>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            mousePosition.z = 0f;

            if (machine.inputActions.Idle.Interaction.triggered)
            {
                var gridPosition = machine.plowland.TargetTilemap.WorldToCell(mousePosition);

                Debug.Log($"gridPosition: {gridPosition}");

                var building = machine.plowland.GetUserBuildingModel(gridPosition);

                if (building != null)
                {
                    Debug.Log($"building: {building} id: {building.id}");
                    ChangeState(new PlayerController.BuildingMenu(machine, building));
                }
            }
        }

        private void OnMenuIconClick(MenuIconModel arg0)
        {
            Debug.Log($"OnMenuIconClick:{isMenuIconClicked}");
            machine.inputActions.Idle.Cancel.performed -= OnCancel;
            MenuIconButton.OnClick.RemoveListener(OnMenuIconClick);
            if (isMenuIconClicked)
            {
                return;
            }
            isMenuIconClicked = true;
            machine.ChangeState(new PlayerController.Building(machine, arg0));
        }

        private void OnCancel(InputAction.CallbackContext context)
        {

            machine.panelIdle.Open(false);

        }
        override public void OnExitState()
        {
            Debug.Log("Idle Exit");
            machine.inputActions.Idle.Cancel.performed -= OnCancel;
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
            if (machine.panelIdle != null)
            {
                Debug.Log("panelIdle is not null");
            }
            machine.panelIdle.Hide();
        }

        private async void OnEndBuilding(bool arg0, Vector3Int arg1)
        {
            await machine.panelIdle.Show();
            machine.ChangeState(new PlayerController.Idle(machine));
        }

        override public void OnExitState()
        {
            machine.playerBuilding.OnEndBuilding.RemoveListener(OnEndBuilding);
        }
    }

    private class BuildingMenu : StateBase<PlayerController>
    {
        private UserBuildingModel building;

        public BuildingMenu(PlayerController machine, UserBuildingModel building) : base(machine)
        {
            this.machine = machine;
            this.building = building;
        }

        override public async void OnEnterState()
        {
            Debug.Log("BuildingMenu");
            Debug.Log($"building: {building} id: {building.id}");
            await machine.panelIdle.Hide();
        }
    }

    private class CloseGame : StateBase<PlayerController>
    {

        public CloseGame(PlayerController machine) : base(machine)
        {
        }
    }

}
