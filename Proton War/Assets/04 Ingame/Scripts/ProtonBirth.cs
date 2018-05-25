using UnityEngine;
using System.Collections;

public class ProtonBirth : MonoBehaviour {

	public GameObject protonPrefab;
	public float timeMin;
	public float timeMax;
	public Vector3 birthPlace;

	void Start () {
		Invoke ("GenerateProton", Random.Range (timeMin, timeMax));
	}

	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateProton(){
		Instantiate (protonPrefab, birthPlace, Quaternion.identity);
		Invoke ("GenerateProton", Random.Range (timeMin, timeMax));
	}

	public void StopGenerate(){
		CancelInvoke ();
	}
}
