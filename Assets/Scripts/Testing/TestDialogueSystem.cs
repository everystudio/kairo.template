using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class TestDialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueSystemTrigger dialogueSystemTrigger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.AddDatabase(dialogueSystemTrigger.selectedDatabase);

            Debug.Log("Space");
            dialogueSystemTrigger.OnUse();
        }
    }
}
