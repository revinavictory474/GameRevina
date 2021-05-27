using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{ 
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Krasnoyarsk&appid=e0b9716b5bde2b76a9b84ff2e5d6744f";
    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.Send();

            if (request.isNetworkError)
                Debug.LogError("network problem: " + request.error);
            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
                Debug.LogError("respone error: " + request.responseCode);
            else
                callback(request.downloadHandler.text);
        }
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.Send();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}
