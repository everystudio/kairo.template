using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogame;

public class InteractableListener : MonoBehaviour, IInteractable
{
    public UnityEventGameObject OnInteract;
    public void Interact(GameObject owner)
    {
        OnInteract.Invoke(owner);
    }

}
