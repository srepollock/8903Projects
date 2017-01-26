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
	public float speed = 0f;
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

    void Start()
    {

    }

    void Update()
    {
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

    void FixedUpdate()
    {
		float 	x = Mathf.Cos(angle) * speed,
				y = Mathf.Sin(angle) * speed;
        if (!stopped) {
			StartRunning();
			timer += Time.deltaTime;
			// if (drag) {
			// 	acceleration = -(0.001f) * Mathf.Pow(speed, 2);
			// }
			float xdist = x * Time.deltaTime;
			float zdist = (y - timer * gravity) * Time.deltaTime;
			this.transform.Translate(xdist, 0f, zdist);
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		//if (timer >= 8) { stopped = true; } // TODO Stop when hitting the target
    }

    void StartRunning()
    {
        if(timer == 0) 
        {
			speed = initialVelocity;
		}
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

	public float getHeight()
	{
		float h = 0;

		return h;
	}
}