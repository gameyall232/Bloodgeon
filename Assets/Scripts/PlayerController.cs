using UnityEngine;

public class PlayerController : MonoBehaviour
{
	InputMaster input;

	[SerializeField] private float jumpForce = 400f;
	[SerializeField] private float movementSmoothing = .05f;
	[SerializeField] private bool airControl = true;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform ceilingCheck;

	float horizontalVector;
	private bool grounded;
	private Rigidbody2D rb;
	private bool facingRight = true;
	private Vector3 velocity = Vector3.zero;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		input = new InputMaster();
		input.Player.Move.performed += _ctx => horizontalVector = _ctx.ReadValue<float>();
		input.Player.Jump.performed += _ => Jump();
	}

	private void FixedUpdate()
	{
		print(horizontalVector);

		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .2f, groundMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject) { grounded = true; }
		}

		if (grounded || airControl)
		{
			Vector3 targetVelocity = new Vector2(horizontalVector * 10f, rb.velocity.y);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (horizontalVector > 0 && !facingRight)
			{
				Flip();
			}
			else if (horizontalVector < 0 && facingRight)
			{
				Flip();
			}
		}
	}

	private void Jump()
	{
		if (grounded)
		{
			grounded = false;
			rb.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnEnable() { input.Enable(); }
	private void OnDisable() { input.Disable(); }
}