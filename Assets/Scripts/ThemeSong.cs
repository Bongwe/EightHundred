using UnityEngine;
using System.Collections;

public class ThemeSong : MonoBehaviour {

	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	public void playThemeSong(AudioClip themeSong)
	{
		aud.clip = themeSong;
		aud.Play();
	}
}
