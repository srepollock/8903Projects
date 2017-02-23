using UnityEngine;

public class Driver : MonoBehaviour {
	public float m = 100;
	public Position p;
	public float i;
	void Start() {
		p = new Position(this.transform.position.x, this.transform.position.y);
		i = m * (Mathf.Pow(this.GetComponent<Renderer>().bounds.size.x, 2) + 
			Mathf.Pow(this.GetComponent<Renderer>().bounds.size.z, 2)) / 12f;
	}
}
