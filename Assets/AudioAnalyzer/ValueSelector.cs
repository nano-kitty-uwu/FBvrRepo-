using UnityEngine;
using UnityEngine.UI;

public class ValueSelector : MonoBehaviour
{
    public Slider slider;
    public void OnSliderChanged()
    {
        Analizator.selectedBand=(int)slider.value;
    }
}
