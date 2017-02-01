using UnityEngine;
using UnityEngine.UI;

public class TargetPosition : MonoBehaviour {
	public Text targetx, targety, targetz;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		targetx.text = this.transform.position.x.ToString();
		targety.text = this.transform.position.y.ToString();
	}
}
