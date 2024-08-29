using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    //todo make kinds of interactables? Some you interact with and can interact again after some set time, others you cant or its different?
    public static event EventHandler OnInteracted;

    public bool canInteract = true;
    
    public static void ResetStaticData() {
        OnInteracted = null;
    }
    
    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
        OnInteracted?.Invoke(this, EventArgs.Empty);
    }
       
    //todo Pickup?
    public virtual void InteractAlternate(Player player) {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }
}
