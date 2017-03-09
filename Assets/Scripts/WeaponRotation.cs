using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour {

	public Physics projectile;
	public TargetPosition target;
	public Text rangex, rangey, rangez, correctGunAngle, currentGunAngle, alphaText, gammaText;

	public float gravity = 9.8f;
	public float angle;
	
	bool canMove = true;

	// Use this for initialization
	void Start () {
		//updateCorrectAngle();
	}
	
	// Update is called once per frame
	void Update () {
		//updateAngle();
		//updateProjectile(projectile, angle);
		//updateText();
		updateProjectileAG(projectile, alphaAngleCalc(), gammaAngleCalc());
		updateCorrectAngle();
		updateText();
	}

	void LateUpdate()
	{
		if (canMove) 
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				// Rotate left
				this.transform.Rotate(new Vector3(0f, -1f, 0f));
				//projectile.rotate(this.transform.rotation);
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				// Rotate right
				this.transform.Rotate(new Vector3(0f, 1f, 0f));
				//projectile.rotate(this.transform.rotation);
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				// Rotate left
				this.transform.Rotate(new Vector3(0f, 0f, 1f));
				//projectile.rotate(this.transform.rotation);
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				// Rotate right
				this.transform.Rotate(new Vector3(0f, 0f, -1f));
				//projectile.rotate(this.transform.rotation);
			}
		}
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
		// 	canMove = !canMove;
		// }
	}

	void updateAngle()
	{
		currentGunAngle.text = Vector3.Angle(Vector3.right, 
			this.transform.right).ToString();
	}

	void updateProjectile(Physics _p, float angle)
	{
		_p.setTheta(angle);
	}

	/// <summary>
	/// Updates the projectile alpha and gamma angles.
	/// </summary>
	/// <param name="_p">Projectile object</param>
	/// <param name="_a">Alpha angle</param>
	/// <param name="_g">Gamma angle</param>
	void updateProjectileAG(Physics _p, float _a, float _g)
	{
		_p.setAlpha(new Vector3(_a, 0f, 0f));
		_p.setGamma(new Vector3(0f, 0f, _g));
	}

	float correctAngle(TargetPosition _t, Physics _p)
	{
		float 	distance, 
				velocity, 
				theta;
		distance = _t.transform.position.x - this.transform.position.x;
		velocity = _p.velocitySpeed;
		theta = (Mathf.Asin((gravity * distance) / Mathf.Pow(velocity, 2f)) / 2f);
		theta = 90 - (theta * Mathf.Rad2Deg);
		return theta;
	}

	void updateCorrectAngle()
	{
		angle = correctAngle(target, projectile);
		correctGunAngle.text = angle.ToString();
	}

	float alphaAngleCalc()
	{
		return 90 - this.transform.eulerAngles.z;
	}

	float gammaAngleCalc()
	{
		return this.transform.eulerAngles.y;
	}

	void updateText()
	{
		// 2D
		// updateRange2D();
		// 3D
		updateRange3D();
		alphaText.text = alphaAngleCalc().ToString();
		gammaText.text = gammaAngleCalc().ToString();
	}

	void updateRange2D()
	{
		rangex.text = "" + (target.transform.position.x 
			- this.transform.position.x);
		rangey.text = "" + (target.transform.position.y 
			- this.transform.position.y);
	}

	void updateRange3D()
	{
		// TODO Update Range
		rangex.text = "" + (target.transform.position.x 
			- this.transform.position.x);
		rangey.text = "" + (target.transform.position.y 
			- this.transform.position.y);
		rangez.text = "" + (target.transform.position.z 
			- this.transform.position.z);
	}
}
