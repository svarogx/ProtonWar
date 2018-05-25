using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoControl : MonoBehaviour {

	public void LogoPassOut(){
		SceneManager.LoadScene ("MainMenu");
	}

}
