using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservoirController : MonoBehaviour {

	public List<ParticleSystem> ParticleSystems;

	public float ScaledValue,levelResevoir;
	public void Awake() {
		
		foreach (Transform child in transform){
			ParticleSystems.Add (child.gameObject.GetComponent<ParticleSystem> ());
		}
	}

	public void setMaxParticle(float level) {
		levelResevoir = level;
		ScaledValue = ( levelResevoir * 500 / 1) + 40 ;
		foreach (var System in ParticleSystems) {
			var ps = System.main;
			ps.maxParticles = (int)ScaledValue;
		}
	}
}
