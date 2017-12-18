using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour {

	// Use this for initialization
	public GameObject expObj;
	public GameObject phyObj;
	public GameObject flameObj;

	private GameObject pictureObj;
	private GameObject clonedExploObj;

	//float beginTime;
	private bool isExplosed;
	private bool isRotated;

	void Start () {
		isExplosed = false;
		isRotated = false;
		//beginTime = Time.time;
		Vector3 cameraPos = Camera.main.gameObject.transform.position;
		Vector3 giftPos = gameObject.transform.position;
		gameObject.transform.LookAt (new Vector3(cameraPos.x, giftPos.y, cameraPos.z));
	}

	// Update is called once per frame
	void Update () {
		/*
		if (!isExplosed && Time.time - beginTime >= 5) {
			isExplosed = true;
			beginTime = Time.time;
			explosion ();
		}
		*/
		if (isRotated) {
			gameObject.transform.Rotate (new Vector3 (0, 4, 0));
		}
	}

	void FixedUpdate() {
		
	}

	public void StartRotate() {
		isRotated = true;
	}

	public void StopRotate() {
		isRotated = false;
	}

	public void setPictureObj(GameObject picture) {
		pictureObj = picture;
	}

	public void explosion() {
		GameObject clonedExploObj = Object.Instantiate (expObj, gameObject.transform.position, gameObject.transform.rotation);
		//GameObject clonedFlame = Object.Instantiate (flameObj, gameObject.transform.position, gameObject.transform.rotation);

		clonedExploObj.transform.parent = gameObject.transform.parent;

		GalleryController script = pictureObj.GetComponent<GalleryController> ();
		script.explosion (gameObject.transform);

		print ("Explosion!!");
		// isExplosed = true;
		// isRotated = true;
		// gameObject.SetActive (false);
	}
}
