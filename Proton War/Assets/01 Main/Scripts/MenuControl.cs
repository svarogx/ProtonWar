using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public AudioClip backSound;
	public AudioClip buttonSound;
	public Animator logoAnim;
	public Animator playAnim;
	public Animator survivalAnim;
	public Animator recordAnim;
	public Animator cameraAnim;
	public GameObject spherePrefab;
	public GameObject stellarPrefab;

	private AudioSource menuAudio;
	private int menuIndex;

	public float sphereTime = 0.5f;
	public float stellarTime = 0.4f;
	public float speedUpTime = 15.0f;
	private Vector3 minVector;
	private Vector3 maxVector;

	void Awake(){
		menuAudio = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		menuIndex = 0;
		menuAudio.loop = true;
		menuAudio.clip = backSound;
		menuAudio.Play ();

		minVector = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
		maxVector = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height , 0.0f)); 
		InvokeRepeating ("SphereRain", sphereTime, sphereTime);
		InvokeRepeating ("StellarRain", stellarTime, stellarTime);
		Invoke ("SpeedUpRain", speedUpTime); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape))
				ShutGame ();
		} else {
			if (Input.GetKey (KeyCode.Escape))
				ShutGame ();
		}
	}

	private void SphereRain(){
		float vectX = Random.Range (minVector.x, maxVector.x * 2.0f);
		float vectY = maxVector.y + 1.0f;
		Vector3 spherePos = new Vector3 (vectX, vectY, 0.0f);
		Instantiate (spherePrefab, spherePos, Quaternion.identity);
	}

	private void StellarRain(){
		float vectX = Random.Range (minVector.x, maxVector.x * 2.0f);
		float vectY = maxVector.y + 2.0f;
		Vector3 stellarPos = new Vector3 (vectX, vectY, 0.0f);
		Instantiate (stellarPrefab, stellarPos, Quaternion.identity);
	}

	private void SpeedUpRain(){
		CancelInvoke ();
		InvokeRepeating ("SphereRain", sphereTime/2, sphereTime/2);
		InvokeRepeating ("StellarRain", stellarTime/2, stellarTime/2);
	}

	private void ButtonPush(){
		logoAnim.SetTrigger ("fadeout");
		playAnim.SetTrigger ("fadeout");
		survivalAnim.SetTrigger ("fadeout");
		recordAnim.SetTrigger ("fadeout");
		cameraAnim.SetTrigger ("zoomin");
		menuAudio.Stop ();
		menuAudio.loop = false;
		menuAudio.clip = buttonSound;
		menuAudio.Play ();
		Invoke ("ChangeScene", 2.0f);
	}

	public void ShutGame(){
		Application.Quit();
	}

	public void PlayGame(){
		if (menuIndex > 0)
			return;
		menuIndex = 1;
		ButtonPush ();
	}

	public void Survival(){
		if (menuIndex > 0)
			return;
		menuIndex = 2;
		ButtonPush ();
	}

	public void Records(){
		if (menuIndex > 0)
			return;
		menuIndex = 3;
		ButtonPush ();
	}

	private void ChangeScene(){
		if (menuIndex < 1)
			return;
		switch (menuIndex) {
		case 1:
			PlayerPrefs.SetInt ("GameMode", 0);
			PlayerPrefs.Save ();
			SceneManager.LoadScene("02 Avatar");
			break;
		case 2:
			PlayerPrefs.SetInt("GameMode",1);
			PlayerPrefs.Save ();
			SceneManager.LoadScene("02 Avatar");
			break;
		case 3:
			Debug.Log ("Escena 3");
			SceneManager.LoadScene("MainMenu");
			break;
		}
	}
}
