using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bullets : MonoBehaviour {
	
	public Text amoText;
	public int amoLeft;
	
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	public void Update () {
		UpdateText ();
	}
	
	
	public void UpdateText(){
		amoText.text = " : " + amoLeft;
	}
}
