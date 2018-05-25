using UnityEngine;
using System.Collections;

public class OddScript : MonoBehaviour {

	public int slotLine;
	public int slotElement;
	public bool isFirst = false;
	public bool isLast = false;
	public bool isReady = false;
	public bool isProcessing = false;

	public int slotState = -2; 			// -2: Disable, -1: Available, 0...n: Color n

	private GameObject sideNeighbor1;	// debe ser private
	private GameObject sideNeighbor2;	// debe ser private
	private GameObject downNeighbor1;  	// debe ser private
	private GameObject downNeighbor2;  	// debe ser private
	private GameObject upNeighbor;  	// debe ser private
	public float downfactor = 1.1f;

	private SpriteRenderer slotRenderer;
	public Sprite[] proton;

	public GameObject scoreHigh;
	public GameObject scoreMedium;
	public GameObject scoreLow;

	private GameControl gameControl;

	void Awake(){
		slotRenderer = this.GetComponent<SpriteRenderer> ();
		gameControl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControl> ();
	}

	// Use this for initialization
	void Start () {
		slotRenderer.enabled = false;
		Invoke ("neighborsDetect", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void FixedUpdate(){
		if (!isReady)
			return;
		switch (slotState) {
		case -2:
			if (slotLine == 1)
				slotState = -1;
			else if (downNeighbor1.GetComponent<OddScript> ().slotState >= 0) {
				slotState = -1;
				if (downNeighbor2 != null) {
					if (downNeighbor2.GetComponent<OddScript> ().slotState >= 0)
						slotState = -1;
//					else
//						slotState = -1;
				} 
			}
			break;
		case -1:
			if (slotLine > 1) {
				if (downNeighbor1.GetComponent<OddScript> ().slotState <= -1) 
					slotState = -2;
				if (downNeighbor2 != null) {
					if (downNeighbor2.GetComponent<OddScript> ().slotState <= -1) 
						slotState = -2;
				}
			}
			break;
		default:
			if (slotLine > 1) {
				if (downNeighbor1.GetComponent<OddScript> ().slotState <= -1) {
					downNeighbor1.GetComponent<OddScript> ().slotState = -1;
					downNeighbor1.GetComponent<OddScript> ().ChangeColor (slotState, false);
					EmptySlot ();
				}
			}
			break;
		}
	}

	public void ChangeColor(int protonColor, bool isImpact){
		if (slotState == -1) {
			slotState = protonColor;
			slotRenderer.enabled = true;
			if (protonColor >= proton.Length)
				protonColor = proton.Length - 1;
			slotRenderer.sprite = proton [protonColor];
			if (isImpact)
				AtomVerify ();
			if (slotLine >= 8)
				gameControl.LvlWRPlus ();
		}
	}

	private void AtomVerify(){
		int popAtom = ProcessNode (slotState);
		if (popAtom > 3) {
			switch (popAtom) {
			case 4:
				Instantiate (scoreLow, transform.position, Quaternion.identity);
				break;
			case 5:
				Instantiate (scoreMedium, transform.position, Quaternion.identity);
				break;
			default:
				Instantiate (scoreHigh, transform.position, Quaternion.identity);
				break;
			}
			CleanNode (slotState, true);
		} else
			CleanNode (slotState, false);
		
	}

	public int ProcessNode(int colorCode){
		if (colorCode != slotState)
			return 0;
		if (isProcessing)
			return 0;
		isProcessing = true;
		int nodetmp = 1;
		if (sideNeighbor1 != null)
			nodetmp += sideNeighbor1.GetComponent<OddScript> ().ProcessNode (colorCode);
		if (sideNeighbor2 != null) 
			nodetmp += sideNeighbor2.GetComponent<OddScript> ().ProcessNode (colorCode);
		if (downNeighbor1 != null) 
			nodetmp += downNeighbor1.GetComponent<OddScript> ().ProcessNode (colorCode);
		if (downNeighbor2 != null) 
			nodetmp += downNeighbor2.GetComponent<OddScript> ().ProcessNode (colorCode);
		if (upNeighbor != null) 
			nodetmp += upNeighbor.GetComponent<OddScript> ().ProcessNode (colorCode);
		return nodetmp;
	}

	public void CleanNode(int colorCode, bool isDestroy){
		if (colorCode != slotState)
			return;
		if (!isProcessing)
			return;
		isProcessing = false;
		if (isDestroy)
			EmptySlot ();
		if (sideNeighbor1 != null)
			sideNeighbor1.GetComponent<OddScript> ().CleanNode (colorCode, isDestroy);
		if (sideNeighbor2 != null) 
			sideNeighbor2.GetComponent<OddScript> ().CleanNode (colorCode, isDestroy);
		if (downNeighbor1 != null) 
			downNeighbor1.GetComponent<OddScript> ().CleanNode (colorCode, isDestroy);
		if (downNeighbor2 != null) 
			downNeighbor2.GetComponent<OddScript> ().CleanNode (colorCode, isDestroy);
		if (upNeighbor != null) 
			upNeighbor.GetComponent<OddScript> ().CleanNode (colorCode, isDestroy);
	}

	public void EmptySlot(){
		if (slotLine >= 8)
			gameControl.LvlWRMinus ();
		slotState = -1;
		slotRenderer.enabled = false;
		slotRenderer.color = Color.white;
	}

	private void neighborsDetect(){
		float deltaRadius = 2.0f;
		if (isLast || isFirst)
			deltaRadius = 3.5f;
		float downdist1 = 10.0f, downdist2, updist = 10.0f;
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), this.transform.GetComponent<SpriteRenderer> ().bounds.extents.x * deltaRadius, LayerMask.NameToLayer("GamePlay"));
		foreach (Collider2D hit in hitColliders) {
			if (hit.gameObject.GetComponent<OddScript> ().slotLine == slotLine - 1) {
				if (Vector3.Distance (this.transform.position, hit.transform.position) < downdist1) {
					downdist2 = downdist1;
					downdist1 = Vector3.Distance (this.transform.position, hit.transform.position);
					if ((downdist2 <= (downfactor * downdist1)) && downdist1 < 2.0f)
						downNeighbor2 = downNeighbor1;
					else
						downNeighbor2 = null;
					downNeighbor1 = hit.gameObject;
				}
			}
			if (hit.gameObject.GetComponent<OddScript> ().slotLine == slotLine + 1) {
				if (Vector3.Distance (this.transform.position, hit.transform.position) < updist) {
					upNeighbor = hit.gameObject;
					updist = Vector3.Distance (this.transform.position, hit.transform.position);
				}
			}
			if (hit.gameObject.GetComponent<OddScript> ().slotLine == slotLine) {
				if (isFirst && hit.gameObject.GetComponent<OddScript>().isLast)
					sideNeighbor1 = hit.gameObject;
				else if (isLast && hit.gameObject.GetComponent<OddScript>().isFirst)
					sideNeighbor2 = hit.gameObject;
				else if (hit.gameObject.GetComponent<OddScript>().slotElement == slotElement - 1)
					sideNeighbor1 = hit.gameObject;
				else if (hit.gameObject.GetComponent<OddScript>().slotElement == slotElement + 1)
					sideNeighbor2 = hit.gameObject;
			}
		}
		isReady = true;
	}
}
