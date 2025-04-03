using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Analizator : MonoBehaviour
{
	AudioSource _audioSource;
	float[] triggerValues = new float[8];
	int selectedBand;
	float _smoothingFactor=0.06f;
		//Lower values(0.01-0.05) = slower adaptation, stable triggers
		//Higher values(0.1-0.3) = quicker response to volume changes
	float[] _samples = new float[512];
	float[] _freqBand = new float[8];
	float[] _bandBuffer = new float[8];
	float[] _bufferDecrease = new float[8];
	float[] _freqBandHighest = new float[8];
	float[] _audioBand = new float[8];

	public static float[] _audioBandBuffer = new float[8];


	// Start is called before the first frame update

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		GetSpectrumAudioSource();
		MakeFrequencyBands();
		BandBuffer();
		CreateAudioBands();
		RecalibrateTriggerValues();
		FireEvensts();
	}

	private void RecalibrateTriggerValues()
	{
		for(int i=0; i < 8;i++)triggerValues[i] = _freqBand[i] * _smoothingFactor + triggerValues[i] * (1f - _smoothingFactor);
	}

	private void FireEvensts()
	{
		if (_audioBandBuffer[selectedBand] > triggerValues[selectedBand])
		EventExtractor.AmplitudeReached(selectedBand, _audioBandBuffer[selectedBand]);
		if (_audioBandBuffer[selectedBand] < triggerValues[selectedBand])
			EventExtractor.AmplitudeDeficit(selectedBand, _audioBandBuffer[selectedBand]);
	}

	void CreateAudioBands()
	{
		for (int i = 0; i < 8; i++)
		{
			if (_freqBand[i] > _freqBandHighest[i])
			{
				_freqBandHighest[i] = _freqBand[i];
			}
			_audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
			_audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
		}
	}

	void GetSpectrumAudioSource()
	{
		_audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
	}


	void BandBuffer()
	{
		for (int g = 0; g < 8; g++)
		{
			if (_freqBand[g] > _bandBuffer[g])
			{
				_bandBuffer[g] = _freqBand[g];
				_bufferDecrease[g] = 0.005f;
			}
			if (_freqBand[g] < _bandBuffer[g])
			{
				_bandBuffer[g] -= _bufferDecrease[g];
				_bufferDecrease[g] *= 1.2f;
			}
		}
	}

	void MakeFrequencyBands()
	{
		int count = 0;
		for (int i = 0; i < 8; i++)
		{
			float average = 0;
			int sampleCount = (int)Mathf.Pow(2, i) * 2;
			if (i == 7)
			{
				sampleCount += 2;
			}
			for (int j = 0; j < sampleCount; j++)
			{
				average += _samples[count] * (count + 1);
				count++;
			}
			average /= count;
			_freqBand[i] = average * 10;
		}
	}
}