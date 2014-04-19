using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	private float shieldTimer;

	public float shieldDeathTime;
	public Color shieldColor;
	public Color shieldDeathColor;

	// Use this for initialization
	void Start () {
		shieldTimer = 0;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (shieldTimer > 0) 
		{
			shieldTimer -= Time.deltaTime;

			float ratio = shieldTimer / shieldDeathTime;
			Color newColor = Color.Lerp (shieldDeathColor, shieldColor, ratio);

			MeshRenderer rend = GetComponent<MeshRenderer> ();
			rend.material.color = newColor;

			if (shieldTimer <= 0) {
				gameObject.SetActive (false);

				CapsuleCollider cap = gameObject.transform.parent.GetComponent<CapsuleCollider> ();
				cap.enabled = true;
			}
		} 
		else 
		{
			MeshRenderer rend = GetComponent<MeshRenderer> ();
			if(rend.material.color != shieldColor)
			{
				rend.material.color = shieldColor;
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (shieldTimer > 0)	return;
		
		if(col.gameObject.tag == "asteroid")
		{
			shieldTimer = shieldDeathTime;
		}
	}
}
