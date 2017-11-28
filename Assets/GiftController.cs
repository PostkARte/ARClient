using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour {

	// Use this for initialization
	public GameObject expObj;
	public GameObject phyObj;
	public GameObject pictureObj;
	public GameObject flameObj;


	private GameObject clonedExploObj;

	float beginTime;
	bool isExplosed;

	void Start () {
		isExplosed = false;
		beginTime = Time.time;
		print (beginTime);
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

	}

	void FixedUpdate() {
		
	}

	public void explosion() {
		GameObject clonedExploObj = Object.Instantiate (expObj, gameObject.transform.position, gameObject.transform.rotation);
		//GameObject clonedFlame = Object.Instantiate (flameObj, gameObject.transform.position, gameObject.transform.rotation);

		clonedExploObj.transform.parent = gameObject.transform.parent;
		//clonedFlame.transform.parent = gameObject.transform.parent;

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Picture");
		for (int i = 0; i < objs.Length; ++i) {
			GameObject obj = objs [i];
			GalleryController script = obj.GetComponent<GalleryController> ();
			script.explosion (gameObject.transform.position);
			print ("Explosion: " + i.ToString());
		}

		print ("Explosion!!");
		gameObject.SetActive (false);
	}
}
