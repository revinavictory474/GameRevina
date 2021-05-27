using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus managerStatus { get; private set; }
    public float cloudValue { get; private set; }
    private NetworkService _network;

    public void Startup(NetworkService networkService)
    {
        Debug.Log("Weather Manager start");

        _network = networkService;
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));
        managerStatus = ManagerStatus.Initializing;
    }

    public void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;

        Dictionary<string, object> clouds = (Dictionary<string, object>)dict["clouds"];
        cloudValue = (long)clouds["all"] / 100f;
        Debug.Log("Value: " + cloudValue);

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        managerStatus = ManagerStatus.Started;
    }
}
