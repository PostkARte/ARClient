using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour {

	// Use this for initialization
	public GameObject expObj;
	public GameObject phyObj;
	public GameObject pictureObj;
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
		if (!isExplosed && Time.time - beginTime >= 2) {
			isExplosed = true;
			explosion (gameObject);
		}
		*/
	}

	public void explosion(GameObject parent) {
		GameObject exploObjCloned = Object.Instantiate (expObj, gameObject.transform.position, gameObject.transform.rotation);
		//GameObject phyObjCloned = Object.Instantiate (phyObj, gameObject.transform.position, gameObject.transform.rotation);

		for (int i = 0; i < 5; ++i) {
			GameObject clonedPic = Instantiate (pictureObj, gameObject.transform.position + new Vector3(0, 0, 0), gameObject.transform.rotation);
			clonedPic.transform.localScale = pictureObj.transform.localScale * 0.1f;
			clonedPic.tag = "Picture";
			clonedPic.transform.parent = parent.transform;
		}


		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Picture");
		for (int i = 0; i < objs.Length; ++i) {
			GameObject obj = objs [i];
			GalleryController script = obj.GetComponent<GalleryController> ();
			script.explosion ();
		}

		print ("Explosion!!");
	}
}
