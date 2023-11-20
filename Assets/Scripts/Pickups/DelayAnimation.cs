using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    [SerializeField]
    float delayAmount;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PlayAnim(delayAmount));
    }

    IEnumerator PlayAnim(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        anim.enabled = true;
        anim.Play("GemShine");

        StartCoroutine(StopAnim());

    }

    IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(1f);

        anim.enabled = false;

        StartCoroutine(PlayAnim(delayAmount));
    }
}
