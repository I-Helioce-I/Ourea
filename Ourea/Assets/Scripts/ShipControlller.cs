using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ShipControlller : MonoBehaviour
{
    [SerializeField]
    float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;

    float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    [SerializeField]
    float lookRateSpeed = 90f;

    Vector2 lookInput, screenCenter, mouseDistance;

    float rollInput;

    [SerializeField]
    float rollSpeed = 90f, rollAcceleration = 3.5f;

    [SerializeField]
    Animator pcAnimator;
    [SerializeField]
    GameObject renderer;
    Quaternion startPosition, endPosition ;

    private void Start()
    {
        //Cursor.visible = false;
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        startPosition = transform.rotation;
    }

    private void Update()
    {
        Movement();

    }

    private void Movement()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        float deadzone = 200f;
        float maxDistance = (Screen.width / 2f - deadzone) / (Screen.width / 2f);
        Vector2 mousePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        Vector2 center = new Vector2(0.5f, 0.5f);
        Vector2 offset = (mousePos - center) / maxDistance;
        mouseDistance = Vector2.ClampMagnitude(offset, 1f);


        //mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        pcAnimator.SetFloat("Speed", activeForwardSpeed * forwardSpeed * 4 / 100);


        transform.position += Vector3.Lerp(transform.position, transform.forward * activeForwardSpeed * Time.deltaTime, 5f);
        transform.position += Vector3.Lerp(transform.position, (transform.right * activeStrafeSpeed * Time.deltaTime), 5f) + Vector3.Lerp(transform.position, (transform.up * activeHoverSpeed * Time.deltaTime), 5f);

    }

}
