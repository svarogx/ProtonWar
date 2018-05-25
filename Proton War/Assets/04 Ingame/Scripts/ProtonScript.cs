using UnityEngine;
using System.Collections;

public class ProtonScript : MonoBehaviour {

	public int protonColor = -1;
	public int protonLimit = 7;
	public AudioClip protonFall;
	public AudioClip protonUnion;
	public AudioClip protonWarning;
	public Sprite[] proton;

	private SpriteRenderer protonRender;
	private AudioSource protonAudio;
	private CircleCollider2D protonCollider;

	void Awake(){
		protonRender = GetComponent<SpriteRenderer> ();
		protonAudio = GetComponent<AudioSource> ();
		protonCollider = GetComponent<CircleCollider2D> ();
	}

	void Start () {
		if (protonLimit > proton.Length)
			protonLimit = proton.Length;
		protonColor = Random.Range (0, protonLimit);
		protonRender.sprite = proton [protonColor];
		protonAudio.Stop ();
		protonAudio.loop = false;
		protonAudio.clip = protonFall;
		protonAudio.Play ();
	}


	void Update () {
	}

	void FixedUpdate(){
	}

	void OnTriggerEnter2D(Collider2D hit){
		if (hit.gameObject.tag == "Slot") {
			if (hit.gameObject.GetComponent<OddScript> ().slotState == -1) {
				protonAudio.Stop ();
				protonAudio.loop = false;
				protonAudio.clip = protonUnion;
				if (protonColor == 6)
					hit.gameObject.GetComponent<OddScript> ().ChangeColor (protonColor, false);
				else
					hit.gameObject.GetComponent<OddScript> ().ChangeColor (protonColor, true);
				switch (hit.gameObject.GetComponent<OddScript> ().slotLine) {
				case 4:
					if (Camera.main.orthographicSize <= 4)
						Camera.main.orthographicSize = 5;
					break;
				case 6:
					if (Camera.main.orthographicSize <= 5)
						Camera.main.orthographicSize = 6;
					break;
				case 8:
					protonAudio.clip = protonWarning;
					break;
				case 9:
					protonAudio.clip = protonWarning;
					break;
				case 10:
					GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControl>().LvlLSReach();
					break;
				}
				protonAudio.Play ();
				Invoke ("DestroyProton", 2.0f);
				protonRender.enabled = false;
				protonCollider.enabled = false;
			}
		} 
	}

	void OnCollisionEnter2D(Collision2D hit){
	}

	void DestroyProton(){
		Destroy (gameObject);
	}
}
