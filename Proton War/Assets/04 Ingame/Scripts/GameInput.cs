using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameInput : MonoBehaviour {

	public GameObject rotateElement;
	public float sensibility = 10.0f;
	public float maxAngle = 5.0f;

	public KeyCode rotateLeft;
	public KeyCode rotateRight;
	public float rotateSpeed = 2.0f;

//	public Text debugText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			TouchHandle ();
			if (Input.GetKey (KeyCode.Escape))
				MainMenu ();
		} else {
			ButtonHandle ();
			if (Input.GetKey (KeyCode.Escape))
				MainMenu ();
		}
	}

	private void TouchHandle(){
		if (Input.touchCount != 2)
			return;
		Touch pos1 = new Touch ();
		Touch pos2 = new Touch ();
		int ratio = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Moved)
				return;
			if (ratio == 0) {
				pos1 = touch;
				ratio += 1;
			}
			else
				pos2 = touch;
		}
		Vector2 prevPos1 = pos1.position - pos1.deltaPosition;
		Vector2 prevPos2 = pos2.position - pos2.deltaPosition;
		Vector2 prevDir = prevPos2 - prevPos1;
		Vector2 currDir = pos2.position - pos1.position;

		float oldAngle = Mathf.Atan2 (prevDir.y, prevDir.x);
		float newAngle = Mathf.Atan2 (currDir.y, currDir.x);
		float deltaAngle = Mathf.DeltaAngle(newAngle, oldAngle);

		RotateObject (Mathf.Clamp(deltaAngle * -sensibility, -maxAngle, maxAngle));
	}

	private void RotateObject(float angle){
		rotateElement.transform.Rotate (0, 0, angle);
	}

	private void ButtonHandle(){
		if (Input.GetKey (rotateLeft)) 
			RotateObject (rotateSpeed * Time.deltaTime);
		if (Input.GetKey (rotateRight)) 
			RotateObject (-rotateSpeed * Time.deltaTime);
	}

	public void ShutGame(){
		Application.Quit();
	}

	public void MainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
