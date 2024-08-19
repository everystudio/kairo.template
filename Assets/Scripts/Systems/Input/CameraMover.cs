using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// InputSystemを使う
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    // カメラの移動速度
    public float moveSpeed = 10.0f;
    // PlayerInputを取得するための変数
    private PlayerInputActions playerInput;

    private InputAction cameraMoveTrigger;

    private bool isMoving = false;

    // Start is called before the first frame update
    public void Initialize(PlayerInputActions playerInput)
    {
        this.playerInput = playerInput;
        // PlayerInputActionsのMoveのアクションにMoveCameraを登録
        playerInput.Idle.CameraMove.performed += MoveCamera;

        cameraMoveTrigger = playerInput.Idle.MoveTrigger;

        cameraMoveTrigger.started += MoveTriggerStarted;
        cameraMoveTrigger.canceled += MoveTriggerCanceled;
    }
    private void OnDestroy()
    {
        cameraMoveTrigger.started -= MoveTriggerStarted;
        cameraMoveTrigger.canceled -= MoveTriggerCanceled;
    }

    private void MoveTriggerStarted(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            isMoving = true;
        }
    }
    private void MoveTriggerCanceled(InputAction.CallbackContext context)
    {
        isMoving = false;
    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        if (!isMoving) return;
        // カメラの移動
        Vector2 move = context.ReadValue<Vector2>();

        move *= -1f;
        transform.position += new Vector3(move.x, move.y, 0) * moveSpeed * Time.deltaTime;
    }


}
