using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {
	// Parts
	public Body b;
	public Tank t;
	public Driver d;
	public Arrow a;
	// Text
	public Text bxT, bzT, txT, tzT, dxT, dzT;
	public Text COMT, MIT, MT;

	public float m;
	public Vector3 COM;
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
		momentOfInertia();
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
		COM = new Vector3(xCOM, zCOM, 0f);
	}

	public float getCOMCar() {
		return tI;
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
				Mathf.Pow(b.transform.position.x - COM.x, 2) +
				Mathf.Pow(b.transform.position.z - COM.y, 2)
		);
		TDistCOM = Mathf.Sqrt(
				Mathf.Pow(t.transform.position.x - COM.x, 2) +
				Mathf.Pow(t.transform.position.z - COM.y, 2)
		);
		DDistCOM = Mathf.Sqrt(
				Mathf.Pow(d.transform.position.x - COM.x, 2) +
				Mathf.Pow(d.transform.position.z - COM.y, 2)
		);
	}

	void InertiaCOM() {
		BICOM = b.i + (b.m * Mathf.Pow(BDistCOM, 2));
		TICOM = t.i + (t.m * Mathf.Pow(TDistCOM, 2));
		DICOM = d.i + (d.m * Mathf.Pow(DDistCOM, 2));
	}

	void momentOfInertia() {
		tI = BICOM + TICOM + DICOM;
	}

	// Update all Text fields
	// Called per update

	void UpdateText() {
		// x,z
		bxT.text = b.transform.position.x.ToString();
		bzT.text = b.transform.position.z.ToString();
		txT.text = t.transform.position.x.ToString();
		tzT.text = t.transform.position.z.ToString();
		dxT.text = d.transform.position.x.ToString();
		dzT.text = d.transform.position.z.ToString();

		COMT.text = COM.x + ", " + COM.y;
		MIT.text = tI.ToString() + "kg m^2";
		MT.text = m.ToString() + "kg";
	}
}
