using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public GameObject OnboardScreen;
	public GameObject MainScreen;
	public GameObject ReceiverScreen;
	public GameObject IconMenu;
	public GameObject Header;
	public GameObject Sphere;

	private GameObject menu;


	private float smoothTime = 0.2F;
	private Vector3 velocity = Vector3.zero;
	private bool menuActive = false;

	// Use this for initialization
	void Start () {
		menu = this.transform.Find ("Menu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3 (menuActive ? -300 : -505, 0, 0);
		menu.transform.localPosition = Vector3.SmoothDamp(menu.transform.localPosition, targetPosition, ref velocity, smoothTime);
	}

	public void OnMenuIconClick () {
		menuActive = !menuActive;
	}

	public void ChangeToMain () {
		IconMenu.SetActive (true);
		Header.SetActive (true);
		OnboardScreen.SetActive (false);
		MainScreen.SetActive (true);
		ReceiverScreen.SetActive (false);
	}

	public void ChangeToReceiver () {
		OnboardScreen.SetActive (false);
		MainScreen.SetActive (false);
		ReceiverScreen.SetActive (true);
	}

	public void Toggle360 () {
		Sphere.SetActive (!Sphere.activeSelf);
	}

	public void OnSenderClick () {
		Application.OpenURL("https://postkarte.github.io/ClientWeb");
	}
}
