using UnityEngine;

public class Movement : MonoBehaviour {

	public Car c;
	/// <summary>
    /// Starting speed
    /// </summary>
	public float initialVelocity = 100f; // 100 m/(s^2)
	/// <summary>
    /// Current Speed
    /// </summary>
	public float speed = 0f;
	/// <summary>
    /// Constant acceleration;
    /// </summary>
	public float acceleration = -10f;

	bool drag = false;
	bool stopped = true;

	float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L)) {
			drag = !drag;
		}
		if (Input.GetKeyDown(KeyCode.Backspace)) {
			c.transform.position = new Vector3(-300f, 0f, 350f);
			timer = 0;
			stopped = false;
			drag = true;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			c.transform.position = new Vector3(-300f, 0f, 350f);
			timer = 0;
			stopped = false;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			stopped = !stopped;
		}
	}

	void FixedUpdate() {
		if (!stopped) {
			StartRunning();
			timer += Time.deltaTime;
			if (drag) {
				acceleration = -(0.001f) * Mathf.Pow(speed, 2);
			}
			speed += acceleration * Time.deltaTime;
			c.transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		if (timer >= 8) { stopped = true; }
	}

	void UpdateTimeText (float t) {
		c.T.text = t + " seconds";
	}

	void UpdateVelocityText (float v) {
		c.V.text = v + " m/s^2";
	}

	void StartRunning() {
		if(timer == 0) {
			speed = initialVelocity;
		}
	}
}
