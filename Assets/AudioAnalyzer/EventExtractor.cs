using System;
using UnityEngine;

public class EventExtractor : MonoBehaviour
{
	private float[] spectrumData;

    public static EventExtractor Instance { get ; private set; }
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	public class BandSettings
	{
		public int bandIndex;
		public float triggerOn;
		public float triggerOff;
	}
	public BandSettings[] bandSettings= new BandSettings[8];

	public event Action<int, float> OnTriggerOn;
	public event Action<int, float> onTriggerOff;

	private void FixedUpdate()
	{
		for(int i=0;  i<bandSettings.Length; i++)
		{
		}
	}
}
