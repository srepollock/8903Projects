using UnityEngine;

public class Body : MonoBehaviour {
	public float m;
	public Position p;
	public float i;
	void Start() {
		p = new Position(this.transform.position.x, this.transform.position.y);
		i = (1.0f / 12.0f) * (m) * (Mathf.Pow(this.GetComponent<Transform>().lossyScale.x, 2) + 
			Mathf.Pow(this.GetComponent<Transform>().lossyScale.y, 2));
	}
}
