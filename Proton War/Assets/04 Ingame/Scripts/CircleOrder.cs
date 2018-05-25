using UnityEngine;
using System.Collections;

public class CircleOrder : MonoBehaviour {

	public GameObject elementPrefab;
	public int maxLine;

	private float bigRadius;
	private float lowRadius; 
	public float factorRadius = 0.95f;

	private SpriteRenderer circleRender;
	public Sprite coreSprite;

	void Awake(){
		circleRender = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		circleRender.sprite = coreSprite;

		bigRadius = GetComponent<SpriteRenderer> ().bounds.extents.x;
		lowRadius = elementPrefab.GetComponent<SpriteRenderer> ().bounds.extents.x * factorRadius;
		Debug.Log ("Big: " + bigRadius);
		Debug.Log ("Low: " + lowRadius);
		for (int i = 1; i <= maxLine; i++) {
			GenerateLine (i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateLine(int slotLine){
		float newRadius = bigRadius + (2 * slotLine - 1) * lowRadius;
		float lineangle = Mathf.Acos ((Mathf.Pow(newRadius,2) - 2 * Mathf.Pow(lowRadius,2)) / Mathf.Pow (newRadius, 2));
		Vector3 newPos;
		GameObject tmpCircle;
		float offst = Random.Range (0.0f, Mathf.PI / 4);
		float angle = 0;
		int i = 1;
		while (angle < Mathf.PI * 2) {
			newPos = new Vector3(newRadius * Mathf.Cos(angle + offst), newRadius * Mathf.Sin(angle + offst),0);
			tmpCircle = Instantiate (elementPrefab, newPos, Quaternion.identity) as GameObject;
			tmpCircle.transform.parent = this.transform.GetChild (slotLine - 1).transform;
			tmpCircle.transform.name = slotLine.ToString() + i.ToString();
			tmpCircle.GetComponent<OddScript> ().slotLine = slotLine;
			tmpCircle.GetComponent<OddScript> ().slotElement = i;
			if (i == 1)
				tmpCircle.GetComponent<OddScript> ().isFirst = true;
			i += 1;
			angle += lineangle;
			if (angle + lineangle >= Mathf.PI * 2)
				tmpCircle.GetComponent<OddScript> ().isLast = true;
			if (angle >= Mathf.PI * 2)
				Destroy (tmpCircle);
		}

	}
}
