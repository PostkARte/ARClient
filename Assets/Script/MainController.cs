using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public GameObject SharedComponents;
	public GameObject OnboardScreen;
	public GameObject MainScreen;
	public GameObject ReceiverScreen;

	private GameObject menu;


	private float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	private bool menuActive = false;

	// Use this for initialization
	void Start () {
		menu = SharedComponents.transform.Find ("Menu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3 (menuActive ? -225 : -575, 0, 0);
		menu.transform.localPosition = Vector3.SmoothDamp(menu.transform.localPosition, targetPosition, ref velocity, smoothTime);
	}

	public void OnMenuIconClick () {
		menuActive = !menuActive;
	}

	public void ChangeToMain () {
		SharedComponents.SetActive (true);
		OnboardScreen.SetActive (false);
		MainScreen.SetActive (true);
		ReceiverScreen.SetActive (false);
	}

	public void ChangeToReceiver () {
		SharedComponents.SetActive (true);
		OnboardScreen.SetActive (false);
		MainScreen.SetActive (false);
		ReceiverScreen.SetActive (true);
	}
}
