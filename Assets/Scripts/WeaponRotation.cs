using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour {

	public Projectile projectile;
	public TargetPosition target;
	public Text rangex, rangey, correctGunAngle, currentGunAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
		{
			// Rotate left
			this.transform.Rotate(new Vector3(0f, -0.1f, 0f));
			projectile.rotate(this.transform.rotation);
		}
		if (Input.GetKey(KeyCode.D))
		{
			// Rotate right
			this.transform.Rotate(new Vector3(0f, 0.1f, 0f));
			projectile.rotate(this.transform.rotation);
		}
		updateAngle();
		updateCorrectAngle();
		updateProjectile();
	}

	void updateAngle()
	{
		currentGunAngle.text = Vector3.Angle(Vector3.forward, 
			this.transform.forward).ToString();
	}

	void updateProjectile()
	{
		
	}

	float correctAngle(TargetPosition _t, Projectile _p)
	{
		float distance, gravity, velocity;
		distance = _t.transform.position.x - _p.transform.position.x;
		gravity = -9.8f;
		velocity = _p.initialVelocity;
		float theta = Mathf.Sin(0.5f * (gravity * (distance / velocity) / velocity));
		return theta;
	}

	void updateCorrectAngle()
	{
		correctGunAngle.text = correctAngle(target, projectile).ToString();
	}
}
