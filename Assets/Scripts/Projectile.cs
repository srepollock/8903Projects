using UnityEngine;

public class Projectile : MonoBehaviour {
    public float initialVelocity = 100f; // 100 m/(s^2)
	/// <summary>
    /// Current Speed
    /// </summary>
	public float speed = 0f;
	/// <summary>
    /// Constant acceleration;
    /// </summary>
	public float acceleration = -10f;
    /// <summary>
    /// Drag or wind resistance on the object.
    /// </summary>
	bool drag = false;
    /// <summary>
    /// If the object is stopped or not.
    /// </summary>
	bool stopped = true;

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!stopped) {
			StartRunning();
			if (drag) {
				acceleration = -(0.001f) * Mathf.Pow(speed, 2);
			}
			speed += acceleration * Time.deltaTime;
		}
    }

    void StartRunning()
    {
        if (!stopped)
        {
            speed = initialVelocity;
        }
    }
}