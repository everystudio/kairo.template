using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseAutoSetter : MonoBehaviour
{
    private bool isDatabaseAdded = false;
    [SerializeField] private List<DialogueDatabase> databaseList = new List<DialogueDatabase>();

    private void Update()
    {
        Scene scene = SceneManager.GetSceneByName("Core");

        if (isDatabaseAdded == false && DialogueManager.hasInstance)
        {
            foreach (var database in databaseList)
            {
                DialogueManager.AddDatabase(database);
            }
            isDatabaseAdded = true;
        }
    }

    private void OnDisable()
    {
        if (!isDatabaseAdded)
        {
            foreach (var database in databaseList)
            {
                DialogueManager.RemoveDatabase(database);
            }
        }

    }
}
