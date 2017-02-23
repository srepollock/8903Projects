using UnityEngine;

public class Arrow : MonoBehaviour {
    public Transform pos;
    void Start() {

    }
    void Update() {
        this.transform.position = pos.position;
    }
}