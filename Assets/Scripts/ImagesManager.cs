using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ImagesManager : MonoBehaviour, IGameManager
{
    public ManagerStatus managerStatus { get; private set; }
    private NetworkService _network;
    private Texture2D _webImage;

    public void Startup(NetworkService networkService)
    {
        Debug.Log("Images manager starting");

        _network = networkService;

        managerStatus = ManagerStatus.Started;
    }

    public void GetWebImage(Action<Texture2D> callback)
    {
        if (_webImage == null)
            StartCoroutine(_network.DownloadImage((Texture2D image) =>
            {
                _webImage = image;
                callback(_webImage);
            }));
        else
            callback(_webImage);
    }
}
