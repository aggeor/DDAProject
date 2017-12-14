using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public PlayerController player;
	private Vector3 offset;
	public float smoothSpeed = 0.125f;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	void Start () {
		if (player!= null) {
			offset = transform.localPosition - player.transform.localPosition;
		}
	}

	void LateUpdate(){
		if (player!= null) {
			//transform.position = player.transform.position + offset;
			Vector3 desiredPosition = player.transform.localPosition + offset;
			//Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
			Vector3 smoothedPosition = Vector3.SmoothDamp (transform.localPosition, desiredPosition,ref velocity,smoothTime);
			transform.localPosition = smoothedPosition ;
		}
	}
}
