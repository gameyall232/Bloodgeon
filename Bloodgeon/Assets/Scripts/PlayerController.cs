using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputMaster input;
    public Transform cam;
    public float mouseSensitivity = .2f;
    public float speed = 6f;
    public float gravity = 30f;
    public float jumpPower = 10f;
    public LayerMask groundMask;

    Vector2 inputVector;
    Vector2 mouseVector;
    Vector3 moveVector;
    float xRotation = 0f;
    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        input = new InputMaster();
        input.Player.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        input.Player.Mouse.performed += ctx => mouseVector = ctx.ReadValue<Vector2>();
        input.Player.Jump.performed += _ => Jump();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down, .25f, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;
        moveVector = new Vector3(inputVector.x, inputVector.y, 0);
        transform.position += (moveVector + velocity) * Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded) velocity.y = jumpPower;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
// INPUT.PLAYER.MOVE.ACTIONTYPE = PASS THROUGH
// INPUT.PLAYER.MOUSE.ACTIONTYPE = PASS THROUGH
// INPUT.PLAYER.JUMP.ACTIONTYPE = BUTTON