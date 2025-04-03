using System;
using UnityEngine;

public static class EventExtractor
{
	static int selectedIndex;

	public static event Action<int, float> OnAmplitudeReached;
	public static void AmplitudeReached(int index, float amp)
	{
		OnAmplitudeReached?.Invoke(index,amp);
	}
	public static event Action<int, float> OnAmplitudeDeficit;
	public static void AmplitudeDeficit(int index, float amp)
	{
		OnAmplitudeDeficit?.Invoke(index, amp);
	}
}
