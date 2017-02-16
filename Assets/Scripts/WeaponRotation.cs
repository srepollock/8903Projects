using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour {

	public Projectile projectile;
	public TargetPosition target;
	public Text rangex, rangey, correctGunAngle, currentGunAngle;

	public float gravity = 9.8f;
	public float angle = 24.7f;

	// Use this for initialization
	void Start () {
		updateCorrectAngle();
		this.transform.Rotate(new Vector3(0f, 0f, 24.07f));
	}
	
	// Update is called once per frame
	void Update () {
		updateAngle();
		updateProjectile(projectile, angle);
		updateText();
	}

	void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			// Rotate left
			this.transform.Rotate(new Vector3(0f, 0f, 0.1f));
			//projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			// Rotate right
			this.transform.Rotate(new Vector3(0f, 0f, -0.1f));
			//projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKey(KeyCode.W))
		{
			// Rotate left
			this.transform.Rotate(new Vector3(0f, 0f, 1f));
			//projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKey(KeyCode.S))
		{
			// Rotate right
			this.transform.Rotate(new Vector3(0f, 0f, -1f));
			//projectile.rotate(this.transform.rotation);
		}
	}

	void updateAngle()
	{
		currentGunAngle.text = Vector3.Angle(Vector3.right, 
			this.transform.right).ToString();
	}

	void updateProjectile(Projectile _p, float angle)
	{
		_p.angle = angle;
	}

	float correctAngle(TargetPosition _t, Projectile _p)
	{
		float 	distance, 
				velocity, 
				theta;
		distance = _t.transform.position.x - this.transform.position.x;
		velocity = _p.initialVelocity;
		theta = (Mathf.Asin((gravity * distance) / Mathf.Pow(velocity, 2f)) / 2f);
		theta = theta * Mathf.Rad2Deg;
		return theta;
	}

	void updateCorrectAngle()
	{
		angle = correctAngle(target, projectile);
		correctGunAngle.text = angle.ToString();
	}

	void updateText()
	{
		updateRange();
	}

	void updateRange()
	{
		rangex.text = "" + (target.transform.position.x 
			- this.transform.position.x);
		rangey.text = "" + (target.transform.position.y 
			- this.transform.position.y);
	}
}
