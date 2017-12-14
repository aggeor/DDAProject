using UnityEngine;
using System.Collections;

public class DestroyFinishedParticle : MonoBehaviour {

	private ParticleSystem thisParticleSystem;

	void Start () {
		thisParticleSystem = GetComponent<ParticleSystem> ();
	}
	void Update () {
		Destroy (gameObject, thisParticleSystem.duration);
	}
}
