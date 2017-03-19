using UnityEngine;

public class CollisionObject : MonoBehaviour {

	public Physics physics;
	public float mass;
	public Vector3 velocityInitial;
	public Vector3 velocity;
	public Vector3 omegao = Vector3.zero;
	public Vector3 omega;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!physics.getStopped()) {
			this.transform.Translate(velocity * Time.deltaTime);
		}
	}
}
