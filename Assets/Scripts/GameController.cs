using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] fishPrefabs;
	public AudioClip moveFish;
	public AudioClip createFish;

	private Vector3 camPos;
	private GameObject target;
	private float smoothCam = 0.5f;
	
	private Color color1 = new Color(0.3f, 0.6f, 1.0f, 1.0f);  // Light blue
	private Color color2 = new Color(1.0f, 1.0f, 0.3f, 1.0f);  // Light yellow
	private float duration = 10.0f;

	private Camera mainCam; 

	void Start() {
		// Get the initial position of the camera
		mainCam = Camera.main;
		camPos = mainCam.transform.position;
	}

	void Update () {

		// Find out if the baby touched the screen
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				// Check to see if we hit a fish
				if (hit.collider.CompareTag("Fish")) {
					// Set the target that the camera will follow
					target = hit.collider.gameObject;
					// Play sound
					AudioSource.PlayClipAtPoint(moveFish, hit.collider.gameObject.transform.position);
				}
			} else {
				Vector3 pos = Input.mousePosition;
				pos.z = Random.Range(5.0f, 10.0f);
				Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
				int index = Random.Range(0, fishPrefabs.Length);
				GameObject fish = Instantiate(fishPrefabs[index], worldPos, Quaternion.identity) as GameObject;
				AudioSource.PlayClipAtPoint(createFish, worldPos);
				// Set the target that the camera will follow
				target = fish;
			}
		}

		// Make the camera focus on the target
		if (target != null) {
			camPos.x = Mathf.Lerp(camPos.x, target.transform.position.x, Time.deltaTime * smoothCam);
			camPos.y = Mathf.Lerp(camPos.y, target.transform.position.y, Time.deltaTime * smoothCam);
			Camera.main.transform.position = camPos;
		}

		// Change background color
		float t = Mathf.PingPong(Time.time, duration) / duration;
		mainCam.backgroundColor = Color.Lerp(color1, color2, t);

	}

}
