using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GalleryController : MonoBehaviour {

	public Transform center;
	private Rigidbody rb;

	void Start() {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void explosion(Vector3 pos) {
		float stride = 10000f;
		float xx = Random.Range (-stride, stride);
		float yy = Random.Range (0, stride);
		float zz = Random.Range (-stride, stride);
		gameObject.transform.position = pos;
		gameObject.AddComponent<Rigidbody> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		rb.mass = 1000f;
		rb.AddForce (new Vector3 (xx, yy, zz));

		VideoPlayer vp = gameObject.GetComponent<VideoPlayer> ();
		AudioSource source = gameObject.GetComponent<AudioSource> ();
		if (vp != null)
			vp.Play ();

		if (source != null)
			source.Play ();
	}

	public void createObject(string type, string url) {
		if (type == "video") {
			gameObject.AddComponent<VideoPlayer> ();
			VideoPlayer player = gameObject.GetComponent<VideoPlayer> ();
			player.url = url;
			player.playOnAwake = true;
			player.isLooping = true;
			player.source = VideoSource.Url;
			player.Stop ();
		} else if (type == "image") {
			StartCoroutine (createImage (url));
		} else if (type == "audio") {
			StartCoroutine (createAudio (url));
			Destroy (gameObject.GetComponent<Rigidbody> ());
			Destroy (gameObject.GetComponent<BoxCollider> ());
			Destroy (gameObject.GetComponent<Renderer> ());
		}
	}

	IEnumerator createImage(string url) {
		WWW www = new WWW(url);

		// Wait for download to complete
		yield return www;

		// assign texture
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = www.texture;
	}

	IEnumerator createAudio(string url) {
		WWW www = new WWW (url);

		yield return www;

		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.clip = www.GetAudioClip ();
		source.loop = true;
		source.Stop ();
	}
}
