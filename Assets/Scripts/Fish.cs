using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

	// Idea from: http://forum.unity3d.com/threads/random-movement.13069/

	private Vector3 vel;
	private float switchDirection = 3.0f;
	private float currTime = 0.0f;
	private float speed; // Speed at which fish will move
	private bool turnRight; // Determine if fish will turn left or right in update
	public bool firstHit = false; // If fish gets hit twice, destroy (GameController)

	// Idea from: http://aarlangdi.blogspot.com.es/2015/01/smooth-drag-and-drop-in-unity-3d-video.html
	private Vector3 dist;
	private float posX;
	private float posY;

	// Drag or not drag
	private float timeLastTouch; // Will be used to record the time when baby last touched
	private float timeToDrag = 0.1f; // If baby holds for longer seconds, he wants to drag, not destroy
	private bool dragged = false; // Did the baby drag the object?
	
	void Start() {

		// Set random speed of fish
		speed = Random.Range(10, 15);

		// Determine if fish will turn left or right
		if (Random.value > 0.5) {
			turnRight = true;
		} else {
			turnRight = false;
		}

		// Set initial velocity
		SetVel();
	}

	void OnMouseDown() {
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
		// Record the last time the baby touched the screen
		timeLastTouch = Time.time + timeToDrag;
	}

	void OnMouseDrag() {
		if (Time.time > timeLastTouch) {
			dragged = true;
		}
		Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
		transform.position = worldPos;
	}

	// If the baby did not drag the object, then destroy it
	void OnMouseUp() {
		if (!dragged) {
			Destroy(gameObject);
		}
		dragged = false;
	}

	
	void SetVel() {

		if (Random.value > 0.5) {
			vel.x = speed * Time.deltaTime * Random.value;
		} else {
			vel.x = -speed * Time.deltaTime * Random.value;
		}

		if (Random.value > 0.5) {
			vel.y = speed * Time.deltaTime * Random.value;
		} else {
			vel.y = -speed * Time.deltaTime * Random.value;
		}

		if (Random.value > 0.5) {
			vel.z = speed * Time.deltaTime * Random.value;
		} else {
			vel.z = -speed * Time.deltaTime * Random.value;
		}

	}

	void FixedUpdate() {

		if (currTime < switchDirection) {
			currTime += 1 * Time.deltaTime;
		} else {
			SetVel();
			if (Random.value > 0.5) {
				switchDirection += Random.value;
			} else {
				switchDirection -= Random.value;
			}
			if (switchDirection < 1) {
				switchDirection = 1 + Random.value;
			}
			currTime = 0;
		}

		gameObject.GetComponent<Rigidbody>().velocity = vel;

	}

	void Update() {
		if (turnRight) {
			transform.Rotate(Vector3.up * 5 * Time.deltaTime);
		} else {
			transform.Rotate(Vector3.down * 5 * Time.deltaTime);
		}

	}


}
