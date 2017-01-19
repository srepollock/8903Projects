using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
	// Parts
	public Body b;
	public Tank t;
	public Driver d;
	// Text
	public Text bxT, bzT, txT, tzT, dxT, dzT;
	public Text COMT, MIT, MT, T, V;

	public float m;
	public Position COM;
	public float BDistCOM;
	public float TDistCOM;
	public float DDistCOM;
	public float BICOM;
	public float TICOM;
	public float DICOM;
	public float tI;

	void Start() {
		setMassTotalCar();
	}

	void Update() {
		setCOMCar();
		DistCOM();
		InertiaCOM();
		totalInertia();
		UpdateText();
	}

	public void setMassTotalCar() {
		m = (b.m + t.m + d.m);
	}

	public void setCOMCar() {
		float 	xCOM = 0, 
				zCOM = 0;
		xCOM = totalxCOM();
		zCOM = totalzCOM();
		COM = new Position(xCOM, zCOM);
		Debug.Log("COM: " + xCOM + ", " + zCOM);
	}

	float totalxCOM() {
		return (
			(b.m * b.transform.position.x) +
			(t.m * t.transform.position.x) +
			(d.m * d.transform.position.x)
		) / (
			b.m +
			t.m +
			d.m
		);
	}

	float totalzCOM() {
		return (
			(b.m * b.transform.position.z) +
			(t.m * t.transform.position.z) +
			(d.m * d.transform.position.z)
		) / (
			b.m +
			t.m +
			d.m
		);
	}

	void DistCOM() {
		BDistCOM = Mathf.Sqrt(
				Mathf.Pow(b.transform.position.x - COM.getPos().First, 2) +
				Mathf.Pow(b.transform.position.z - COM.getPos().Second, 2)
		);
		TDistCOM = Mathf.Sqrt(
				Mathf.Pow(t.transform.position.x - COM.getPos().First, 2) +
				Mathf.Pow(t.transform.position.z - COM.getPos().Second, 2)
		);
		DDistCOM = Mathf.Sqrt(
				Mathf.Pow(d.transform.position.x - COM.getPos().First, 2) +
				Mathf.Pow(d.transform.position.z - COM.getPos().Second, 2)
		);
		Debug.Log("BDistCOM: " + BDistCOM);
		Debug.Log("TDistCOM: " + TDistCOM);
		Debug.Log("DDistCOM: " + DDistCOM);
	}

	void InertiaCOM() {
		Debug.Log("body i: " + b.i);
		BICOM = b.i + (b.m * Mathf.Pow(BDistCOM, 2));
		TICOM = t.i + (t.m * Mathf.Pow(TDistCOM, 2));
		DICOM = d.i + (d.m * Mathf.Pow(DDistCOM, 2));
		Debug.Log("BICOM: " + BICOM);
		Debug.Log("TICOM: " + TICOM);
		Debug.Log("DICOM: " + DICOM);
	}

	void totalInertia() {
		tI = BICOM + TICOM + DICOM;
		Debug.Log("Total Inertia: " + tI);
	}

	void UpdateText() {
		// x,z
		bxT.text = b.transform.position.x.ToString();
		bzT.text = b.transform.position.z.ToString();
		txT.text = t.transform.position.x.ToString();
		tzT.text = t.transform.position.z.ToString();
		dxT.text = d.transform.position.x.ToString();
		dzT.text = d.transform.position.z.ToString();

		COMT.text = COM.getPos().First + ", " + COM.getPos().Second;
		MIT.text = tI.ToString() + "kg m^2";
		MT.text = m.ToString() + "kg";
	}
}
