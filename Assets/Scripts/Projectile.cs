using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
	/// <summary>
	/// Reference to the weapon shooting the projectile.
	/// </summary>
	public WeaponRotation gun;
	public GameObject point;
    public float initialVelocity = 100f; // 100 m/(s^2)
	/// <summary>
    /// Current Speed
    /// </summary>
	public float speed = 100f;
	/// <summary>
    /// Constant acceleration;
    /// </summary>
	public float acceleration = -10f;
	public float gravity = 9.8f;
	public float angle = 0;
    public Text projectilex, projectiley, velocityText, timeText;

	// Rotation of the ball
	/// <summary>
	/// Radius of the projectile. Taken from the scale and set int awake;
	/// </summary>
	public float radius = 0;
	public float angularVelocity = 1.8f; // 1.8rad/s^2
	public float angularAcceleration = 0f; // toggle between 0 and 0.6rad/s^2
	public float ballAngle = 0f;

    /// <summary>
    /// Drag or wind resistance on the object.
    /// </summary>
	bool drag = false;
    /// <summary>
    /// If the object is stopped or not.
    /// </summary>
	bool stopped = true;

	float timer = 0;
	/// <summary>
	/// Position of the object
	/// </summary>
	float x = 0, y = 0;

    void Start()
    {

    }

    void Update()
    {
        if (!stopped) {
			StartRunning();
			timer += Time.deltaTime;
			float xdist = x * Time.deltaTime;
			float ydist = (y - (timer * gravity)) * Time.deltaTime;
			setPointPosition();
			this.transform.Translate(xdist, ydist, 0f, Space.World);
			if (timer >= 6f) stopped = true; // Stop at 6s
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		UpdatePositionText();
		if (Input.GetKeyDown(KeyCode.L)) 
        {
			drag = !drag;
		}
		if (Input.GetKeyDown(KeyCode.Backspace)
		||	Input.GetKeyDown(KeyCode.R)) 
        {
			resetPosition();
		}
		if (Input.GetKeyDown(KeyCode.Space)) 
        {
			stopped = !stopped;
		}
    }

    void StartRunning()
    {
        if(timer == 0) 
        {
			speed = initialVelocity;
		}
		x = Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
		y = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;
    }

    void UpdateTimeText (float t) 
    {
		timeText.text = t + " seconds";
	}

	void UpdateVelocityText (float v) 
    {
		velocityText.text = v + " m/s^2";
	}

	void resetPosition()
	{
		this.transform.position = gun.transform.position;
		timer = 0;
		stopped = false;
	}
	void setPointPosition()
	{
		this.transform.Rotate(0f, 0f, angularVelocity);
	}

	/// <summary>
	/// *DEPRICATED*
	/// Rotate the object with the quaterion passed in
	/// </summary>
	/// <param name="q">Quaterion to rotate by</param>
	public void rotate(Quaternion q)
	{
		this.transform.rotation = q;
	}

	void UpdatePositionText()
	{
		projectilex.text = this.transform.position.x.ToString();
		projectiley.text = this.transform.position.y.ToString();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Center")
		{
			stopped = true;
		}
	}
}