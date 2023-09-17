using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using UnityEngine.Events;
using System;
using Data;
using System.IO;
using System.Net.Http;
using UnityEngine.Networking;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

public class ShuntingYard : MonoBehaviour
{

    [Serializable]
    public class DepoStateEvent : UnityEvent<DepoState> { }

    public DepoStateEvent DataPointReceived;

    private SocketIOClient.SocketIO _socket;

    public string URL = "http://34.65.119.75:3000/";
    //https://depot-hackzurich.iltis.rocks
    //http://34.65.119.75:3000/

    System.Collections.Concurrent.ConcurrentQueue<DepoState> _depoStates = new System.Collections.Concurrent.ConcurrentQueue<DepoState>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initialize());
    }

    private void OnDestroy()
    {
        _socket.Dispose();
    }

    private IEnumerator Initialize()
    {
        //yield return ParsePositions();
        yield return ConnectSocketIO();
    }

    private IEnumerator ParsePositions()
    {
        var asset = Resources.Load<TextAsset>("track-osm");
        using (var reader = new StringReader(asset.text))
        {

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                var data = line.Split(';');
                if (data.Length < 2) continue;
                if (data[0].Equals("track_id")) continue;//header will be ignored
                var url = $"https://api.openstreetmap.org/api/0.6/way/{data[1]}.json";
                using (var wayRequest = UnityWebRequest.Get(url))
                {
                    yield return wayRequest.SendWebRequest();
                    if (wayRequest.result != UnityWebRequest.Result.Success)
                    {
                        throw new ArgumentException($"can't execute '{url}'");
                    }
                    var wayData = JsonConvert.DeserializeObject<OSMWayData>(wayRequest.downloadHandler.text);
                    //var wayData = JsonUtility.FromJson<OSMWayData>(wayRequest.downloadHandler.text);
                    var latitude = 0d;
                    var longitude = 0d;
                    foreach (var node in wayData.elements[0].nodes)
                    {
                        url = $"https://api.openstreetmap.org/api/0.6/node/{node}.json";
                        using (var nodeRequest = UnityWebRequest.Get(url))
                        {
                            yield return nodeRequest.SendWebRequest();
                            if (nodeRequest.result != UnityWebRequest.Result.Success)
                            {
                                throw new ArgumentException($"can't execute '{url}'");
                            }
                            var nodeData = JsonConvert.DeserializeObject<OSMNodeData>(nodeRequest.downloadHandler.text);
                            latitude += nodeData.elements[0].lat;
                            longitude += nodeData.elements[0].lon;
                        }
                    }
                    latitude /= wayData.elements[0].nodes.Count;
                    longitude /= wayData.elements[0].nodes.Count;
                    Debug.Log($"Lat:{latitude}; Long:{longitude}");
                }
            }
        }
    }

    private IEnumerator GetJsonData<T>(string url)
    {
        using (var wayRequest = UnityWebRequest.Get(url))
        {
            yield return wayRequest.SendWebRequest();
            if (wayRequest.result != UnityWebRequest.Result.Success)
            {
                throw new ArgumentException($"can't execute '{url}'");
            }
            yield return JsonUtility.FromJson<T>(wayRequest.downloadHandler.text);
        }
    }

    private IEnumerator ConnectSocketIO()
    {
        var query = new Dictionary<string, string>();
        query["startTime"] = "2023-07-11T01:47:25.263Z";
        _socket = new SocketIOClient.SocketIO(URL, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
            Query = query
        });

        //_socket.On("depotStateUpdate", response =>//realtime
        //_socket.On("depotStateUpdateFast", response =>//10x faster 
        _socket.On("depotStateUpdateVeryfast", response =>//100x faster
        //_socket.On("depotStateUpdateStep", response =>//every .5 sec
        {
            //Debug.Log(response);
            var val = response.GetValue<DepoState>();
            _depoStates.Enqueue(val);

        });
        var task = _socket.ConnectAsync();
        yield return new WaitUntil(() => task.IsCompleted);
    }

    private void Update()
    {
        while (_depoStates.TryDequeue(out var state))
        {
            DataPointReceived?.Invoke(state);
        }
    }

}
