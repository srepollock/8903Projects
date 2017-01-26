using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour {

	public Projectile projectile;
	public TargetPosition target;
	public Text rangex, rangey, correctGunAngle, currentGunAngle;

	public float gravity = 9.8f;
	public float angle;

	// Use this for initialization
	void Start () {
		updateCorrectAngle();
	}
	
	// Update is called once per frame
	void Update () {
		updateAngle();
		updateProjectile(projectile, angle);
	}

	void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			// Rotate left
			this.transform.Rotate(new Vector3(0f, -0.1f, 0f));
			//projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			// Rotate right
			this.transform.Rotate(new Vector3(0f, 0.1f, 0f));
			//projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKey(KeyCode.W))
		{
			// Rotate left
			this.transform.Rotate(new Vector3(0f, -1f, 0f));
			//projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKey(KeyCode.S))
		{
			// Rotate right
			this.transform.Rotate(new Vector3(0f, 1f, 0f));
			//projectile.rotate(this.transform.rotation);
		}
	}

	void updateAngle()
	{
		currentGunAngle.text = Vector3.Angle(Vector3.forward, 
			this.transform.forward).ToString();
	}

	void updateProjectile(Projectile _p, float angle)
	{
		_p.angle = angle;
	}

	float correctAngle(TargetPosition _t, Projectile _p)
	{
		float 	distance, 
				velocity, 
				velocityY,
				theta;
		distance = _t.transform.position.x - this.transform.position.x;
		velocity = _p.initialVelocity;
		velocityY = -velocity;
		theta = 0.5f * Mathf.Asin((gravity * distance) / Mathf.Pow(velocity, 2));
		theta = theta * Mathf.Rad2Deg;
		return theta;
	}

	void updateCorrectAngle()
	{
		angle = correctAngle(target, projectile);
		correctGunAngle.text = angle.ToString();
	}
}
