using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour {
	
	public GameObject debrisPrefab;
	int numDebris = 10;
	public float velocity = 5f;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < numDebris; i++) {
			GameObject d = (GameObject)Instantiate(debrisPrefab, transform.position, Random.rotation);
			d.rigidbody.AddForce( Random.insideUnitSphere * velocity + Vector3.up * velocity/2, ForceMode.Impulse );
		}
	}
}
