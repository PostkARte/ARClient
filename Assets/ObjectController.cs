﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectController : MonoBehaviour {

	public GameObject pictureObj;
	public GameObject parObj;
	public GameObject giftObj;
    public GameObject plane;
	public GameObject messageObj;
	public GameObject videoObj;
	public Text inputCode;

	private Transform toDrag;
	private float dist;
	private Vector3 offset; 
	private GameObject obj;
	private float fingerDist;
	private string data;

	// Use this for initialization
	void Start () {
		giftObj.SetActive (false);
		messageObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		/* Dragging GameObject */
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
			RaycastHit[] hits;
			hits = Physics.RaycastAll (ray);

			for (int i = 0; i < hits.Length; ++i) {
				RaycastHit hit = hits [i];
				if (hit.collider.gameObject.CompareTag ("Picture")) {
					print ("User Tap Object: " + hit.collider.gameObject.name);
					obj = hit.collider.gameObject;
					break;
				}
			}
		}

		if (obj && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero
			// get the touch position from the screen touch to world point
			Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.4f));
			Rigidbody rb = obj.GetComponent<Rigidbody> ();
			rb.AddForce ((touchedPos - obj.transform.position) * 5.0f * rb.mass);
		}



		/* Scalaing Gesture */
		if (Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit[] hits;
			hits = Physics.RaycastAll (ray);
			for (int i = 0; i < hits.Length; ++i) {
				RaycastHit hit = hits [i];
				if (hit.collider.gameObject.CompareTag ("Picture")) {
					print ("User Tap Object: " + hit.collider.gameObject.name);
					obj = hit.collider.gameObject;
					break;
				}
			}

			fingerDist = Vector3.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
		}

		if (obj && Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Moved) 
		{
			float scaleDist = Vector3.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);

			if (scaleDist > fingerDist) {
				float yy = (obj.transform.localScale.y + 0.05f);
				if (yy <= 2)	
					obj.transform.localScale += new Vector3 (0.05f, 0.05f, 0); 
			} 
			else if (scaleDist < fingerDist) {
				float yy = (obj.transform.localScale.y - 0.05f);
				if (yy >= 0.95f)
					obj.transform.localScale -= new Vector3 (0.05f, 0.05f, 0);
			}

			fingerDist = scaleDist;
		}

		if (obj && Input.touchCount <= 2 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			obj = null;
		}

		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
			RaycastHit[] hits;
			hits = Physics.RaycastAll (ray);

			for (int i = 0; i < hits.Length; ++i) {
				RaycastHit hit = hits [i];
				if (hit.collider.gameObject.CompareTag ("gift")) {
					GiftController exploScript = hit.collider.gameObject.GetComponent<GiftController> ();
					exploScript.explosion ();
					break;
				}

				if (hit.collider.gameObject.CompareTag("Picture"))
					break;
			}
		}
			
		BrakeObject (messageObj);
		BrakeObject (videoObj);
	}

	IEnumerator getJSON(string url) {
		print ("getJSON!!");
		WWW www = new WWW(url);
		yield return www;
		CodeInfo code = CodeInfo.CreateFromJSON (www.text);
		for (int i = 0; i < code.assets.Length; ++i) {
			GameObject clonedPic = Instantiate (pictureObj, new Vector3(1000, 1000, 1000), Quaternion.identity);
			clonedPic.transform.localScale = pictureObj.transform.localScale * 0.1f;
			clonedPic.tag = "Picture";
			clonedPic.transform.parent = parObj.transform;

			GalleryController galleryScript = clonedPic.GetComponent<GalleryController> ();
			galleryScript.createObject (code.assets[i].type, code.assets[i].url);

			print (code.assets[i].type + " " + code.assets [i].url);
		}

        
	}

	public void detectGift(string jsonString, Text status) {
		CodeInfo code = CodeInfo.CreateFromJSON (jsonString);

		if (code.code.Length < 5) {
			status.text = "Match Postcard Fail ... ";
			return;
		}
			
		plane.GetComponent<StaticMapService>().LoadMap(code.latitude, code.longitude);
		GameObject[] pictures = GameObject.FindGameObjectsWithTag ("Picture");
		for (int i = 0; i < pictures.Length; ++i) 
			Destroy (pictures [i]);

		for (int i = 0; i < code.assets.Length; ++i) {
			string type = code.assets [i].type;
			GameObject clonedPic = Instantiate (pictureObj, new Vector3(1000, 1000, 1000), Quaternion.identity);
			clonedPic.transform.localScale = pictureObj.transform.localScale * 0.1f;
			clonedPic.tag = "Picture";
			clonedPic.transform.parent = parObj.transform;

			GalleryController galleryScript = clonedPic.GetComponent<GalleryController> ();
			galleryScript.createObject (type, code.assets[i].url);

			if (type == "video") {
				videoObj = clonedPic;
				ToggleObject (clonedPic);
			}

			print (code.assets[i].type + " " + code.assets [i].url);
		}

		if (code.assets.Length == 0)
			status.text = "There is no pictures in postcard ..";
		else
			status.text = "Match postcard successfully !";
		
		giftObj.SetActive (true);

		setMessage (code.text);
	}

	public void createGift() {
		/* http://35.196.236.27:3000/postcard/code/V4GW63 */
		string code = inputCode.text;
		string url = "http://35.196.236.27:3000/postcard/code/" + code;
		print (url);

		GameObject[] pictures = GameObject.FindGameObjectsWithTag ("Picture");
		for (int i = 0; i < pictures.Length; ++i) {
			Destroy (pictures [i]);
		}

		StartCoroutine (getJSON(url));
		giftObj.SetActive (true);
	}

	public void toggleMessage() {
		ToggleObject (messageObj);
	}

	public void toggleVideo() {
		ToggleObject (videoObj);
	}

	private void setMessage(string text) {
		TextMeshPro textmeshPro = messageObj.GetComponent<TextMeshPro> ();
		textmeshPro.SetText (text);
		messageObj.transform.position = new Vector3 (0, 0, 0);
	}

	private void ToggleObject(GameObject obj) {
		if (obj != null) {
			obj.SetActive (!obj.activeSelf);
			if (obj.activeSelf) {
				obj.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 0, 50f));
			} else {
				obj.transform.position = new Vector3 (0, 0, 0);
			}
		}
	}

	private void BrakeObject(GameObject obj) {
		if (obj != null && obj.transform.position.z >= 1) {
			obj.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		}
	}

}

[System.Serializable]
public class AssetInfo
{
	public int id;
	public int postcard_id;
	public string type;
	public string url;
	public string uuid;
	public string created_at;
	public string updated_at;
}

[System.Serializable]
public class CodeInfo
{
	public int id;
	public string code;
	public string created_at;
	public string uuid;
	public string updated_at;
	public string postcard;
	public AssetInfo[] assets;
	public float latitude;
	public float longitude;
    public string text;

    public static CodeInfo CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<CodeInfo>(jsonString);
	}

	// Given JSON input:
	// {"name":"Dr Charles","lives":3,"health":0.8}
	// this example will return a PlayerInfo object with
	// name == "Dr Charles", lives == 3, and health == 0.8f.
	/*
	 * 
	 * 
	 * {"id":1,
	 *  "code":"3XQWX1",
	 *  "latitude":123,
	 *  "longitude":123,
	 *  "uuid":"7663d109-18d4-4928-92b5-a60ec9681a5a",
	 *  "created_at":"2017-11-18 08:11:41",
	 *  "updated_at":"2017-11-18 08:11:41",
	 *  "assets":[
	 *     {"id":1,
	 *      "type":"image",
	 *      "url":"http://35.196.236.27:3000/upload/image/cc2f9b99-060b-4d6e-a4c7-523cf70fdc6b.png",
	 *      "postcard_id":1,
	 *      "uuid":"cc2f9b99-060b-4d6e-a4c7-523cf70fdc6b",
	 *      "created_at":"2017-11-18 08:11:41",
	 *      "updated_at":"2017-11-18 08:11:41"
	 *     },
	 *     {"id":2,"type":"image","url":"http://35.196.236.27:3000/upload/image/0e6fd21c-9e97-4dce-8584-faa90356134d.png","postcard_id":1,"uuid":"0e6fd21c-9e97-4dce-8584-faa90356134d","created_at":"2017-11-18 08:11:41","updated_at":"2017-11-18 08:11:41"},
	 *     {"id":3,"type":"video","url":"http://35.196.236.27:3000/upload/video/9221abec-e352-45d9-9947-18fe9aa34826.mp4","postcard_id":1,"uuid":"9221abec-e352-45d9-9947-18fe9aa34826","created_at":"2017-11-18 08:11:41","updated_at":"2017-11-18 08:11:41"}
	 *    ]
	 * }
	 * */
}
