using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Physics : MonoBehaviour {
#region Public Variable per Project
    public Transform Lpoint, Rpoint, Tpoint;
#endregion
#region Public Physics Variabls
    /// <summary>
    /// Velocity of the object.
    /// </summary>
    public Vector3 velocity;
    /// <summary>
    /// Initial Velocity of the object. Set in <see cref="Start()"/>.
    /// </summary>
    public Vector3 velocityInitial;
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
    public float mass = 1; // Default 1
    /// <summary>
    /// Radius of the object to be rotated (basically width)
    /// </summary>
    public float radius;
    /// <summary>
    /// Force Vector of the object
    /// </summary>
    public Vector3 force;
    /// <summary>
    /// Inertia of the object.
    /// </summary>
    public float inertia;
#endregion
#region Private Physics Variables
    /// <summary>
    /// Current total time of object based on Timer.deltaTime additions
    /// </summary>
    float timer = 0f;
    /// <summary>
    /// Center of Mass for the object, grabbing all subsequent objects masses
    /// </summary>
    Vector3 COM = Vector3.zero;
    /// <summary>
    /// Thrust to move the object (in water). Should remain constant for test 
    /// purposes
    /// </summary>
    float thrust;
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
	bool windanddrag = false;
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
    Vector3 omegao = Vector3.zero;
    /// <summary>
    /// Angular Velocity
    /// </summary>
    Vector3 omega;
    /// <summary>
    /// Previous omega. Used for updating.
    /// </summary>
    Vector3 previousOmega = Vector3.zero;
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
    /// Drag coefficient. Used in boat project
    /// </summary>
    float dragCoefficient;
    /// <summary>
    /// Friction Coefficient. Used in project 12
    /// </summary>
    float frictionCoefficient = 0;
    /// <summary>
    /// Wind for kinematics
    /// </summary>
    Vector3 wind; // 0f, 0f, -10f
    /// <summary>
    /// Wind coefficient
    /// </summary>
    float windCoefficient; // 0.1N/(m/s)
    /// <summary>
    /// Tau of the object. Mass/DragCoefficient
    /// </summary>
    float tau;
    /// <summary>
    /// Boolean for checking only the first collision.
    /// </summary>
    bool haveCollided = false;
    /// <summary>
    /// Used in Project 9: Collision.
    /// </summary>
    float J;
    /// <summary>
    /// Collison count used in Project 9. Should be only 1.
    /// </summary>
    int collisionCount = 0;
    /// <summary>
    /// Normal vector of the collision. Used in Project 10
    /// </summary>
    Vector3 normalVector;
    /// <summary>
    /// Final L 1
    /// </summary>
    Vector3 Lf1;
    /// <summary>
    /// Final L 2
    /// </summary>
    Vector3 Lf2;
    /// <summary>
    /// Final iwf 1
    /// </summary>
    float iwf1;
    /// <summary>
    /// Final iwf 2
    /// </summary>
    float iwf2;
    /// <summary>
    /// Coefficient of Resitution (e).
    /// 
    /// Ranges from 0 <= e <= 1 (steps of 0.1)
    /// </summary>
    float coefficientOfRestitution;
#endregion
#region UI Text Variables
	// End all with `...Text` for simplicity
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
    /// Set the stopped boolean for running.
    /// </summary>
    /// <param name="_stopped">True/False</param>
    public void setStopped(bool _stopped) {
        stopped = _stopped;
    }
    /// <summary>
    /// Return if the object is stopped or not.
    /// </summary>
    /// <returns>Running/Stopped</returns>
    public bool getStopped() {
        return stopped;
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
    /// <summary>
    /// Set the drag coefficient for the object.
    /// </summary>
    /// <param name="_d">Drag to set to</param>
    public void setDragCoefficient(float _d) {
        dragCoefficient = _d;
    }
    /// <summary>
    /// Get the drag coefficient.
    /// </summary>
    /// <returns>Drag coefficient of the object.</returns>
    public float getDragCoefficient() {
        return dragCoefficient;
    }
    /// <summary>
    /// Set the wind coefficient.
    /// </summary>
    /// <param name="_w">Wind to set to</param>
    public void setWindCoefficient(float _w) {
        windCoefficient = _w;
    }
    /// <summary>
    /// Get the wind coefficient.
    /// </summary>
    /// <returns>Wind coefficient of the object.</returns>
    public float getWindCoefficient() {
        return windCoefficient;
    }
    /// <summary>
    /// Set the direciton of the wind. 
    /// 
    /// Default for us (0, 0, -10)
    /// </summary>
    /// <param name="_w">Direction to set the wind to.</param>
    public void setWindDirection(Vector3 _w) {
        wind = _w;
    }
    /// <summary>
    /// Get the direciton of the wind.
    /// </summary>
    /// <returns>Vector3 wind</returns>
    public Vector3 getWindDirection() {
        return wind;
    }
#endregion
    void Start() {
        
    }
    void Update() {
        // Input Functions
        // Force changer buttons: up/down arrows
        // Right arrow toggles R force 
        // Left arrow toggles L force
        // Movement function called here
        if (!stopped) {
            
        }

        centerOfMassLoop();

        updateText();
    }
    void FixedUpdate() {
        if (timer >= 12f) stopped = true;
        // Movement function called here(?)


		if (!stopped) updateTimer(Time.deltaTime);
    }

    /// <summary>
    /// Get all child components as a Physics array.
    /// </summary>
    /// <returns>Physics array of child components.</returns>
    Physics[] getChildPhysicsComponents() {
        ArrayList childAL = new ArrayList();
        foreach (Transform child in transform) {
            childAL.Add(child.GetComponent<Physics>());
        }
        Physics[] objsOut = (Physics[]) childAL.ToArray(typeof(Physics));
        return objsOut;
    }

    /// <summary>
    /// Calculates total mass of the object, taking a list of all child objects.
    /// </summary>
    /// <returns></returns>
    float calculateTotalMass(Physics[] gos) {
        float totalMass = 0;
        for (int i = 0; i < gos.Length; i++) {
            totalMass += gos[i].mass;
        }
        return totalMass;
    }

    Vector3 calculateCenterOfMass(Physics[] gos) {
        float xCOM = 0, yCOM = 0, zCOM = 0;
        xCOM = calculateXCOM(gos);
        yCOM = calculateYCOM(gos);
        zCOM = calculateZCOM(gos);
        return new Vector3(xCOM, yCOM, zCOM); // z should not be changed
    }

#region COM Components
    /// <summary>
    /// Calculates the X component of the COM position.
    /// </summary>
    /// <param name="gos">Physics Game Objects</param>
    /// <returns>X component of the COM positon</returns>
    float calculateXCOM(Physics[] gos) {
        float topTotal = 0, bottomTotal = 0;
        for (int i = 0; i < gos.Length; i++) {
            topTotal += gos[i].mass * gos[i].transform.position.x;
            bottomTotal += gos[i].mass;
        }
        return topTotal / bottomTotal;
    }

    /// <summary>
    /// Calculates the Y component of the COM position.
    /// </summary>
    /// <param name="gos">Physics Game Objects</param>
    /// <returns>Y component of the COM positon</returns>
    float calculateYCOM(Physics[] gos) {
        float topTotal = 0, bottomTotal = 0;
        for (int i = 0; i < gos.Length; i++) {
            topTotal += gos[i].mass * gos[i].transform.position.y;
            bottomTotal += gos[i].mass;
        }
        return topTotal / bottomTotal;
    }

    /// <summary>
    /// Calculates the Z component of the COM position.
    /// </summary>
    /// <param name="gos">Physics Game Objects</param>
    /// <returns>Z component of the COM positon</returns>
    float calculateZCOM(Physics[] gos) {
        float topTotal = 0, bottomTotal = 0;
        for (int i = 0; i < gos.Length; i++) {
            topTotal += gos[i].mass * gos[i].transform.position.z;
            bottomTotal += gos[i].mass;
        }
        return topTotal / bottomTotal;
    }

    /// <summary>
    /// Distance to the COM for the game object.
    /// </summary>
    /// <param name="go">Game object to find the Distance</param>
    /// <returns></returns>
    float distanceToCOM(Physics go) {
        return Mathf.Sqrt(
            Mathf.Pow(go.transform.position.x - this.COM.x, 2f) +
            Mathf.Pow(go.transform.position.y - this.COM.y, 2f)
        );
    }

    /// <summary>
    /// Total inertia of the parent object. Should be called on the upper most object.
    /// </summary>
    /// <param name="gos">Physics Game Objects</param>
    /// <returns>Total inertia</returns>
    float totalInertia(Physics[] gos) {
        float ti = 0;
        for (int i = 0; i < gos.Length; i++) {
            ti += gos[i].inertia + (gos[i].mass * Mathf.Pow(distanceToCOM(gos[i]), 2f));
        }
        return ti;
    }

    /// <summary>
    /// Loop for center of mass variables.
    /// </summary>
    void centerOfMassLoop() {
        Physics[] physicsObjects = getChildPhysicsComponents();
        calculateCenterOfMass(physicsObjects);
        this.inertia = totalInertia(physicsObjects);
    }
#endregion

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
	Vector3 calculateAngularVelocity(Vector3 _omega, Vector3 _alpha, float _t){
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
	Vector3 calculateRotationVelocity(Vector3 _v, Vector3 _omega, Vector3 _R){
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
	Vector3 calculateRotationAcceleration(Vector3 _acg, Vector3 _omega, Vector3 _alpha, Vector3 _R){
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
	float calculateRotationAngle(float _thetao, Vector3 _omega, Vector3 _alpha, float _t){
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
    /// Resets the position of the object.
    ///
    /// Different for each project.
    /// </summary>
    void resetPosition(CollisionObject col) {
        this.transform.position = new Vector3(-380.0f, 30.0f, 0.0f);
        col.transform.position = new Vector3(80.0f, 0.0f, 0.0f);
        this.velocity = this.velocityInitial;
        col.velocity = col.velocityInitial;
        this.J = 0.0f;
        collisionCount = 0;
        haveCollided = false;
        this.resetTimer();
    }

    /// <summary>
    /// First collision response of two objects.
    /// </summary>
    /// <param name="col">Collision object</param>
    void collisionResponse(CollisionObject col) {
        Vector3 Jj = -1f * (this.velocityInitial - col.velocityInitial) * (coefficientOfRestitution + 1) * ((this.mass * col.mass) / (this.mass + col.mass));
        Vector3 uf = new Vector3(((Jj.x/this.mass) + this.velocity.x), ((Jj.y/this.mass) + this.velocity.y), ((Jj.z/this.mass) + this.velocity.z));
        Vector3 vf = new Vector3(((-Jj.x/col.mass) + col.velocity.x), ((Jj.y/col.mass) + col.velocity.y), ((Jj.z/col.mass) + col.velocity.z));
        if (haveCollided) {
            haveCollided = false;
            collisionCount++;
            if (this.name == "Object1") {
                this.velocity = uf;
            }
            if (col.name == "Object2") {
                col.velocity = vf;
            }
        }
    }

    /// <summary>
    /// First collision response of two objects in 2D.
    /// </summary>
    /// <param name="col">Collision object</param>
    void collisionResponse2D(CollisionObject col) {
        normalVector.x = Mathf.Sqrt(Mathf.Pow(40f, 2f) - Mathf.Pow(-20f, 2f));
        normalVector.y = -20f;
        normalVector.z = 0f;
        Vector3 normalHat = normalVector.normalized;
        float uin = Vector3.Dot(this.velocityInitial, normalHat);
        float vin = Vector3.Dot(col.velocityInitial, normalHat);
        float vrn = (uin - vin);
        Vector3 vrnn = (uin - vin) * normalHat;
        Vector3 tVector = Vector3.Cross(Vector3.Cross(normalHat, this.velocityInitial), normalHat);
        Vector3 tHat = tVector.normalized;
        float uit = Vector3.Dot(this.velocityInitial, tHat);
        Vector3 uitt = new Vector3(uit * tHat.x, uit * tHat.y, 0f);
        float vit = Vector3.Dot(col.velocityInitial, tHat);
        Vector3 vitt = new Vector3(vit * tHat.x, vit * tHat.y, 0f);
        J = -vrn * (coefficientOfRestitution + 1) * ((this.mass * col.mass) / (this.mass + col.mass));
        Vector3 Jn = -vrnn * (coefficientOfRestitution + 1) * ((this.mass * col.mass) / (this.mass + col.mass));
        Vector3 ufnn = new Vector3((Jn.x / this.mass) + uin * normalHat.x, (Jn.y / this.mass) + uin * normalHat.y, 0.0f);
        Vector3 vfnn = new Vector3((-Jn.x / col.mass) + vin * normalHat.x, (-Jn.y / col.mass) + vin * normalHat.y, 0.0f);
        Vector3 uf = new Vector3(ufnn.x + uitt.x, ufnn.y + uitt.y, 0.0f);
        Vector3 vf = new Vector3(vfnn.x + vitt.x, vfnn.y + vitt.y, 0.0f);
        if (haveCollided) {
            haveCollided = false;
            collisionCount++;
            if (this.name == "Object1") {
                this.velocity = uf;
            }
            if (col.name == "Object2") {
                col.velocity = vf;
            }
        }
    }

    /// <summary>
    /// First collision response of two objects in 2D that will rotate.
    /// </summary>
    /// <param name="col">Collision object</param>
    void collisionResponseRotate(CollisionObject col) {
        // Move Back OBJ1
        float rad1 = this.transform.localScale.x / 2f;
        float obj1XPosDiff = Mathf.Sqrt(Mathf.Pow(2f * rad1, 2f) - Mathf.Pow(col.transform.position.y - this.transform.position.y, 2f));
        this.transform.position = new Vector3(col.transform.position.x - obj1XPosDiff, this.transform.position.y, this.transform.position.z);
        // Get the position of the collision
        Vector3 p1 = this.transform.position, 
                p2 = col.transform.position;
        Vector3 P = new Vector3((p2.x - ((p2.x - p1.x) / 2f)), (p1.y + (p2.y - p1.y) / 2f), 0f);
        // Respond using nHat = iHat (right)
        normalVector = this.transform.position - col.transform.position;
        Vector3 normalHat = normalVector.normalized;

        Vector3 tVector = Vector3.Cross(Vector3.Cross(normalHat, this.velocityInitial), normalHat);
        Vector3 tHat = tVector.normalized;
        // Vr = (uix - vix)
        Vector3 vr = this.velocityInitial - col.velocityInitial;
        // Get center of object to point of collision
        Vector3 r1 = P - this.transform.position;
        r1.z = 0f;
        Vector3 r2 = P - col.transform.position;
        r2.z = 0f;
        // Set the moment of Inertia for each
        this.I = (2f/5f) * this.mass * (Mathf.Pow(this.transform.lossyScale.x / 2f, 2));
        col.I = (2f/5f) * col.mass * (Mathf.Pow(col.transform.lossyScale.x / 2f, 2));
        // Caculate bottom bracket for J calculation
        float bracket1 = Vector3.Dot(normalHat, Vector3.Cross((Vector3.Cross(r1, normalHat) / this.I), r1));
        float bracket2 = Vector3.Dot(normalHat, Vector3.Cross((Vector3.Cross(r2, normalHat) / col.I), r2));
        float bottomBracket = (1f / this.mass) + (1f / col.mass) + bracket1 + bracket2; 
        bottomBracket = 1f / bottomBracket;
        J = -Vector3.Dot(vr, normalHat) * (coefficientOfRestitution + 1) * bottomBracket;
        Vector3 Jn = J * normalHat;
        // Calculate Final Velocites
        Vector3 ufxn = this.velocityInitial + (J / this.mass) * (normalHat + frictionCoefficient * tHat);
        Vector3 vfxn = col.velocityInitial + (-J / col.mass) * (normalHat - frictionCoefficient * tHat);
        // Calculate Final Rotations
        omega = omegao + (J / this.I) * (Vector3.Cross(r1, (normalHat + frictionCoefficient * tHat)));
        col.omega = col.omegao + (J / col.I) * (Vector3.Cross(r2, (normalHat - frictionCoefficient * tHat)));
        //Lf
        Lf1 = Vector3.Cross(r1, (this.velocity * this.mass));
        Lf2 = Vector3.Cross(r2, (col.velocity * col.mass));
        iwf1 = this.I * this.omega.z;
        iwf2 = col.I * col.omega.z;
        if (haveCollided) {
            haveCollided = false;
            collisionCount++;
            this.velocity = ufxn;
            col.velocity = vfxn;
            stopped = true;
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.name == "Object2" && collider.GetComponent<CollisionObject>() != null) {
            if (collisionCount <= 0) haveCollided = true; // Was hitting twice
            if (haveCollided) {
                collisionResponseRotate(collider.GetComponent<CollisionObject>());
                this.transform.Translate(new Vector3(-40, 0, 0));
                collider.transform.Translate(new Vector3(40, 0, 0));
            }
        }
    }
#region Not Working. //TODO Fix Wind Drag
	/// <summary>
	/// Move projectile in a 3D space.
	/// </summary>
	/// <param name="_t">Total time</param>
	/// <param name="_dt">Delta time</param>
	void moveProjectile3D(float _t, float _dt) {
		if (!stopped) {
            if (windanddrag) {
                tau = calculateTau(mass, dragCoefficient);
                Vector3 radAlpha = Mathf.Deg2Rad * alpha,
                        radGamma = Mathf.Deg2Rad * gamma;
                float   cwgdC = windOverDragCos(windCoefficient, wind, radGamma, dragCoefficient),
                        cwgdS = windOverDragSin(windCoefficient, wind, radGamma, dragCoefficient);
                float etT = Mathf.Exp(-_t/tau);
                // Vector3 vf = new Vector3(
                //     (0 + (velocity.x * tau * etT + (cwgdC * tau * etT) - (cwgdC * _t))),
                //     (0 + (velocity.y * tau * etT + (gravity.y * Mathf.Pow(tau, 2f) * etT) - (gravity.y * tau * _t))),
                //     (0 + (velocity.z * tau * etT + (cwgdS * tau * etT) - (cwgdS * _t)))
                // ) * _dt;
                Vector3 vf = new Vector3(
                    (velocity.x * Mathf.Sin(radAlpha.x) * Mathf.Cos(radGamma.z)),
                    (velocity.y * Mathf.Cos(radAlpha.x)),
                    (velocity.z * Mathf.Sin(radAlpha.x) * Mathf.Sin(radGamma.z) * -1)
                );
                vf = new Vector3(
                    ((etT * vf.x) + ((etT - 1) * cwgdC)),
                    ((etT * vf.y) + ((etT - 1) * -gravity.y * tau)),
                    ((etT * vf.z) + ((etT - 1) * cwgdS))
                ) * _dt;
                this.transform.Translate(vf);
            }
            else {
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
	}
    /// <summary>
    /// Caculate Tau of the object.
    /// </summary>
    /// <param name="_m">Mass of the object</param>
    /// <param name="_dC">Drag Coefficient of the object.</param>
    /// <returns>Tau</returns>
    float calculateTau(float _m, float _dC) {
        return _m/_dC;
    }
    /// <summary>
    /// Returns wind coefficient over drag coefficient. Cos function
    /// </summary>
    /// <param name="_wC">Wind Coefficient</param>
    /// <param name="wind">Wind strength (stored in wind.z)</param>
    /// <param name="_g">Gamma</param>
    /// <param name="_dC">Drag Coefficient</param>
    /// <returns>Results</returns>
    float windOverDragCos(float _wC, Vector3 wind, Vector3 _g, float _dC) {
        return (_wC * wind.z * Mathf.Cos(_g.z)) / _dC;
    }
    /// <summary>
    /// Returns wind coefficient over drag coefficient. Sin function
    /// </summary>
    /// <param name="_wC">Wind Coefficient</param>
    /// <param name="wind">Wind strength (stored in wind.z)</param>
    /// <param name="_g">Gamma</param>
    /// <param name="_dC">Drag Coefficient</param>
    /// <returns>Results</returns>
    float windOverDragSin(float _wC, Vector3 wind, Vector3 _g, float _dC) {
        return (_wC * wind.z * Mathf.Sin(_g.z)) / _dC;
    }
#endregion
	/// <summary>
	/// Update all text found in region Text Variables. All are public.
	/// </summary>
    void updateText() {

    }
}