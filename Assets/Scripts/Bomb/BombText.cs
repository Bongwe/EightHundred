using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BombText : MonoBehaviour {
	
	public Text bombText;
	public int bombsLeft;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*public void Update () {
		UpdateText ();
	}*/
	
	
	public void UpdateText(){
		bombText.text = " : " + bombsLeft;
	}
}
