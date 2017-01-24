using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour {

	public Projectile projectile;
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
}
