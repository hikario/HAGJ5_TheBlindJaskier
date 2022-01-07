using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    private Slider mySlider;


    void Awake()
    {
        mySlider = gameObject.GetComponent<Slider>();
        mySlider.value = Assets.Scripts.Model.GlobalBar.Popularity;

        EventManager.RegisterEventListener("PopularityUpdate", UpdatePopularity);
    }

    void OnDestroy()
    {
        EventManager.DeregisterEventListener("PopularityUpdate", UpdatePopularity);
    }

    void UpdatePopularity()
    {
        mySlider.value = Assets.Scripts.Model.GlobalBar.Popularity;
    }

    
}
