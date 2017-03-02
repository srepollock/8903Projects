using UnityEngine;
using UnityEngine.UI;

public class Physics : MonoBehaviour {
    // Rotation and Movement
    /// <summary>
    /// Velocity of the object.
    /// </summary>
    public Vector3 velocity = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Acceleration of the object.
    /// </summary>
    public Vector3 acceleration = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Mass of the object
    /// </summary>
    public float mass = 10e6f;
    /// <summary>
    /// Radius of the object to be rotated (basically width)
    /// </summary>
    public float radius;
    /// <summary>
    /// Force Vector of the object
    /// </summary>
    public Vector3 force = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Thrust to move the object (in water). Should remain constant for test 
    /// purposes
    /// </summary>
    public float thrust = 10e7f;
    public Text massText,
                depthText,
                dragText,
                tcText,
                positionText,
                velocityText,
                accelerationText,
                timeText,
                tauText;
    /// <summary>
    /// Start or stop game movement
    /// </summary>
    bool    stopped = true,
            stopAcceleration = false,
            alphaBool = false;
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
    Vector3 previousOmega = new Vector3();
    /// <summary>
    /// Initial Angular Acceleration
    /// </summary>
    Vector3 alphao = new Vector3(0f, 0f, 0f);
    /// <summary>
    /// Angular Acceleration
    /// </summary>
    Vector3 alpha;
    /// <summary>
    /// Initial Angle of Rotation of the object
    /// </summary>
    float thetao = 0f;
    /// <summary>
    /// Theta for the rotation
    /// </summary>
    float theta;
    /// <summary>
    /// Drag coefficient. Used in boat project
    /// </summary>
    float dragCoefficient = 0f;
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

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            stopped = !stopped;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            addMass(10e6f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            subMass(10e6f);
        }
        // Add weight to boat
        sinkBoat();
        updateText();
    }

    void FixedUpdate() {
        if (timer >= 12f) stopped = true;
        if (!stopped) moveBoat(Time.deltaTime);
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

    /// <summary>
    /// Initialize the Rotation values
    /// </summary>
    public void initCarRotation(Arrow arrow, Car car) {
        resetTheta();
        resetOmega();
        r = arrowToCOM(arrow.transform.position, car.COM);
        I = car.getInertiaCar();
        alpha = (calculateAngularAcceleration(r, force, I));
        resetTimer();
    }

    /// <summary>
    /// Rotation loop of the object
    /// </summary>
    public void rotateCarLoop(Car car, Arrow arrow) {
        r = arrowToCOM(arrow.transform.position, car.COM);
        theta = calculateRotationAngle(thetao, previousOmega, alpha, timer);
        omega = calculateAngularVelocity(omega, alpha, Time.deltaTime);
        Debug.Log("Omega: " + omega.z);
        if (!stopAcceleration) {
            alpha = calculateAngularAcceleration(r, F, I);
        } else {
            alpha = new Vector3();
        }
        if (alphaBool) {
            alpha = new Vector3();
        }
        previousOmega = omega;
        //this.transform.Rotate(omega * Mathf.Rad2Deg * Time.deltaTime);
        this.transform.RotateAround(car.COM, Vector3.forward, (omega.z * Mathf.Rad2Deg * Time.deltaTime));
        if (getTimer() >= 1.96f) stopAcceleration = true;
    }
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

    void updateText() {
        massText.text = "Ship Mass: " + mass + "kg";
        depthText.text = "Depth: " + depth + "m";
        dragText.text = "Drag: " + dragCoefficient;
        tcText.text = "T/C: " + thrust/dragCoefficient + "m/s";
        positionText.text = "Position: " + distance + "m";
        velocityText.text = "Velocity: " + velocity.x + "m/s";
        accelerationText.text = "Acceleration: " + acceleration.x + "m/s^2";
        timeText.text = "Time: " + getTimer() + "s";
        tauText.text = "Tau: " + mass/dragCoefficient + "s";
    }
}