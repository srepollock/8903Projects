using UnityEngine;

public class Movement : MonoBehaviour {

	public Car c;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
			c.transform.position += new Vector3(0f, 10f, 0f);
		} else if (Input.GetKeyDown(KeyCode.A)) {
			c.transform.position += new Vector3(-10f, 0f, 0f);
		} else if (Input.GetKeyDown(KeyCode.S)) {
			c.transform.position += new Vector3(0f, -10f, 0f);
		} else if (Input.GetKeyDown(KeyCode.D)) {
			c.transform.position += new Vector3(10f, 0f, 0f);
		}
	}
}
