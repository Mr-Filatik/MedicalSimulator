using System;
using System.Collections.Generic;

public class Entities
{
    //This all antities for NetworkConnection
}


[Serializable]
public class Message
{
    public string message;
}

[Serializable]
public class BoolResponse
{
    public bool value;
}

[Serializable]
public class UserResponse
{
    public int id;
    public string login;
    public string password;
    public string surname;
    public string name;
    public string patronymic;
    public string email;
    public string phone;
    public int age; 
}

[Serializable]
public class UserUpdateRequest
{
    public int id;
    public string login;
    public bool loginUpdate;
    public string surname;
    public bool surnameUpdate;
    public string name;
    public bool nameUpdate;
    public string patronymic;
    public bool patronymicUpdate;
    public string email;
    public bool emailUpdate;
    public string phone;
    public bool phoneUpdate;
    public int age;
    public bool ageUpdate;
    public string password;
    public bool passwordUpdate;
}

[Serializable]
public class Count
{
    public string count;
}

[Serializable]
public class AttemptResponse
{
    public Attempt attempt;
}

[Serializable]
public class Attempt
{
    public int id;
    public UserForAttempt user;
    public UserForAttempt creator;
    public DiagnosisForAttempt expectedDiagnosis;
    public DiagnosisForAttempt specifiedDiagnosis;
    public ModeForAttempt mode;
    public string code;
    public string startDateTime; //timestamp
    public string endDateTime; //timestamp
    public int errorCount = 10;
    public List<MedicalActionDiagnosis> medicalActionDiagnoses;
}

[Serializable]
public class UserForAttempt
{
    public int id;
    public string surname;
    public string name;
    public string patronymic;
}

[Serializable]
public class ModeForAttempt
{
    public int id;
    public string name;
}

[Serializable]
public class DiagnosisForAttempt
{
    public int id;
    public string name;
}

[Serializable]
public class StartAttemptResponse
{
    public DateTime time;
}

[Serializable]
public class EndAttemptResponse
{
    public DateTime time;
    public bool grade;
    public int errorCount;
}

[Serializable]
public class SymptomForAttempt
{
    public int id;
    public string name;
}

[Serializable]
public class MedicalActionDiagnosis
{
    public int id;
    public MedicalAction medicalAction;
    public SymptomMeaning symptomMeaning;
    public int order;
}

[Serializable]
public class MedicalAction
{
    public int id = 1;
    public SymptomForAttempt symptom;
    public string name;
    public string exercise;
}

[Serializable]
public class SymptomMeaning
{
    public int id;
    public SymptomForAttempt symptom;
    public bool isFlag;
    public bool flag;
    public bool isNumber;
    public int number;
    public bool isValue;
    public string value;
    public bool isImage;
    public string image;
    public bool isSound;
    public string sound;
}

[Serializable]
public class Symptoms
{
    public List<Symptom> symptom;
}

[Serializable]
public class Symptom
{
    public SymptomForAttempt symptom;
    public SymptomMeaning symptomMeaning;
}

[Serializable]
public class Diagnosis
{
    public int current_page;
    public int last_page;
    public List<DiagnosisForAttempt> diagnosis;
}