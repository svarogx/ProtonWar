using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContenderScript : MonoBehaviour {

	public int contenderID = -1;
	public Sprite contenderSprite;
	public string contenderName;
	public InputField contenderInput;

	private Image contenderCanvas;
	private AvatarControl avatarControl;

	void Awake(){
		contenderCanvas = GetComponent<Image> ();
		avatarControl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<AvatarControl> ();
	}

	void Start () {
		contenderCanvas.sprite = contenderSprite;
		contenderInput.text = contenderName;
	}
	
	void Update () {
	
	}

	public void ContenderClear(){
		contenderCanvas.color = Color.white;
	}

	public void ContenderClic(){
		contenderCanvas.color = Color.red;
		avatarControl.SelectContender (contenderID);
	}

	public void SaveContender(){
		if (contenderInput.text.Length > 0)
			PlayerPrefs.SetString("ActiveUser", contenderInput.text);
		else
			PlayerPrefs.SetString("ActiveUser", contenderName);
		PlayerPrefs.SetString("ActiveAvatar", contenderSprite.name);
		PlayerPrefs.Save ();
	}
}
