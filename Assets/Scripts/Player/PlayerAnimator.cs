using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator am;
    PlayerMovement pm;

    void Awake()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandleSpriteFlip();

        if (pm.moveDir.x !=0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
        }
        else
        {
            am.SetBool("Move", false);
        }
    }

    private void HandleSpriteFlip()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
