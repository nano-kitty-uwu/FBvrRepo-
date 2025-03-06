using UnityEngine;

public class GunController : MonoBehaviour
{
	[SerializeField] int _id;
	[SerializeField] float triggerOn;
	[SerializeField] float triggerOff;
	bool triggered = false;
	float spectrumValue=1;

	private void FixedUpdate()
	{
		spectrumValue = Analizator._audioBandBuffer[_id];

		if(spectrumValue > triggerOn)
		{
			triggered = true;
		}
		if(spectrumValue < triggerOff)
		{
			triggered = false;
		}
		PerformTriggerage();
	}

	void PerformTriggerage()
	{ 
		if(spectrumValue>0) transform.localScale = new Vector3(0.1f,spectrumValue*2,0.1f);
		if(triggered )
		{
			Debug.Log("Band: "+_id.ToString()+" triggered with value: "+spectrumValue.ToString());
		}
		if (!triggered)
		{
			Debug.Log("Band: " + _id.ToString() + " untriggerd with value: " + spectrumValue.ToString());
		}
	}
}
