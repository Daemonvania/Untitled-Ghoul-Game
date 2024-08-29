using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : Interactable
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Interact(Player player)
    {

        Debug.Log("Vase Interact");
        _rigidbody.AddForce(transform.forward * 200);
    }
}
