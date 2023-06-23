using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterIn : MonoBehaviour
{
    public GameObject   UserMessage;
    public GameObject   EnterCode;
    public GameObject   End;
    public Text         EndText;
    public Text         UserText;
    public TMPro.TMP_Dropdown Diag;
    public TMPro.TMP_Dropdown dd1;
    public TMPro.TMP_Dropdown dd2;
    public TMPro.TMP_Dropdown dd3;
    public TMPro.TMP_Dropdown dd4;
    public TMPro.TMP_Dropdown dd5;
    public TMPro.TMP_Dropdown dd6;

    private int answer;
    private string infoAttempt;
    private int idAttempt;
    private bool sozn;
    private string breathe;
    private int oxi;
    private string sphy;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string ValueToSymb(int val)
    {
        switch (val)
        {
            case 0: return  "A";
            case 1: return  "B";
            case 2: return  "C";
            case 3: return  "D";
            case 4: return  "E";
            case 5: return  "F";
            case 6: return  "G";
            case 7: return  "H";
            case 8: return  "I";
            case 9: return  "J";
            case 10: return "K";
            case 11: return "L";
            case 12: return "M";
            case 13: return "N";
            case 14: return "O";
            case 15: return "P";
            case 16: return "Q";
            case 17: return "R";
            case 18: return "S";
            case 19: return "T";
            case 20: return "U";
            case 21: return "Y";
            case 22: return "W";
            case 23: return "X";
            case 24: return "Y";
            case 25: return "Z";
        }
        return "0";
    }

    public void EndMenuActivate()
    {
        End.SetActive(true);
    }

    public void HideMenu()
    {
        UserMessage.SetActive(false);
    }
    public void HideEnterMenu()
    {
        EnterCode.SetActive(false);
    }
    public void ChangeText(int num)
    {
        switch (num)
        {
            case 0: UserText.text = "Инфо о попытке:" + infoAttempt; break;
            case 1:  if (sozn == true) { UserText.text = "Наличие сознание: да"; } else { UserText.text = "Наличие сознание: нет"; }; break; 
            case 2: UserText.text = breathe; break;
            case 3: UserText.text = "Процент кислорода:" + oxi.ToString(); break;
            case 4: UserText.text = sphy; break;
        }
        UserMessage.SetActive(true);
    }
    public void CheckCode()
    {
       
        string[] values = new string[] { ValueToSymb(dd1.value), ValueToSymb(dd2.value), ValueToSymb(dd3.value), ValueToSymb(dd4.value), ValueToSymb(dd5.value), ValueToSymb(dd6.value) };
        string code = string.Join("", values);
        //Debug.Log(code);
        InfoForRequest o = NetworkManager.Instance.GetAttempt(code);
        StartCoroutine(NetworkManager.Instance.SendingRequest<AttemptResponse>(o.Address, o.Data, o.RequestType, (x) =>
        {
            Settings.Attempt = x.attempt;
            answer      = x.attempt.expectedDiagnosis.id;
            idAttempt   = x.attempt.id;
            sozn        = x.attempt.medicalActionDiagnoses[0].symptomMeaning.isFlag;
            breathe     = x.attempt.medicalActionDiagnoses[1].symptomMeaning.value;
            oxi         = x.attempt.medicalActionDiagnoses[2].symptomMeaning.number;
            sphy        = x.attempt.medicalActionDiagnoses[3].symptomMeaning.value;
            infoAttempt = "Пользователь:" + x.attempt.user + " Тип попытки: " + x.attempt.mode.name;

            Debug.Log(x.attempt.medicalActionDiagnoses[0].symptomMeaning.isFlag);
            Debug.Log(x.attempt.medicalActionDiagnoses[1].symptomMeaning.value);
            Debug.Log(x.attempt.medicalActionDiagnoses[2].symptomMeaning.number);
            Debug.Log(x.attempt.medicalActionDiagnoses[3].symptomMeaning.value);
            Debug.Log(x.attempt.mode.name);
            ChangeText(0);


            Debug.Log("Ид попытки = " + idAttempt);
            o = NetworkManager.Instance.ResetAttempt(idAttempt);
            StartCoroutine(NetworkManager.Instance.SendingRequest<StartAttemptResponse>(o.Address, o.Data, o.RequestType, (x) =>
            {
                Debug.Log("Попытка сброшена!");
                o = NetworkManager.Instance.StartAttempt(idAttempt);
                StartCoroutine(NetworkManager.Instance.SendingRequest<StartAttemptResponse>(o.Address, o.Data, o.RequestType, (x) =>
                {
                    Debug.Log("Попытка началась!");
                }));
            }));
            //OpenPanel(_startPanel, Settings.Attempt);
        }));

        o = NetworkManager.Instance.GetDiagnosis(0);
        StartCoroutine(NetworkManager.Instance.SendingRequest<Diagnosis>(o.Address, o.Data, o.RequestType, (x) =>
        {
            List<DiagnosisForAttempt> text = x.diagnosis;
            List<TMP_Dropdown.OptionData> v = new List<TMP_Dropdown.OptionData>();
            foreach (DiagnosisForAttempt diagnosis in text)
            {
                v.Add(new TMP_Dropdown.OptionData() { text = diagnosis.name });
            }
            Diag.ClearOptions();
            Diag.AddOptions(v);
        }));


        HideEnterMenu();
    }

    public void EndExame()
    {
        int diagnosis = Diag.value + 1;
        InfoForRequest o = NetworkManager.Instance.EndAttempt(idAttempt, diagnosis, 0);
        StartCoroutine(NetworkManager.Instance.SendingRequest<EndAttemptResponse>(o.Address, o.Data, o.RequestType, (x) => {
            Debug.Log("Попытка окончена!");
        }));
        Debug.Log(answer);
        Debug.Log(diagnosis);
        if(answer == diagnosis)
        {
            EndText.text = "Верный диагноз!";
        }
        else
        {
            EndText.text = "Неверный диагноз!";
        }
        EndMenuActivate();
    }
}
public class Settings
{
    public static Attempt Attempt;
}
