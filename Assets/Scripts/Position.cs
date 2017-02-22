using UnityEngine;

public class Position : MonoBehaviour {
	public float x;
    public float y;
    public Position(float _x, float _y) {
        x = _x;
        y = _y;
    }

    public Pair<float, float> getPos() {return new Pair<float, float>(x, y); }
}
