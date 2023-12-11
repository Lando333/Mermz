using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool autoAttack = true;
    public bool autoAim = false;

    [SerializeField] TextMeshProUGUI aimDisplay;
    [SerializeField] TextMeshProUGUI attackDisplay;

    private void Start()
    {
        AimOff();
        AttackOn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleAutoAim();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleAutoAttack();
        }
    }

    private void ToggleAutoAim()
    {
        if (autoAim == false)
        {
            AimOn();
        }
        else if (autoAim == true)
        {
            AimOff();
        }
    }

    private void ToggleAutoAttack()
    {
        if (autoAttack == false)
        {
            AttackOn();
        }
        else if (autoAttack == true)
        {
            AttackOff();
        }
    }

    private void AimOn()
    {
        autoAim = true;
        aimDisplay.text = "On";
    }

    private void AimOff()
    {
        autoAim = false;
        aimDisplay.text = "Off";
    }

    private void AttackOn()
    {
        autoAttack = true;
        attackDisplay.text = "On";
    }
    private void AttackOff()
    {
        autoAttack = false;
        attackDisplay.text = "Off";
    }
}
