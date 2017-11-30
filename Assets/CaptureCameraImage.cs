﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CaptureCameraImage : MonoBehaviour {
	public GameObject cubeObj;
	public GameObject mainCam;

	private RenderTexture rt;
	private ObjectController oc;

	// Use this for initialization
	void Start () {
		//beginTime = Time.time;
		rt = gameObject.GetComponent<Camera> ().targetTexture;
		oc = mainCam.GetComponent<ObjectController> ();
	}

	public void CaptureImage() {
		var oldRT = RenderTexture.active;
		var tex = new Texture2D(rt.width, rt.height);
		RenderTexture.active = rt;

		tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		tex.Apply();

		byte[] pngBytes = tex.EncodeToJPG();
		StartCoroutine (Upload (pngBytes));

		Texture2D newTexture = new Texture2D (rt.width, rt.height);
		newTexture.LoadImage (pngBytes);
		cubeObj.GetComponent<Renderer> ().material.mainTexture = newTexture;

		RenderTexture.active = oldRT;
		print ("Capture Image!!");
	}

	IEnumerator Upload(byte[] pngBytes) {
		WWWForm wwwF = new WWWForm ();
		wwwF.AddBinaryData ("postcard", pngBytes);
		WWW www = new WWW ("http://35.196.236.27:3000/postcard/match/", wwwF);
		yield return www;

		if (www.error == null) {
			print ("Messaege: " + www.text);
			oc.detectGift (www.text);
		}
		else
			print ("Error: " + www.error.ToString ());
	}


	// Update is called once per frame
	void Update () {
		
	}
}