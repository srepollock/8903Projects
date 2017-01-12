using UnityEngine;

public class Position : MonoBehaviour {
	public float x;
    public float z;
    public Position(float _x, float _z) {
        x = _x;
        z = _z;
    }

    public Pair<float, float> getPos() {return new Pair<float, float>(x, z); }
}
