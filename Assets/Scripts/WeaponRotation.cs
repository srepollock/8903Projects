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

	void correctAngle(TargetPosition _t, Projectile _p)
	{
		float	x,
				velocityInitialX,
				velocityFinalX,
				accelerationX = 0,
				timeX = 0,
				y = 0,
				velocityInitialY,
				velocityFinalY,
				accelerationY = 9.81f,
				timeY;
		x = _t.transform.position.x - _p.transform.position.x;
		velocityInitialX = velocityFinalX = _p.initialVelocity;
		velocityFinalY = velocityInitialX;
		velocityInitialY = -1 * velocityInitialX;
		
	}
}
