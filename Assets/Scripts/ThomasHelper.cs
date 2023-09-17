using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ThomasHelper : MonoBehaviour
{
    [Serializable]
    public class UnityStringEvent : UnityEvent<string> { }

    public UnityStringEvent Answerreceived;

    public void AskServer(string msg)
    {
        StartCoroutine(SendRequest(msg));
        //StartCoroutine(AskServerAsync(msg));
    }

    private IEnumerator AskServerAsync(string msg)
    {
        var str = $"_json:{{'description':'{msg}'}}";
        var post = UnityWebRequest.Get("http://34.22.204.244:443/askHelp/" + msg);
        yield return post.SendWebRequest();
        var data = post.downloadHandler.data;
        var clip = WavUtility.ToAudioClip(data);

        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Answerreceived?.Invoke(msg);
        //post.result.
    }

    // Ihre OpenAI API-Zugangsdaten
    private string apiKey = Secrets.OPEN_AI_KEY;

    // Die URL für die GPT-3 API
    private string apiUrl = "https://api.openai.com/v1/chat/completions";

    // Methode zum Senden einer Anfrage an die GPT-3 API
    public IEnumerator SendRequest(string userMessage)
    {
        // Erstellen Sie die Anfrage-Nachricht
        var requestJson = "{\"model\": \"gpt-3.5-turbo\",\"messages\": [{\"role\": \"system\",\"content\": \"Du bekommst eine Anfrage nach einer Route von Punkt A zu Punkt B. Du sollst eine Antwort erstellen, die ich an den Nutzer geben kann die zeigt, dass du den Start und Endpunkt verstanden hast und den Nutzer nochmal um Bestätigung bittet. Achte bitte auch darauf, dass du beim Start und Enpunkt zwischen den Buchstaben und der Zahl ein Leerzeichen setzt.\"},{\"role\": \"user\",\"content\": \"" + userMessage + "\"}]}";

        // Erstellen Sie eine UnityWebRequest
        var request = UnityWebRequest.Post(apiUrl, requestJson, "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // Senden Sie die Anfrage
        yield return request.SendWebRequest();

        // Überprüfen Sie auf Fehler
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Fehler beim Senden der Anfrage: " + request.error);
        }
        else
        {
            // Die Antwort auslesen
            var responseJson = request.downloadHandler.text;
            var answer = JsonConvert.DeserializeObject<GPTAnswer>(responseJson);
            Debug.Log("Antwort von GPT-3: " + responseJson);
            yield return AskServerAsync(answer.choices[0].message.content);
            // Hier können Sie die Antwort weiterverarbeiten
        }


    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Choice
    {
        public int index { get; set; }
        public Message message { get; set; }
        public string finish_reason { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class GPTAnswer
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }



}
