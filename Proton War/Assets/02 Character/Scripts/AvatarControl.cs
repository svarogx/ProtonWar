using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AvatarControl : MonoBehaviour {

	public ContenderScript[] contenders;
	public GameObject panelWindow;

	private int contenderSelect = -1;

	// Use this for initialization
	void Start () {
		contenderSelect = -1;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape))
				ButtonBackMenu ();
		} else {
			if (Input.GetKey (KeyCode.Escape))
				ButtonBackMenu();
		}
	}

	public void SelectContender(int contenderID){
		if (contenderID == contenderSelect)
			return;
		if (contenderSelect >= 0)
			contenders [contenderSelect].ContenderClear ();
		contenderSelect = contenderID;
	}

	public void ButtonBackMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void ButtonSelect(){
		if (contenderSelect < 0) {
			panelWindow.SetActive (true);
			return;
		}
		contenders [contenderSelect].SaveContender ();
		SceneManager.LoadScene ("circle");
	}

	public void PanelShutdown(){
		panelWindow.SetActive (false);
	}
}
