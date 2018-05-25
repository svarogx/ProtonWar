using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour {

	void Start(){
	}

	void Update () {
		RaycastHit2D hit;
		Vector2 ray;
		GameObject casual;
		int indx;
		foreach (Touch touch in Input.touches) {
			indx = (int)touch.fingerId;
			ray = Camera.main.ScreenToWorldPoint (touch.position);
			hit = Physics2D.Raycast (ray, Vector2.zero);
			switch (touch.phase) {
			case TouchPhase.Began:
				if (hit.collider) {
					if (hit.collider.tag == "Contender") {
						casual = hit.collider.gameObject;
						casual.GetComponent<ContenderScript>().ContenderClic();
					}
				}
				break;
			case TouchPhase.Canceled:
				break;
			case TouchPhase.Ended:
				break;
			case TouchPhase.Moved:
				break;
			case TouchPhase.Stationary:
				break;
			}
		}

	}
}
