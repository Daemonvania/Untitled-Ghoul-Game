using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Climb : Interactable
{
    [SerializeField] private Transform[] positions;

    public override void Interact(Player player)
    {
        Debug.Log(player.isHigh);
        player.SetMovement(false);
        if (player.isHigh)
        {
            player.transform.DOMove(positions[1].position, 0.1f).OnComplete(() =>
            {
                player.transform.DOMove(positions[0].position, 0.3f).SetEase(Ease.Linear).OnComplete((() =>
                {
                    player.isHigh = false;
                    player.SetMovement(true);
                }));
            });
        }
        else
        {
            player.transform.DOMove(positions[1].position, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
            {
                player.transform.DOMove(positions[2].position, 0.1f).OnComplete((() =>
                {
                    player.isHigh = true;
                    player.SetMovement(true);
                }));
            });
        }
   
    }
}
