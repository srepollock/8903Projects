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
	public float angle = 0;
    public Text projectilex, projectiley, velocityText, timeText;
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
			// if (drag) {
			// 	acceleration = -(0.001f) * Mathf.Pow(speed, 2);
			// }
			float xdist = x * Time.deltaTime;
			float ydist = (y - (timer * gravity)) * Time.deltaTime;
			this.transform.Translate(xdist, ydist, 0f);
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		UpdatePositionText();
		//if (timer >= 8) { stopped = true; } // TODO Stop when hitting the target
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
		if (col.gameObject.tag == "target")
		{
			stopped = true;
		}
	}
}