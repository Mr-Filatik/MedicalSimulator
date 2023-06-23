using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }

    public Action StartWaiting = null;
    public Action CanselWaiting = null;

    public Action Logout = null;
    public Action Error = null;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator SendingRequest<Obj>(string address, string data, RequestType requestType, Action<Obj> finalAction, bool isBlockApplication = false)
    {
        bool isDone = false;
        if (isBlockApplication) StartWaiting?.Invoke();
        UnityWebRequest webRequest = null;
        switch (requestType)
        {
            case RequestType.GET:
                webRequest = UnityWebRequest.Get(address);
                webRequest.SetRequestHeader("Access-Key", "Access-Key");
                break;
            case RequestType.POST:
                webRequest = new UnityWebRequest(address, "POST");
                webRequest.SetRequestHeader("Access-Key", "Access-Key");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
                webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.PUT:
                webRequest = new UnityWebRequest(address, "PUT");
                webRequest.SetRequestHeader("Access-Key", "Access-Key");
                byte[] bodyRaw1 = Encoding.UTF8.GetBytes(data);
                webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw1);
                webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.DELETE:
                webRequest = UnityWebRequest.Delete(address);
                webRequest.SetRequestHeader("Access-Key", "Access-Key");
                break;
            default:
                break;
        }
        if (webRequest != null)
        {
            try
            {
                yield return webRequest.SendWebRequest();
                Debug.Log($"CODE = {webRequest.responseCode}");
                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    ResponseCodeAnaliser(webRequest);
                }
                else
                {
                    if (webRequest.downloadHandler != null)
                    {
                        Debug.Log($"RESPONSE = {webRequest.downloadHandler.text}");
                        finalAction?.Invoke(JsonUtility.FromJson<Obj>(webRequest.downloadHandler.text));
                        isDone = true;
                    }
                }
            }
            finally
            {
                webRequest.Dispose();
            }
        }
        if (!isDone) Error?.Invoke();
        if (isBlockApplication) CanselWaiting?.Invoke();
    }

    public void ResponseCodeAnaliser(UnityWebRequest webRequest)
    {
        ErrorResponse error = JsonUtility.FromJson<ErrorResponse>(webRequest.downloadHandler.text);
        switch (webRequest.responseCode)
        {
            case 200:
                Debug.Log($"Запрос отработал");
                break;
            case 401:
                Debug.Log($"Необходима авторизация");
                Logout?.Invoke();
                break;
            case 403:
                Debug.Log($"Недостаточно прав");
                break;
            case 404:
                Debug.Log($"Ресурс не найден");
                break;
            default:
                Debug.Log($"Неизвестная ошибка: {error.message}");
                break;
        }
    }

    /// <returns>AttemptResponse</returns>
    public InfoForRequest GetAttempt(string code) => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/attempt/{code}",
        Data = $"",
        RequestType = RequestType.GET
    };
    /// <returns>StartAttemptResponse</returns>
    public InfoForRequest StartAttempt(int id) => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/start/attempt",
        Data = $"{{\"id\":{id}}}",
        RequestType = RequestType.PUT
    };
    /// <returns>EndAttemptResponse</returns>
    public InfoForRequest EndAttempt(int id, int diagnosisId, int errorCount) => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/end/attempt",
        Data = $"{{\"id\":{id},\"diagnosisId\":{diagnosisId},\"errorCount\":{errorCount}}}",
        RequestType = RequestType.PUT
    };
    /// <returns>StartAttemptResponse</returns>
    public InfoForRequest ResetAttempt(int id) => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/reset/attempt",
        Data = $"{{\"id\":{id}}}",
        RequestType = RequestType.PUT
    };
    /// <returns>Symptoms</returns>
    public InfoForRequest GetSymptoms() => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/symptom",
        Data = $"",
        RequestType = RequestType.GET
    };
    /// <returns>Symptom</returns>
    public InfoForRequest GetSymptom(int id) => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/symptom/{id}",
        Data = $"",
        RequestType = RequestType.GET
    };
    /// <returns>Diagnosis</returns>
    public InfoForRequest GetDiagnosis(int page) => new InfoForRequest()
    {
        Address = $"{NetworkSettings.Address}/diagnosis/{page}",
        Data = $"",
        RequestType = RequestType.GET
    };
}

public class InfoForRequest
{
    public string Address;
    public string Data;
    public RequestType RequestType;
}

public class ErrorResponse
{
    public int code;
    public string message;
}

public enum RequestType : byte
{
    //For Read
    GET = 1,
    //For Create
    POST = 2,
    //For Update
    PUT = 3,
    //For Delete
    DELETE = 4
}
