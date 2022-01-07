using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPropagater : MonoBehaviour
{
    public void propagateQualityChoice(string quality)
    {
        if (quality == "Thinned")
        {
            Assets.Scripts.Model.GlobalBar.CurrentQuality = Assets.Scripts.Model.AlcoholQualityes.Low;
        }
        else if (quality == "Diluted")
        {
            Assets.Scripts.Model.GlobalBar.CurrentQuality = Assets.Scripts.Model.AlcoholQualityes.Medium;
        }
        else if (quality == "Original")
        {
            Assets.Scripts.Model.GlobalBar.CurrentQuality = Assets.Scripts.Model.AlcoholQualityes.High;
        }
        // Debug.Log(Assets.Scripts.Model.GlobalBar.CurrentQuality);
    }

    public void propagatePricesChoice(string prices)
    {
        if (prices == "Low")
        {
            Assets.Scripts.Model.GlobalBar.CurrentAlcoholPrices = Assets.Scripts.Model.AlcoholPrices.Low;
        }
        else if (prices == "Norm")
        {
            Assets.Scripts.Model.GlobalBar.CurrentAlcoholPrices = Assets.Scripts.Model.AlcoholPrices.Medium;
        }
        else if (prices == "High")
        {
            Assets.Scripts.Model.GlobalBar.CurrentAlcoholPrices = Assets.Scripts.Model.AlcoholPrices.High;
        }
        // Debug.Log(Assets.Scripts.Model.GlobalBar.CurrentAlcoholPrices);
    }

    public void propagateSourceChoice(string source)
    {
        if (source == "Detroit")
        {
            Assets.Scripts.Model.GlobalBar.CurrentSource = Assets.Scripts.Model.GlobalBar.AlcoholSources.Detroit;
        }
        else if (source == "Moonshine")
        {
            Assets.Scripts.Model.GlobalBar.CurrentSource = Assets.Scripts.Model.GlobalBar.AlcoholSources.Moonshine;
        }
        else if (source == "Mafia")
        {
            Assets.Scripts.Model.GlobalBar.CurrentSource = Assets.Scripts.Model.GlobalBar.AlcoholSources.Mafia;
        }
        // Debug.Log(Assets.Scripts.Model.GlobalBar.CurrentSource);
    }

}
