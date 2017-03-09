using UnityEngine;
using UnityEngine.UI;

public class Physics : MonoBehaviour {
#region Public Physics Variabls
    /// <summary>
    /// Velocity of the object.
    /// </summary>
    public Vector3 velocity;
    /// <summary>
    /// Acceleration of the object.
    /// </summary>
    public Vector3 acceleration;
	/// <summary>
	/// Gravity (-9.81m/s^2) in the Y dirction of the world.
	/// </summary>
	public Vector3 gravity = new Vector3(0.0f, -9.81f, 0.0f);
    /// <summary>
    /// Mass of the object in kg.
    /// </summary>
    public float mass;
    /// <summary>
    /// Radius of the object to be rotated (basically width)
    /// </summary>
    public float radius;
    /// <summary>
    /// Force Vector of the object
    /// </summary>
    public Vector3 force;
	/// <summary>
    /// Drag coefficient. Used in boat project
    /// </summary>
    public float dragCoefficient;
#endregion
#region Private Physics Variables
    /// <summary>
    /// Thrust to move the object (in water). Should remain constant for test 
    /// purposes
    /// </summary>
    public float thrust;
    /// <summary>
    /// Boolean for stopping the objects movement.
    /// </summary>
	bool stopped = true;
    /// <summary>
    /// Boolean for stopping the acceleration of an object for constant accelration.
    /// </summary>
	bool stopAcceleration = false;
	/// <summary>
	/// 
	/// </summary>
	bool alphaBool = false;
	/// <summary>
	/// Boolean for turning wind resistance on or off.
	/// </summary>
	bool windResistance = false;
    /// <summary>
    /// Object Velocity
    /// </summary>
    Vector3 objectVelocity;
    /// <summary>
    /// Object Acceleration
    /// </summary>
    Vector3 objectAcceleration;
    /// <summary>
    /// Force(forceX, forceY, forceZ) Vector
    /// </summary>
    Vector3 F;
    /// <summary>
    /// Radial Vector from COM to Force Origin
    /// </summary>
    Vector3 r;
    /// <summary>
    /// Moment of Inertia
    /// </summary>
    float I;
    /// <summary>
    /// Initial Angular Velocity
    /// </summary>
    Vector3 omegao = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Angular Velocity
    /// </summary>
    Vector3 omega;
    Vector3 previousOmega = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Initial motion about the X axis of the object in 2D/3D particle 
	/// kinematics
    /// </summary>
    Vector3 alphao;
    /// <summary>
    /// Angular Acceleration
	/// Or Motion about the X axis in 2D/3D Particle kinematics.
    /// </summary>
    Vector3 alpha;
	/// <summary>
	/// Initial motion about the Z axis of the object in 2D/3D particle 
	/// kinematics
	/// </summary>
	Vector3 betao;
	/// <summary>
	/// Motion about the Y axis in 2D/3D particle kinemtics
	/// </summary>
	Vector3 beta;
	/// <summary>
	/// Initial Gamma of the object.
	/// </summary>
	Vector3 gammao;
	/// <summary>
	/// Motion about the Z axis in 3D Partical kinematics.
	/// </summary>
	Vector3 gamma;
    /// <summary>
    /// Initial Angle of Rotation of the object
    /// </summary>
    float thetao = 0f;
    /// <summary>
    /// Theta for the rotation
    /// </summary>
    float theta;
    /// <summary>
    /// Distance to move per frame. Used in boat project
    /// </summary>
    float distance = 0f;
    /// <summary>
    /// Depth of the object. Used in boat project
    /// </summary>
    float depth = 0f;
    /// <summary>
    /// Current total time of object based on Timer.deltaTime additions
    /// </summary>
    float timer = 0f;
#endregion
#region UI Text Variables
	// End all with `...Text` for simplicity
	public Text velocityText;
    public Text timeText;
#endregion
#region Private Physics Variables Setters/Getters
    /// <summary>
    /// Update the timer with the value passed in.
    /// </summary>
    /// <param name="_dt">Delta Time</param>
    public void updateTimer(float _dt) {
        timer += _dt;
    }
    /// <summary>
    /// Get the value the timer is at.
    /// </summary>
    /// <returns>Time</returns>
    public float getTimer() {
        return timer;
    }
    /// <summary>
    /// Reset the timer for the class.
    /// </summary>
    public void resetTimer() {
        timer = 0f;
    }
    /// <summary>
    /// Sets the mass of the object
    /// </summary>
    /// <param name="_m">Mass to set too.</param>
    public void setMass(float _m) {
        mass = _m;
    }
    /// <summary>
    /// Adds mass to current mass.
    /// </summary>
    /// <param name="_m">Mass to add.</param>
    public void addMass(float _m) {
        mass += _m;
    }
    /// <summary>
    /// Removes mass to current mass.
    /// </summary>
    /// <param name="_m">Mass to subtract.</param>
    public void subMass(float _m) {
        mass -= _m;
    }
    /// <summary>
    /// Gets the mass of the object
    /// </summary>
    /// <returns>Mass of the object.</returns>
    public float getMass() {
        return mass;
    }
    /// <summary>
    /// Get the omega
    /// </summary>
    /// <returns>Omega</returns>
    public Vector3 getOmega() {
        return omega;
    }
    /// <summary>
    /// Resets the omega for the class
    /// </summary>
    public void resetOmega() {
        omega = omegao;
    }
    /// <summary>
    /// Get the X axis movement of the object.
    /// </summary>
    /// <returns>Alpha</returns>
    public Vector3 getAlpha() {
        return alpha;
    }
	/// <summary>
	/// Set the alpha of the object.
	/// </summary>
	/// <param name="_a">Vector of Y axis movement</param>
	public void setAlpha(Vector3 _a) {
		alpha = _a;
	}
	/// <summary>
	/// Get the Y axis movement of the object.
	/// </summary>
	/// <returns>Vector3 of beta</returns>
	public Vector3 getBeta() {
		return beta;
	}
	/// <summary>
	/// Set the Y axis movement of the object.
	/// </summary>
	/// <param name="_b">Vector of Y axis movement</param>
	public void setBeta(Vector3 _b) {
		beta = _b;
	}
	/// <summary>
	/// Get the Z axis movement of the object.
	/// </summary>
	/// <returns>Vector3 of gamma</returns>
	public Vector3 getGamma() {
		return gamma;
	}
	/// <summary>
	/// Set gamma for the object.
	/// </summary>
	/// <param name="_g">Vector of Y axis movement</param>
	public void setGamma(Vector3 _g) {
		gamma = _g;
	}
    /// <summary>
    /// Resets the alpha for the class
    /// </summary>
    public void resetAlpha() {
        alpha = alphao;
    }
    /// <summary>
    /// Sets theta
    /// </summary>
    /// <param name="t">Value to set theta to</param>
    public void setTheta(float _t) {
        theta = _t;
    }
    /// <summary>
    /// Get the theta
    /// </summary>
    /// <returns>Theta</returns>
    public float getTheta() {
        return theta;
    }
    /// <summary>
    /// Resets the theta for the class
    /// </summary>
    public void resetTheta() {
        theta = thetao;
    }
    /// <summary>
    /// Sets the forceX
    /// </summary>
    /// <param name="x">Force X</param>
    public void setForceX(float x) {
        force.x = x;
    }
    /// <summary>
    /// Sets the forceY
    /// </summary>
    /// <param name="y">Force Y</param>
    public void setForceY(float y) {
        force.y = y;
    }
    /// <summary>
    /// Sets the forceZ
    /// </summary>
    /// <param name="z">Force Z</param>
    public void setForceZ(float z) {
        force.z = z;
    }
    /// <summary>
    /// Sets the Force Vector
    /// </summary>
    /// <param name="f">Force Vector created</param>
    public void setForceVector(Vector3 _f) {
        F = _f;
    }
    /// <summary>
    /// Returns the Force Vector
    /// </summary>
    /// <returns>Force Vector</returns>
    public Vector3 getForceVector() {
        return F;
    }
    /// <summary>
    /// Sets the Radial Vector
    /// </summary>
    /// <param name="_r">Radial Vector created</param>
    public void setR(Vector3 _r) {
        r = _r;
    }
    /// <summary>
    /// Gets the Radial Vector
    /// </summary>
    /// <returns>Radial Vector</returns>
    public Vector3 getRadialVector() {
        return r;
    }
#endregion
    void Start() {

    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            stopped = !stopped;
        }
        updateText();
    }
    void FixedUpdate() {
        if (timer >= 12f) stopped = true;
		moveProjectile3D(timer, Time.deltaTime);
		if (!stopped) updateTimer(Time.deltaTime);
    }

    /// <summary>
    /// Calculate the Object Acceleration
    /// </summary>
    /// <param name="_F">Force Vector</param>
    /// <param name="_M">Mass of Object</param>
    /// <returns>Acceleration Vector</returns>
    Vector3 calculateObjectAcceleration(Vector3 _F, float _M) {
        return _F / _M;
    }

    /// <summary>
    /// Calculate the Object Velocity
    /// </summary>
    /// <param name="_a">Acceleration Vector</param>
    /// <param name="_t">Time</param>
    /// <returns>Object Velocity</returns>
    Vector3 calculateObjectVelocity(Vector3 _a, float _t) {
        return _a * _t;
    }

    /// <summary>
	/// Calculate the updating Angular Velocity (omega)
	/// </summary>
	/// <param name="_omegao">Initial Omega</param>
	/// <param name="_alpha">Alpha</param>
	/// <param name="_t">Total time</param>
	/// <returns>Omega</returns>
	Vector3 calculateAngularVelocity(Vector3 _omega, Vector3 _alpha, float _t)
	{
        float curOmega = _omega.z + (_alpha.z * _t);
        previousOmega.z = curOmega;
		return new Vector3(0f, 0f, curOmega);
	}

    /// <summary>
	/// Calculate the Rotation Velocity
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
	/// Calculate the Rotation Acceleration (alpha)
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
	/// <param name="_omegao">Angular Velocity</param>
	/// <param name="_t">Total time</param>
	/// <param name="_alpha">Angular Acceleration</param>
	/// <returns>Theta at time</returns>
	float calculateRotationAngle(float _thetao, Vector3 _omega, Vector3 _alpha, float _t)
	{
        Debug.Log("Alpha: " + alpha.z);
		return _thetao + (_omega.z * _t) + (0.5f * _alpha.z * Mathf.Pow(_t, 2));
	}

    /// <summary>
    /// Creates the torque vector
    /// </summary>
    /// <param name="_r">Radial Vector</param>
    /// <param name="_F">Force Vector</param>
    /// <returns>Torque Vector</returns>
    Vector3 calculateTorque(Vector3 _r, Vector3 _F) {
        return Vector3.Cross(_r, _F);
    }

	/// <summary>
	/// Creates the r vector for rotation
	/// </summary>
	/// <param name="arrow">Arrow postion</param>
	/// <returns>r Vector3</returns>
	Vector3 arrowToCOM(Vector3 arrow, Vector3 COM) {
		return new Vector3(arrow.x - COM.x, arrow.y - COM.y, 0f);
	}

    /// <summary>
    /// Calculates the Angular Acceleration
    /// </summary>
    /// <param name="_r">Radial Vector</param>
    /// <param name="_F">Force Vector</param>
    /// <param name="_I">Moment of Inertia</param>
    /// <returns>Angular Acceleration Vector</returns>
    Vector3 calculateAngularAcceleration(Vector3 _r, Vector3 _F, float _I) {
        Vector3 rxf = calculateTorque(_r, _F);
        float x = Mathf.Abs(rxf.x) / _I;
        float y = Mathf.Abs(rxf.y) / _I;
        float z = Mathf.Abs(rxf.z) / _I;
        return new Vector3(x, y, z);
    }

// // DEPRICATED
//     /// <summary>
//     /// Initialize the Rotation values
//     /// </summary>
//     public void initCarRotation(Arrow arrow, Car car) {
//         resetTheta();
//         resetOmega();
//         r = arrowToCOM(arrow.transform.position, car.COM);
//         I = car.getInertiaCar();
//         alpha = (calculateAngularAcceleration(r, force, I));
//         resetTimer();
//     }
// DEPRICATED
    // /// <summary>
    // /// Rotation loop of the object
    // /// </summary>
    // public void rotateCarLoop(Car car, Arrow arrow) {
    //     r = arrowToCOM(arrow.transform.position, car.COM);
    //     theta = calculateRotationAngle(thetao, previousOmega, alpha, timer);
    //     omega = calculateAngularVelocity(omega, alpha, Time.deltaTime);
    //     Debug.Log("Omega: " + omega.z);
    //     if (!stopAcceleration) {
    //         alpha = calculateAngularAcceleration(r, F, I);
    //     } else {
    //         alpha = new Vector3();
    //     }
    //     if (alphaBool) {
    //         alpha = new Vector3();
    //     }
    //     previousOmega = omega;
    //     //this.transform.Rotate(omega * Mathf.Rad2Deg * Time.deltaTime);
    //     this.transform.RotateAround(car.COM, Vector3.forward, (omega.z * Mathf.Rad2Deg * Time.deltaTime));
    //     if (getTimer() >= 1.96f) stopAcceleration = true;
    // }
    /// <summary>
    /// Move the car
    /// </summary>
    /// <param name="car">Car</param>
    public void moveCarObject(Car _car) {
        if (getTimer() >= 7.96f) stopped = true;
        if (!stopAcceleration || !(getTimer() >= 2f)) {
            acceleration = calculateObjectAcceleration(F, _car.m);
            velocity = calculateObjectVelocity(acceleration, getTimer());
        } else {
            acceleration = new Vector3();
        }
        _car.transform.position += (velocity * Time.deltaTime);
    }

    /// <summary>
    /// Sink the boat based on mass.
    /// </summary>
    void sinkBoat() {
        float pf = 1000f;
        depth = mass / (this.transform.lossyScale.x * this.transform.lossyScale.z * pf);
        this.transform.position = new Vector3(this.transform.position.x, (-depth), this.transform.position.z);
        calculateDragCoefficient();
    }

    /// <summary>
    /// Move the boat in the water.
    /// </summary>
    /// <param name="dt">Delta time</param>
    void moveBoat(float dt) {
        float tct = (thrust / dragCoefficient) * dt;
        float tcvc = (thrust - (dragCoefficient * velocity.x)) / dragCoefficient;
        float mc = mass / dragCoefficient;
        float ectm = Mathf.Exp(((-1f * dragCoefficient) * dt) / mass) - 1f;
        distance = distance + tct + (tcvc * mc * ectm);
        this.transform.position = new Vector3(distance, 0f, 0f);
        acceleration = new Vector3(((thrust - dragCoefficient * velocity.x) / mass), 0f, 0f);
        float moveX = ((thrust - (Mathf.Exp(((-1f * dragCoefficient) * dt) / mass)) * (thrust - dragCoefficient * velocity.x))) * (1f / dragCoefficient);
        velocity = new Vector3(moveX, 0f, 0f);
        updateTimer(dt);
    }

    /// <summary>
    /// Calculate the drag on the boat.
    /// </summary>
    void calculateDragCoefficient() {
            if (mass == 10000000f) dragCoefficient = (10e6f / 4f);
            else if (mass == 20000000f) dragCoefficient = (10e6f / 2f);
            else if (mass == 30000000f) dragCoefficient = ((10e6f / 4f) + (10e6f / 2f));
            else if (mass == 40000000f) dragCoefficient = 10e6f;
    }

	/// <summary>
	/// Move projectile in a 3D space.
	/// </summary>
	/// <param name="_t">Total time</param>
	/// <param name="_dt">Delta time</param>
	void moveProjectile3D(float _t, float _dt) {
		if (!stopped) {
            Debug.Log("Running");
			Vector3 radAlpha = Mathf.Deg2Rad * alpha,
					radGamma = Mathf.Deg2Rad * gamma;
			Vector3 vf = new Vector3(
				(velocity.x * Mathf.Sin(radAlpha.x) * Mathf.Cos(radGamma.z)),
				(velocity.y * Mathf.Cos(radAlpha.x) + (gravity.y * _t)),
				(velocity.z * Mathf.Sin(radAlpha.x) * Mathf.Sin(radGamma.z) * -1)
			) * _dt;
			this.transform.Translate(vf);
		}
	}

	/// <summary>
	/// Update all text found in region Text Variables. All are public.
	/// </summary>
    void updateText() {
        timeText.text = getTimer() + " s";
    }
}