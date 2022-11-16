using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text winText;
	public float rayLength;
	public LayerMask groundMask;
	public float force;

	Vector3 movement;
	Ray ray;
	RaycastHit hit;
	public bool canJump;
	public bool isGrounded;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();		
	}
    private void Update()
    {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		if (Input.GetMouseButtonDown(0) && isGrounded) canJump = true;
    }
    void FixedUpdate ()
	{
		rb.AddForce (movement * speed);

		RaycastGround();

		if (canJump) Jump();
	}

	void RaycastGround()
    {
		ray.origin = transform.position;
		//Vector3.up se refiere al eje global, y transform.up al eje local
		ray.direction = -Vector3.up;

		if (Physics.Raycast(ray, out hit, rayLength, groundMask))
		{
			Debug.Log("Estoy chocando con algo de la capa groundMask");
			isGrounded = true;
		}
		else isGrounded = false;

		Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }
	void Jump()
	{
		canJump = false;
		rb.AddForce(Vector3.up * force);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		winText.text = "";
		countText.text = "Count: " + count.ToString ();

		if (count >= 12) 
		{
			winText.text = "You Win!";
		}
	}
}