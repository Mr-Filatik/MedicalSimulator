using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Text;
using Palmmedia.ReportGenerator.Core.Common;

public class TestController : MonoBehaviour
{
    public TMP_Text text1 = null;
    public TMP_InputField inputField1 = null;
    public GameObject wait = null;
    public GameObject error = null;
    

    void Start()
    {
        text1.SetText($"Start");
        inputField1.text = $"{NetworkSettings.Address}/user";
        //StartCoroutine(getRequest($"{NetworkSettings.Address}/v1/greeter/{"Test on http1"}"));
        //http://server.medical-sumulator.h1n.ru/

        NetworkManager.Instance.StartWaiting = () => wait.SetActive(true);
        NetworkManager.Instance.CanselWaiting = () => wait.SetActive(false);
        NetworkManager.Instance.Logout = () => Debug.Log("Logout");
        NetworkManager.Instance.Error = () => error.SetActive(true);
    }

    public void CloseError()
    {
        error.SetActive(false);
    }

    public void Send()
    {
        if (!string.IsNullOrEmpty(inputField1.text))
        {
            Debug.Log("Path: " + inputField1.text);
            //string data1 = "text={\"id\":3,\"login\":\"3\",\"surname\":\"3\",\"name\":\"3\",\"patronymic\":\"3\",\"email\":\"\",\"phone\":\"\",\"age\":0}";
            //var utf8 = Encoding.UTF8;
            //byte[] utfBytes = utf8.GetBytes(data1);
            //string data = utf8.GetString(utfBytes, 0, utfBytes.Length);
            string data = "{\"id\":3,\"login\":\"3\",\"surname\":\"3\",\"name\":\"3\",\"patronymic\":\"3\",\"email\":\"\",\"phone\":\"\",\"age\":12,\"ageUpdate\":true}";
            //var a = new
            //{
            //    id = 1,
            //    login = "new",
            //    surname = "new",
            //    name = "new",
            //    patronymic = "new",
            //    email = "new",
            //    phone = "new",
            //    age = 8,
            //    password = "new",
            //};
            //string data = JsonUtility.ToJson(a);
            //string data = JsonSerializer.ToJsonString(a).Replace(" ", "");
            Debug.Log(data);
            //string data = "id=3&login=3&surname=3&name=3&patronymic=3&email=&phone=&age=0";
            //StartCoroutine(NetworkManager.Instance.SendingRequest<UserResponse>(inputField1.text, data, RequestType.POST, (x) => { Debug.Log($"Res - {x.surname}"); }));
            //StartCoroutine(NetworkManager.Instance.SendingRequest<BoolResponse>(inputField1.text, data, RequestType.PUT, (x) => { Debug.Log($"Res - {x.value}"); }));
            StartCoroutine(NetworkManager.Instance.SendingRequest<BoolResponse>(inputField1.text, data, RequestType.DELETE, (x) => { Debug.Log($"Res - {x.value}"); }));
        }
        else
        {
            inputField1.text = $"{NetworkSettings.Address}/user";
        }
    }

    

    public void Get1()
    {
        InfoForRequest o = NetworkManager.Instance.GetAttempt("EDEDED");
        StartCoroutine(NetworkManager.Instance.SendingRequest<AttemptResponse>(o.Address, o.Data, o.RequestType, (x) => { 
            Debug.Log($"Res - {x.attempt.startDateTime}");
            var i = ConversionOperations.GetDateTimeFromTimestamp(x.attempt.startDateTime);
            i = i.AddDays(2);
            Debug.Log($"Res(R) - {ConversionOperations.GetTimestampFromDateTime(i)}"); }));
    }

    public void Get2()
    {
        InfoForRequest o = NetworkManager.Instance.StartAttempt(1);
        StartCoroutine(NetworkManager.Instance.SendingRequest<StartAttemptResponse>(o.Address, o.Data, o.RequestType, (x) => { Debug.Log($"Res - {x.time}"); }));
    }

    public void Get3()
    {
        InfoForRequest o = NetworkManager.Instance.EndAttempt(1, 1, 0);
        StartCoroutine(NetworkManager.Instance.SendingRequest<EndAttemptResponse>(o.Address, o.Data, o.RequestType, (x) => { Debug.Log($"Res - {x.grade}"); }));
    }

    public void Get4()
    {
        InfoForRequest o = NetworkManager.Instance.ResetAttempt(1);
        StartCoroutine(NetworkManager.Instance.SendingRequest<StartAttemptResponse>(o.Address, o.Data, o.RequestType, (x) => { Debug.Log($"Res - {x.time}"); }));
    }

    public void Get5()
    {
        InfoForRequest o = NetworkManager.Instance.GetSymptoms();
        StartCoroutine(NetworkManager.Instance.SendingRequest<Symptoms>(o.Address, o.Data, o.RequestType, (x) => { Debug.Log($"Res - {x.symptom.Count}"); }));
    }

    public void Get6()
    {
        InfoForRequest o = NetworkManager.Instance.GetSymptom(1);
        StartCoroutine(NetworkManager.Instance.SendingRequest<Symptom>(o.Address, o.Data, o.RequestType, (x) => { Debug.Log($"Res - {x.symptomMeaning.number}"); }));
    }
}
