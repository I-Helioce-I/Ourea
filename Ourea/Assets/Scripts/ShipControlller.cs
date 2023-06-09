using Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Input = UnityEngine.Input;
public enum CharacterState
{
    Stationnary_Idle,
    Stationnary_Moving,
    Swim_Idle,
    Cruise_Idle,
    Aim
}
public class ShipControlller : MonoBehaviour
{
    public PlayerController pc;
    Vector2 lookInput, screenCenter, mouseDistance;

    [SerializeField]
    Animator pcAnimator;
    [SerializeField]
    GameObject[] cams;
    [SerializeField]
    CinemachineBrain cB;

    public string ressourcesType;

    [SerializeField]
    CharacterState currentState = CharacterState.Stationnary_Idle;

    private CharacterController controller;
    public Transform cam;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;




    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        controller = GetComponent<CharacterController>();
        pc = GetComponent<PlayerController>();
        
    }

    private void Update()
    {
        MovementHandler();

        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeState(CharacterState.Cruise_Idle);
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            ChangeState(CharacterState.Stationnary_Idle);
        }

        if (Input.GetKey(KeyCode.C))
        {
            ChangeState(CharacterState.Swim_Idle);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            ChangeState(CharacterState.Stationnary_Idle);
        }

        if (Input.GetKey(KeyCode.Mouse1) && pc.tool)
        {
            ChangeState(CharacterState.Aim);
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ChangeState(CharacterState.Stationnary_Idle);
        }
    }

    private void MovementHandler()
    {
        switch (currentState)
        {
            case CharacterState.Stationnary_Idle:
                {
                    StationnaryMovement();
                }
                break;
            case CharacterState.Stationnary_Moving:
                {

                }
                break;
            case CharacterState.Swim_Idle:
                {
                    SwimForward();
                }
                break;
            case CharacterState.Cruise_Idle:
                {
                    SwimForward();
                }
                break;
            case CharacterState.Aim:
                {
                    SwimForward();

                }
                break;
            default: break;
        }
    }

    public void ChangeState(CharacterState newState)
    {
        //Exit transition
        switch (currentState)
        {
            case CharacterState.Stationnary_Idle:
                {

                }
                break;
            case CharacterState.Stationnary_Moving:
                {

                }
                break;
            case CharacterState.Swim_Idle:
                {
                    pcAnimator.SetBool("Swim", false);
                    speed = 2.5f;
                }
                break;
            case CharacterState.Cruise_Idle:
                {
                    pcAnimator.SetBool("Cruise", false);

                }
                break;
            case CharacterState.Aim:
                {
                    UIManager.Instance.SetCursorActive(false);
                    pcAnimator.SetBool("Aim", false);
                }
                break;
            default: break;
        }

        currentState = newState;

        //Enter transition
        switch (currentState)
        {
            case CharacterState.Stationnary_Idle:
                {
                    OrientToWorldUp();
                    speed = 2.5f;
                    pcAnimator.speed = speed / 4;
                    CameraSwitcher(cams[0]);
                }
                break;
            case CharacterState.Stationnary_Moving:
                {


                }
                break;
            case CharacterState.Swim_Idle:
                {
                    pcAnimator.SetBool("Swim", true);
                    speed = 7.5f;
                    CameraSwitcher(cams[0]);
                }
                break;
            case CharacterState.Cruise_Idle:
                {
                    pcAnimator.SetBool("Cruise", true);
                    speed = 15;
                    CameraSwitcher(cams[0]);
                }
                break;
            case CharacterState.Aim:
                {
                    UIManager.Instance.SetCursorActive(true);
                    pcAnimator.SetBool("Aim", true);
                    CameraSwitcher(cams[1]);
                }
                break;
            default: break;
        }
    }

    public CharacterState GetCharacterState()
    {
        return currentState;
    }

    void CameraSwitcher(GameObject camToActivate)
    {
        foreach (GameObject cam in cams)
        {
            if (cam != camToActivate)
            {
                cam.SetActive(false);
            }
            else
            {
                cam.SetActive(true);
            }
        }
    }

    void OrientToWorldUp()
    {
        float rotationSpeed = 2;

        Quaternion rotation = Quaternion.FromToRotation(transform.up, new Vector3(transform.localRotation.x, 1, transform.localRotation.z));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * rotationSpeed);
    }

    public void StationnaryMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float hover = Input.GetAxisRaw("Hover");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }

        controller.Move(new Vector3(0, hover, 0) * speed * Time.deltaTime);
    }

    public void SwimForward()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(cB.transform.forward * speed * Time.deltaTime);
        }

        //transform.rotation = Quaternion.Euler(cB.transform.localEulerAngles);
    }
}
