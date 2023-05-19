using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    PlayerController pC;
    ShipControlller sC;
    

    // Start is called before the first frame update
    void Start()
    {
        pC = GetComponent<PlayerController>();
        sC = GetComponent<ShipControlller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && sC.GetCharacterState() == CharacterState.Aim)
        {
            ShootRaycast();
        }
        else
        {

        }
    }

    public void ShootRaycast()
    {
        RaycastHit hit;

        if(Physics.Raycast(pC.toolGO.transform.position, sC.cam.forward, out hit, 5))
        {
            Debug.Log(hit.collider.gameObject.name);
            // Need to create an ore script with for the damages then check if component and there we go
        }
    }
}
