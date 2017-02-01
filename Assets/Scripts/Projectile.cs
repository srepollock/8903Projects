using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
	/// <summary>
	/// Reference to the weapon shooting the projectile.
	/// </summary>
	public WeaponRotation gun;
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
	/// <summary>
	/// Angle to fire (2D).
	/// </summary>
	public float angle = 0;
	/// <summary>
	/// Alpha angle (3D).
	/// </summary>
	public float alpha = 0;
	/// <summary>
	/// Gamma angle (3D).
	/// </summary>
	public float gamma = 0;
    public Text projectilex, projectiley, projectilez, velocityText, timeText;
    /// <summary>
    /// Drag or wind resistance on the object.
    /// </summary>
	bool drag = false;
    /// <summary>
    /// If the object is stopped or not.
    /// </summary>
	bool stopped = true;

	float timer = 0;
	float x = 0, y = 0;

    void Start()
    {

    }

    void Update()
    {
        if (!stopped) {
			StartRunning();
			timer += Time.deltaTime;
			//move2D();
			move3D(timer);
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		UpdatePositionText();
		//if (timer >= 8) { stopped = true; }
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
            // Shoot
			stopped = !stopped;
		}
    }

	void StartRunning()
    {
        if(timer == 0) 
        {
			speed = initialVelocity;
		}
    }
	/// <summary>
	/// Move in 2D space.
	/// </summary>
	void move2D()
	{
		x = Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
		y = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;
		if (drag) {
			acceleration = -(0.001f) * Mathf.Pow(speed, 2);
		}
		float xdist = x * Time.deltaTime;
		float ydist = (y - (timer * gravity)) * Time.deltaTime;
		this.transform.Translate(xdist, ydist, 0f);
	}
	/// <summary>
	/// Move in 3D space.
	/// </summary>
	/// <param name="_t">Total Time</param>
	void move3D(float _t)
	{
		float 	vx, vy, vz;
		float 	radAlpha = Mathf.Deg2Rad * alpha,
				radGamma = Mathf.Deg2Rad * gamma;
		vx = speed * Mathf.Sin(radAlpha) * Mathf.Cos(radGamma);
		vy = speed * Mathf.Cos(radAlpha) + (-gravity * _t);
		vz = speed * Mathf.Sin(radAlpha) * Mathf.Sin(radGamma) * -1; // -1 because Left Hand System
		//Debug.Log("Alpha: " + alpha + " Gamma: " + gamma);
		//Debug.Log("Vx: " + vx + " Vy: " + vy + " Vz: " + vz);
		Vector3 vf = new Vector3(vx, vy, vz) * Time.deltaTime;
		this.transform.Translate(vf);
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
		Debug.Log("Collision: " + col.gameObject.name);
		if (col.gameObject.name == "Center")
		{
			stopped = true;
		}
	}
}