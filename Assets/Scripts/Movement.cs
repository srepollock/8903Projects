using UnityEngine;

public class Movement : MonoBehaviour {
    // There is a better way to do this. Fix for the final
    public Car car;
    public Arrow arrow;
    // Rotation and Movement
    /// <summary>
    /// Velocity of the object.
    /// </summary>
    public float velocity = 100f;
    /// <summary>
    /// Acceleration of the object.
    /// </summary>
    public float acceleration = -10f;
    /// <summary>
    /// Radius of the object to be rotated (basically width)
    /// </summary>
    public float radius;
    /// <summary>
    /// Force X of the object
    /// </summary>
    public float forceX = 0f;
    /// <summary>
    /// Force Y of the object
    /// </summary>
    public float forceY = 0f;
    /// <summary>
    /// Force Z of the object
    /// </summary>
    public float forceZ = 0f;
    /// <summary>
    /// Start or stop game movement
    /// </summary>
    bool stopped = true;
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
    Vector3 omega = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Initial Angular Acceleration
    /// </summary>
    Vector3 alphao = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Angular Acceleration
    /// </summary>
    Vector3 alpha = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Initial Angle of Rotation of the object
    /// </summary>
    float thetao = 0f;
    /// <summary>
    /// Theta for the rotation
    /// </summary>
    float theta;
    /// <summary>
    /// Current total time of object based on Timer.deltaTime additions
    /// </summary>
    float timer = 0f;

    void Start() {

    }

    void Update() {

    }

    void FixedUpdate() {

    }

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

    // /// <summary>
    // /// Sets omega
    // /// </summary>
    // /// <param name="o">Value to set omega to</param>
    // public void setOmega(float o) {
    //     omega = o;
    // }

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

    // /// <summary>
    // /// Sets alpha.z
    // /// </summary>
    // /// <param name="z">Value to set alpha.z to</param>
    // public void setAlpha(float z) {
    //     alpha.z = z;
    // }

    // /// <summary>
    // /// Sets alpha
    // /// </summary>
    // /// <param name="a">Vector3 to set alpha to</param>
    // public void setAlpha(Vector3 a) {
    //     alpha = a;
    // }

    /// <summary>
    /// Get the alpha
    /// </summary>
    /// <returns>Alpha</returns>
    public Vector3 getAlpha() {
        return alpha;
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
    public void setTheta(float t) {
        theta = t;
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
        forceX = x;
    }

    /// <summary>
    /// Sets the forceY
    /// </summary>
    /// <param name="y">Force Y</param>
    public void setForceY(float y) {
        forceY = y;
    }

    /// <summary>
    /// Sets the forceZ
    /// </summary>
    /// <param name="z">Force Z</param>
    public void setForceZ(float z) {
        forceZ = z;
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

    // DEPRICATED
    /// <summary>
    /// Create Force Vector
    /// </summary>
    /// <param name="_fx">Force X for the vector</param>
    /// <param name="_fy">Force Y for the vector</param>
    /// <param name="_fz">Force Z for the vector</param>
    /// <returns>Force Vector</returns>
    Vector3 createForceVector(float _fx, float _fy, float _fz) {
        return new Vector3(_fx, _fy, _fz);
    }

    /// <summary>
	/// Calculate the updating Angular Velocity (omega)
	/// </summary>
	/// <param name="_omegao">Initial Omega</param>
	/// <param name="_alpha">Alpha</param>
	/// <param name="_t">Total time</param>
	/// <returns>Omega</returns>
	Vector3 calculateAngularVelocity(Vector3 _omegao, Vector3 _alpha, float _t)
	{
		return new Vector3(0f, 0f, (_omegao.z + (_alpha.z * _t)));
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
	float calculateRotationAngle(float _thetao, Vector3 _omegao, Vector3 _alpha, float _t)
	{
		return _thetao + (_omegao.z * _t) + (0.5f * _alpha.z * Mathf.Pow(_t, 2));
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

    /// <summary>
    /// Initialize the Rotation values
    /// </summary>
    public void initRotation(Arrow arrow, Car car) {
        resetTheta();
        resetOmega();
        setR(arrowToCOM(arrow.transform.position, car.COM));
        setForceVector(new Vector3(forceX, forceY, forceZ));
        I = car.getCOMCar();
        alpha = (calculateAngularAcceleration(r, F, I));
        resetTimer();
    }

    /// <summary>
    /// Rotation loop of the object
    /// </summary>
    public void rotateLoop(Car car) {
        setTheta(calculateRotationAngle(thetao, omegao, getAlpha(), getTimer()));
        omega = calculateAngularVelocity(omegao, getAlpha(), getTimer());
        alpha = calculateAngularAcceleration(r, F, I);
        updateTimer(Time.deltaTime);
        if (getTimer() >= 2f) {
            stopped = true;
        }
    }

    // DEPRICATED
    void updateText() {

    }
}