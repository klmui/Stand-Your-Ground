using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = FindObjectOfType<AudioManager>().GetMasterVolumeMultiplier();

        slider.onValueChanged.AddListener((v) => {
            FindObjectOfType<AudioManager>().UpdateVolumeMultiplier(v);
        });
    }
}
