using UnityEngine;

public class Mass : MonoBehaviour {
	float mass;

	public Mass() { mass = 0; }
	public Mass(float _m) { mass = _m; }

	public float getMass() { return mass; }
	public void setMass(float _m) { mass = _m; }
}
