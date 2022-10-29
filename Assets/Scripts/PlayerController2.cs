using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

        private float movementX;
        private float movementY;
		private float movementJump;

	private Rigidbody rb;
	private int count;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;
		movementJump = 0;

		SetCountText ();

                // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
                winTextObject.SetActive(false);
	}

	
	void FixedUpdate ()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (movementX, movementJump, movementY);

		rb.AddForce (movement * speed);
		movementJump = 0;
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}

	}

	private void changeScene()
	{
		SceneManager.LoadScene("MiniGame2");
	}

        void OnMove(InputValue value)
        {
        	Vector2 v = value.Get<Vector2>();

        	movementX = v.x;
        	movementY = v.y;
        }

		private void OnJump()
	{
		if(transform.position.y <= .5)
		{
			movementJump = 35f;
		}
	}

        void SetCountText()
	{
		countText.text = "Contador: " + count.ToString();

		if (count >= 12) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
							changeScene();

		}
	}
}
