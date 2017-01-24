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
        if (!stopped) {
			StartRunning();
			timer += Time.deltaTime;
			if (drag) {
				acceleration = -(0.001f) * Mathf.Pow(speed, 2);
			}
			speed += acceleration * Time.deltaTime;
			//c.transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		if (timer >= 8) { stopped = true; } // TODO Stop when hitting the target
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
}