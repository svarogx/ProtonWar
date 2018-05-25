using UnityEngine;
using System.Collections;

public class SphereMove : MonoBehaviour {

	public Sprite[] sphere;

	public GameObject steam;
	public Sprite[] steamSprite;
	public Vector3[] steamOffset;

	public float speed = 5.0f;
	public float lifeTime = 4.0f;
	private SpriteRenderer sphereRender;
	private SpriteRenderer steamRender;


	void Awake(){
		sphereRender = GetComponent<SpriteRenderer> ();
		steamRender = steam.GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		int indx = Random.Range (0, sphere.Length);
		sphereRender.sprite = sphere [indx];
		indx = Random.Range (0, steamSprite.Length);
		steamRender.sprite = steamSprite [indx];
		steam.transform.localPosition = steamOffset [indx];
		Invoke ("TimeToLife", lifeTime);
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		float realSpeed = speed * Time.fixedDeltaTime;
		transform.position = new Vector3 (transform.position.x - realSpeed, transform.position.y - realSpeed, transform.position.z); ;
	}

	void Update () {
	
	}

	private void TimeToLife(){
		Destroy (gameObject);
	}
}
