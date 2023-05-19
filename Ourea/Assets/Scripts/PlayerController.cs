using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    ShipControlller sC;

    [SerializeField]
    float currentHelath, maxHealth;
    [SerializeField]
    float currentOxygen, maxOxygen;

    [SerializeField]
    public bool tool = false;
    [SerializeField]
    public GameObject toolGO;
    ToolController tC;

    private void Awake()
    {
        tC = GetComponent<ToolController>();
        sC = GetComponent<ShipControlller>();
        currentHelath = maxHealth;
        currentOxygen = maxOxygen;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !tool)
        {
            tool = true;
            UIManager.Instance.SelectSlot(0);
            toolGO.SetActive(true);
            tC.enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.E) && tool)
        {
            tool = false;
            UIManager.Instance.UnSelectSlot();
            toolGO.SetActive(false);
            tC.enabled = false;
        }
    }


    public ShipControlller GetShipControlller()
    {
        return sC;
    }


}