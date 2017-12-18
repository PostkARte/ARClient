using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GalleryController : MonoBehaviour {

	public Transform center;

	private Vector3 goal;
	private Vector3 targetScale;
	private Rigidbody rb;
	private GameObject childObj;
	private GameObject giftObj;

	void Start() {
		childObj = gameObject.transform.GetChild (0).GetChild(0).gameObject;
		goal = Vector3.zero;
		targetScale = gameObject.transform.localScale;
		center = gameObject.transform;
	}

	// Update is called once per frame
	void Update () {
		/* Scaling the picture */
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, center.position + goal, Time.deltaTime * 2f);
		gameObject.transform.localScale = Vector3.Lerp (gameObject.transform.localScale, targetScale, Time.deltaTime * 2f);

		/* Always look at the camera */
		gameObject.transform.LookAt (
			new Vector3(
				Camera.main.transform.position.x, 
				gameObject.transform.position.y, 
				Camera.main.transform.position.z
			)
		);
	}

	public void SetGift(GameObject otherGiftObj) {
		giftObj = otherGiftObj;
	}

	public void StartRotate() {
		GiftController gc = giftObj.GetComponent<GiftController> ();
		gc.StartRotate ();
	}

	public void StopRotate() {
		GiftController gc = giftObj.GetComponent<GiftController> ();
		gc.StopRotate ();
	}

	public void explosion(Transform posTransform) {
		center = posTransform;
		//gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos, Time.deltaTime);
		gameObject.transform.position = center.position;
		gameObject.transform.localScale = Vector3.zero;

		goal = new Vector3(0, 0.1f, 0);
		targetScale = Vector3.one;
			
		gameObject.AddComponent<Rigidbody> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
		rb.mass = 1000f;
		//rb.AddForce (new Vector3 (xx, yy, zz));

		VideoPlayer vp = childObj.GetComponent<VideoPlayer> ();
		AudioSource source = childObj.GetComponent<AudioSource> ();
		if (vp != null)
			vp.Play ();

		if (source != null)
			source.Play ();

	}

	public void createObject(string type, string url) {
		if (type == "video") {
			StartCoroutine (createVideo (url));
		} else if (type == "image") {
			StartCoroutine (createImage (url));
		} else if (type == "audio") {
			StartCoroutine (createAudio (url));
		}
	}

	IEnumerator createImage(string url) {
		WWW www = new WWW(url);

		// Wait for download to complete
		yield return www;

		// assign texture
		Renderer renderer = childObj.GetComponent<Renderer>();
		renderer.material.mainTexture = www.texture;
	}

	IEnumerator createVideo(string url) {
		
		childObj.AddComponent<VideoPlayer> ();
		VideoPlayer player = childObj.GetComponent<VideoPlayer> ();
		player.url = url;
		player.playOnAwake = true;
		player.isLooping = true;
		player.source = VideoSource.Url;
		player.Stop ();
		yield return null;
	}

	IEnumerator createAudio(string url) {
		WWW www = new WWW (url);

		yield return www;

		AudioSource source = childObj.AddComponent<AudioSource> ();
		source.clip = www.GetAudioClip ();
		source.loop = true;
		source.Stop ();

		Destroy (childObj.GetComponent<Rigidbody> ());
		Destroy (childObj.GetComponent<BoxCollider> ());
		Destroy (childObj.GetComponent<Renderer> ());
	}
}
