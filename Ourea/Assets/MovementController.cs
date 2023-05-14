using UnityEngine;

public class MovementController : MonoBehaviour
{
    private CharacterController controller;
    public Transform cam;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float hover = Input.GetAxisRaw("Hover");


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle,0f); 

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        controller.Move(new Vector3(0, hover, 0) * speed * Time.deltaTime); 

    }
}
