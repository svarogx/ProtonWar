using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour {

	public Image playerAvatar;
	public Animator coreAnim;
	public int coreFactor = 5;

	private int userScore = 0;
	private int beatScore;
	private bool newRecord = false;

	private string userName;
	private string userPath;

	private int coreCount = 0;

	public AudioClip gameAudio;
	public AudioClip alarmAudio;
	public AudioClip endAudio;
	private AudioSource gameSound;

	// Use this for initialization
	void Awake(){
		gameSound = GetComponent<AudioSource> ();
	}

	void Start () {
		gameSound.Stop ();
		gameSound.loop = true;
		gameSound.clip = gameAudio;
		gameSound.Play ();

		if (!PlayerPrefs.HasKey ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", 100);
			PlayerPrefs.Save ();
		}
		beatScore = PlayerPrefs.GetInt ("HighScore");
		if (PlayerPrefs.HasKey ("ActiveUser")) {
			userName = PlayerPrefs.GetString ("ActiveUser");
			userPath = PlayerPrefs.GetString ("ActiveAvatar");
			playerAvatar.sprite = Resources.Load<Sprite> (userPath);
		} else {
			userName = "";
			userPath = "";
			playerAvatar.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddScore(int score){
		userScore += score;
		if (userScore > beatScore) {
			newRecord = true;
			beatScore = userScore;
		}
	}

	public void LvlWRPlus(){
		coreCount += 1;
		LvlSetWarning ();
	}

	public void LvlWRMinus(){
		coreCount -= 1;
		LvlSetWarning ();
	}

	private void LvlSetWarning(){
		float coreLevel = (float)coreCount / (float)coreFactor;
		if (coreCount <= 0) {
			coreAnim.SetBool ("alert", false);
			coreAnim.SetFloat ("level", 0.0f);
			gameSound.clip = gameAudio;
		} else {
			coreAnim.SetBool ("alert", true);
			coreAnim.SetFloat ("level", coreLevel);
			gameSound.clip = alarmAudio;
		}
		if (coreCount <= 1) {
			gameSound.Stop ();
			gameSound.loop = true;
			gameSound.Play ();
		}
	}

	public void LvlLSReach(){
		gameSound.Stop ();
		gameSound.loop = false;
		gameSound.clip = endAudio;
		gameSound.Play ();
		GetComponent<ProtonBirth> ().StopGenerate ();
		GetComponent<GameInput> ().enabled = false;
		if (newRecord) {
			PlayerPrefs.SetInt ("HighScore", beatScore);
			PlayerPrefs.Save ();
		}
		Invoke ("NextLevel", 4.0f);
	}

	private void NextLevel(){
		SceneManager.LoadScene ("MainMenu");	
	}
}
