using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController  controller;
    private bool test;
    void Start()
    {
        animator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            test = true;
            animator.runtimeAnimatorController = controller;
            animator.gameObject.SetActive(true);
        }

        if (!test) return;
        
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            animator.gameObject.SetActive(false);
        };
    }
}
