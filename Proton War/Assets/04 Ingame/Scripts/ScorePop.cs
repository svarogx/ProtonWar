using UnityEngine;
using System.Collections;

public class ScorePop : MonoBehaviour {

	public Sprite[] scoreSprites;
	public int scoreValue;
	public float timeToLife = 2.0f;

	private GameControl gameControl;
	private SpriteRenderer scoreRender;

	void Awake(){
		gameControl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControl>();
		scoreRender = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		if (scoreSprites.Length > 0) {
			int indx = Random.Range (0, scoreSprites.Length);
			scoreRender.sprite = scoreSprites [indx];
		}
		Invoke ("ScoreDestroy", timeToLife);
		gameControl.AddScore (scoreValue);
	}
	
	private void ScoreDestroy(){
		Destroy (gameObject);
	}
}
