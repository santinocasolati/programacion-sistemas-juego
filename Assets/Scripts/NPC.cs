using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public InteractionPriority InteractionPriority => InteractionPriority.High;

    public void Interact()
    {

    }
}
