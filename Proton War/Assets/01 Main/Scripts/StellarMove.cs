using UnityEngine;
using System.Collections;

public class StellarMove : MonoBehaviour {

	public Sprite[] stellar;

	public float speed = 5.0f;
	public float lifeTime = 4.0f;
	private SpriteRenderer stellarRender;


	void Awake(){
		stellarRender = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		int indx = Random.Range (0, stellar.Length);
		stellarRender.sprite = stellar [indx];
		Invoke ("TimeToLife", lifeTime);
	}

	// Update is called once per frame
	void FixedUpdate(){
		float realSpeed = speed * Time.fixedDeltaTime;
		transform.position = new Vector3 (transform.position.x - realSpeed, transform.position.y - realSpeed, transform.position.z); ;
	}

	private void TimeToLife(){
		Destroy (gameObject);
	}
}
