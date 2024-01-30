using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(Button))]
public class ContinueConversation : MonoBehaviour
{
    private DialogueSystemActions dialogueSystemActions;

    private void Start()
    {
        dialogueSystemActions = new DialogueSystemActions();
        dialogueSystemActions.Enable();

        dialogueSystemActions.Conversation.Continue.performed += _ =>
        {
            Button button = GetComponent<Button>();
            if (button != null && button.interactable)
            {
                button.onClick.Invoke();
            }
        };

    }
    /*
    void Update()
    {
        // 左クリックまたはキー入力があれば
        if (Input.GetButtonDown("Fire1"))
        {
            // Dialogue Systemの次の行動を呼び出す
            if (DialogueManager.isConversationActive)
            {
                DialogueManager.instance.SendMessage("OnContinue");
            }
        }
    }
    */
}
