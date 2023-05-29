using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Transform _actionPoint;
    [SerializeField] private LayerMask _unwalkableLayer;
    [SerializeField] private Animator _heroAnimator;
    
    private bool _isMovable = true;
    private float moveSpeed;
    private float animationSpeed = 1;
    private float lastX = 0;
    private float lastY = 0;
    private bool endMovement;
    private void Start()
    {
        _movePoint.parent = null;
        
        _heroAnimator.runtimeAnimatorController = AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam[0].GetAnimatorController();
        moveSpeed = _moveSpeed;
    }

    private void Update()
    {
        if (!_isMovable) return;
        
        transform.position = Vector3.MoveTowards(transform.position,_movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _movePoint.position) <= 0.05f)
        {
            if (endMovement)
            {
                endMovement = false;
                AppManager.Instance.PlayerManager.PlayerSo.SetLastPosition(transform.position);
                if (AppManager.Instance.MapManager.OnEndMovementTick.Invoke())
                    return;
            }
            
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f),
                        .2f, _unwalkableLayer))
                {
                    _movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    _actionPoint.position = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f) + transform.position;
                }

                lastX = Input.GetAxisRaw("Horizontal");
                lastY = 0;
                endMovement = true;
            }
            else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f),
                        .2f, _unwalkableLayer))
                {
                    _movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    _actionPoint.position = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f) + transform.position;
                }

                lastX = 0;
                lastY = Input.GetAxisRaw("Vertical");
                endMovement = true;
            }
            animationSpeed = 1;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = _moveSpeed * 2;
                animationSpeed = moveSpeed;
            }
            else
            {
                moveSpeed = _moveSpeed;
                animationSpeed = moveSpeed;
            }
        }

        SetAnimatorVariable();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        _movePoint.position = position;
    }
    
    private void SetAnimatorVariable()
    {
        _heroAnimator.SetFloat("X", lastX);
        _heroAnimator.SetFloat("Y", lastY);
        _heroAnimator.speed = animationSpeed;
    }
    
    public void SetPlayerMovable(bool canMove)
    {
        _isMovable = canMove;
    }
}
