using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{

    [SerializeField] private Material sky;
    [SerializeField] private Light sun;
    private float _fullIntensity;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }
    private void OnDestroy()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }
    void Start()
    {
        _fullIntensity = sun.intensity;
    }
    //void Update()
    //{
    //    SetOvercast(_cloudValue);
    //    _cloudValue += .005f;
    //}
    private void OnWeatherUpdated()
    {
        SetOvercast(Managers.Weather.cloudValue);
    }
    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }

}
