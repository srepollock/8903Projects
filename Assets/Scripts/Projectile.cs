using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {
	/// <summary>
	/// Reference to the weapon shooting the projectile.
	/// </summary>
	public WeaponRotation gun;
	/// <summary>
	/// Reference to point on the bullet to measure rotation.
	/// </summary>
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
	/// <summary>
	/// Gravity
	/// </summary>
	public float gravity = 9.8f;
	/// <summary>
	/// Angle to fire and move
	/// </summary>
	public float angle = 0;
    public Text projectilex, projectiley, velocityText, timeText, angularDisplacement,
		angularVelocity, angularAcceleration, pointPosition, pointVelocity, pointAcceleration;

	// Rotation of the ball
	/// <summary>
	/// Radius of the projectile. Taken from the scale and set int awake;
	/// </summary>
	public float radius = 0;
	/// <summary>
	/// Omega
	/// </summary>
	public float omegao = 1.8f; // 1.8rad/s^2
	float omega = 0f;
	Vector3 omegaV;
	float theta = 0f;
	Vector3 RV;
	Vector3 projVelocity;
	Vector3 projAcceleration;
	Vector3 rotV;
	Vector3 alphaV;
	Vector3 rotA;
	/// <summary>
	/// Alpha
	/// </summary>
	public float alphao = 0f; // toggle between 0 and 0.6rad/s^2
	/// <summary>
	/// Theta of the rotation
	/// </summary>
	public float rotationThetao = 0f;

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
			this.transform.Translate(xdist, ydist, 0f, Space.World);
			rotateProjectile(x, y - (timer * gravity));
			if (timer >= 6f) stopped = true; // Stop at 6s
		}
		UpdateTimeText(timer);
		UpdateVelocityText(speed);
		UpdatePositionText();
		UpdateText();
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
	/// <summary>
	/// Setup the object to start moving
	/// </summary>
    void StartRunning()
    {
        if(timer == 0) 
        {
			speed = initialVelocity;
		}
		x = Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
		y = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;
    }
	/// <summary>
	/// Updates the time text
	/// </summary>
	/// <param name="t">Time</param>
    void UpdateTimeText (float t) 
    {
		timeText.text = t + " seconds";
	}
	/// <summary>
	/// Updates the velocity text
	/// </summary>
	/// <param name="v">Velocity</param>
	void UpdateVelocityText (float v) 
    {
		velocityText.text = v + " m/s^2";
	}
	/// <summary>
	/// Reset the position of the object.
	/// </summary>
	void resetPosition()
	{
		this.transform.position = gun.transform.position;
		timer = 0;
		stopped = false;
	}
	void rotateProjectile(float xdist, float ydist)
	{
		omega = updatingOmega(omegao, alphao, timer);
		omegaV = new Vector3(0f, 0f, omega);
		theta = calculateRotationAngle(rotationThetao, omegao, timer, alphao);
		RV = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
		projVelocity = new Vector3(xdist, ydist, 0f);
		rotV = calculateRotationVelocity(projVelocity, omegaV, RV);
		alphaV = new Vector3(0f, 0f, alphao);
		projAcceleration = new Vector3(0f, -gravity, 0f);
		rotA = calculateRotationAcceleration(projAcceleration, omegaV, alphaV, RV);
		this.transform.Rotate(omegaV * Mathf.Rad2Deg * Time.deltaTime, Space.World);
	}
	/// <summary>
	/// Calculate the updating omega
	/// </summary>
	/// <param name="_omegao">Initial Omega</param>
	/// <param name="_alpha">Alpha</param>
	/// <param name="_t">Total time</param>
	/// <returns>Omega</returns>
	float updatingOmega(float _omegao, float _alpha, float _t)
	{
		return _omegao + (_alpha * _t);
	}
	/// <summary>
	/// Calculate the rotation velocity
	/// </summary>
	/// <param name="_v">Velocity of object</param>
	/// <param name="_w">Omega</param>
	/// <param name="_R">Radius</param>
	/// <returns>Velocity of rotation</returns>
	Vector3 calculateRotationVelocity(Vector3 _v, Vector3 _omega, Vector3 _R)
	{
		return _v + Vector3.Cross(_omega, _R);
	}
	/// <summary>
	/// Calculate the rotation acceleration of the object.
	/// </summary>
	/// <param name="_acg">Accleration of the object</param>
	/// <param name="_omega">Omega</param>
	/// <param name="_alpha">Alpha</param>
	/// <param name="_R">Radius</param>
	/// <returns>Acceleration of rotation</returns>
	Vector3 calculateRotationAcceleration(Vector3 _acg, Vector3 _omega, Vector3 _alpha, Vector3 _R)
	{
		return _acg + 
			Vector3.Cross(_alpha, _R) + 
			Vector3.Cross(_omega, Vector3.Cross(_omega, _R));
	}
	/// <summary>
	/// Calculate rotation angle from the point to the center at the time.
	/// </summary>
	/// <param name="_thetao">Theta initial</param>
	/// <param name="_omegao">Omega intial</param>
	/// <param name="_t">Total time</param>
	/// <param name="_alpha">Alpha</param>
	/// <returns>Theta at time</returns>
	float calculateRotationAngle(float _thetao, float _omegao, float _t, float _alpha)
	{
		return _thetao + (_omegao * _t) + (0.5f * _alpha * Mathf.Pow(_t, 2));
	}
	void setPointPosition(float _omega)
	{
		this.transform.Rotate(0f, 0f, _omega);
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
	/// <summary>
	/// Update positioning text
	/// </summary>
	void UpdatePositionText()
	{
		projectilex.text = this.transform.position.x.ToString();
		projectiley.text = this.transform.position.y.ToString();
	}
	/// <summary>
	/// Update all text for the projectile.
	/// </summary>
	void UpdateText()
	{
		angularDisplacement.text = theta + " rad";
		angularVelocity.text = omega + " rad/s";
		angularAcceleration.text = alphao + " rad/s^2";
		pointPosition.text = "(" + point.transform.position.x + "m, " + point.transform.position.y + "m)";
		pointVelocity.text = "(" + rotV.x + "m/s, " + rotV.y + "m/s)";;
		pointAcceleration.text = "(" + rotA.x + "m/s^2, " + rotA.y + "m/s^2)";
	}
	/// <summary>
	/// Gather triggers for collison on entering the trigger
	/// </summary>
	/// <param name="col">Collider to this object</param>
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Center")
		{
			stopped = true;
		}
	}
}