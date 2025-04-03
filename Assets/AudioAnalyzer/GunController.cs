using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class GunController : MonoBehaviour
{
	public GameObject laserObject;
	
	private void OnEnable()
	{
		EventExtractor.OnAmplitudeReached += EventExtractor_OnAmplitudeReached;
		EventExtractor.OnAmplitudeDeficit += EventExtractor_OnAmplitudeDeficit;
	}

	private void EventExtractor_OnAmplitudeDeficit(int arg1, float arg2)
	{
		if(laserObject!= null) laserObject.SetActive(false);
	}

	private void EventExtractor_OnAmplitudeReached(int arg1, float arg2)
	{
		if (laserObject != null)laserObject.SetActive(true);
	}
}
