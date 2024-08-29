using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDoorLever : Interactable
{
    [SerializeField] private GameObject doorInactive;
    [SerializeField] private GameObject doorActivated;


    private void Start()
    {
        doorActivated.SetActive(false);
    }

    public override void Interact(Player player)
    {
        if (!canInteract) return;
        canInteract = false;
        doorInactive.SetActive(false);
        doorActivated.SetActive(true);
        StartCoroutine(ResetInteract());
    }
    
    private IEnumerator ResetInteract()
    {
        yield return new WaitForSeconds(1);
        doorInactive.SetActive(true);
        doorActivated.SetActive(false);
        canInteract = true;
    }
}
