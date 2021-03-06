using UnityEngine;
using System.Collections;

public class Intersection : MonoBehaviour {
	
	public Transform[] connections;
	public bool locked = false;
	int tankLimit = 4;
	static int tankCount = 0;
	public GameObject tankPrefab;
	
	public static bool spawningEnabled;
	
	// Use this for initialization
	void Start () {
		spawningEnabled = false;
	}
	
	void OnDestroy() {
		tankCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(tankCount < tankLimit && Random.Range(0, 10) == 0) {
			SpawnTank();
		}
	}
	
	public Transform GetRandomConnection() {
		if(connections.Length == 0) {
			Debug.LogError("No connections!");
			return transform;
		}
		
		int i = Random.Range(0, connections.Length);

		for(int j = 0; j < connections.Length; j++) {
			Intersection intersection = connections[ (i+j) % connections.Length ].GetComponent<Intersection>();
			if(!intersection.locked) {
				intersection.locked = true;
				locked = false;
				return intersection.transform;
			}
		}
		
		// Couldn't find any unlocked connections. Return ourselves and let the tank try again next frame.
		return transform;
	}
	
	void SpawnTank() {
		if(locked) {
			return;
		}
		
		if(!spawningEnabled) {
			return;
		}
		
		Vector3 pos = transform.position + Vector3.up * 200f;
		GameObject go = (GameObject)Instantiate(tankPrefab, pos, Quaternion.identity);
		tankCount++;
		TankMovement tank = go.GetComponent<TankMovement>();
		tank.toIntersection = transform;
		locked = true;
	}
	
	public void TankDied() {
		locked = false;
		tankCount--;
	}
}
