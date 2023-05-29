using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform _actionPoint;
    [SerializeField] private LayerMask _actionLayer;

    private bool _canInteract = true;
    private void Update()
    {
        if (!_canInteract) return;
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            Collider2D collision = Physics2D.OverlapCircle(_actionPoint.position, .2f, _actionLayer);
            
            if (!collision) return;
            
            if (collision.TryGetComponent(out PNJ pnj))
            {
                pnj.StartInteraction();
                return;
            }
        }
    }

    public void SetPlayerInteractable(bool canInteract)
    {
        _canInteract = canInteract;
    }
}
