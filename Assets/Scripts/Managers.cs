using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]
public class Managers : MonoBehaviour
{
    public static WeatherManager Weather { get; private set; }
    public static ImagesManager Images { get; private set; }
    private List<IGameManager> _startSequence;

    private void Awake()
    {
        Weather = GetComponent<WeatherManager>();
        Images = GetComponent<ImagesManager>();
        _startSequence = new List<IGameManager>();
        _startSequence.Add(Weather);
        _startSequence.Add(Images);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();

        foreach (IGameManager gameManager in _startSequence)
            gameManager.Startup(network);

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady<numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager gameManager in _startSequence)
                if (gameManager.managerStatus == ManagerStatus.Started)
                    numReady++;

            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);

            yield return null;
        }
        Debug.Log("All managers started up");
    }
}
