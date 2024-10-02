using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DynamicDataQuiz : QuestionHandler
{
    DataHolder holder;
    readonly string[][] data = new string[2][];

    int good = 0, bad = 0, dataLength = int.MaxValue;
    Text questionText, answerText, goodText, badText;
    string answer, question, normalizedAnswer;

    [SerializeField]
    AudioClip clipCorrect, clipUncorrect;

    [SerializeField]
    bool reduceToAlphaNum = true;

    bool isInitialized = false;
    
    readonly Dictionary<char, char> map = new();

    [SerializeField]
    int start, end;
    int Start {
        set {
            start = Mathf.Clamp(value, 0, dataLength);
        }
    }
    int End {
        set {
            end = Mathf.Clamp(value, 0, dataLength);
        }
    }


    void Init() {
        holder = GetComponent<DataHolder>();
        data[0] = new string[0];
        data[1] = new string[0];
        data[0] = holder.Data1;
        data[1] = holder.Data2;

        if (data[0].Length != data[1].Length)
            Debug.LogError("Improper data format. Not matching Length.");

        dataLength = data[0].Length;

        questionText = FindObjectOfType<QuestionText>().GetComponent<Text>();
        answerText = FindObjectOfType<AnswerText>().GetComponent<Text>();
        goodText = FindObjectOfType<GoodText>().GetComponent<Text>();
        badText = FindObjectOfType<BadText>().GetComponent<Text>();

        InitMap();

        Start = start;
        End = end;

        isInitialized = true;
    }

    public override void GenerateQuestion() {
        if (!isInitialized)
            Init();

        int whichListIsQuestion = Random.Range(0, 2);
        int whichListIsAnswer = 1 - whichListIsQuestion;

        string[] questionList = data[whichListIsQuestion];
        string[] answerList = data[whichListIsAnswer];

        int index = Random.Range(start, end);

        question = questionList[index];
        answer = answerList[index];

        normalizedAnswer = NormalizeText(answer);

        questionText.text = question;
    }

    public override void Submit() {
        string input = answerText.text;
        string normalizedInput = NormalizeText(input);

        if (normalizedInput == normalizedAnswer)
        {
            EmitSound(clipCorrect);
            good++;
        }
        else
        {
            EmitSound(clipUncorrect);
            bad++;
        }


        UpdateValues();

        GenerateQuestion();
    }

    void UpdateValues() {
        Debug.Log($"good:{good}\tbad:{bad}\nQ: {question}\tA: {answer}");

        goodText.text = good.ToString();
        badText.text = bad.ToString();
    }

    void InitMap() {
        string letters = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";

        for (int i = 0; i < letters.Length; i++)
        {
            map[letters[i]] = letters[i];
        }
        for(int i = 0; i < numbers.Length; i++)
        {
            map[numbers[i]] = numbers[i];
        }

        map['é'] = 'e';
        map['è'] = 'e';
        map['ê'] = 'e';
        map['ï'] = 'i';
        map['à'] = 'a';
        map['ù'] = 'u';
    }

    string NormalizeText(string text) {
        text = text.ToLower();

        text = text.Filter(true, true, false, false, false);

        if (reduceToAlphaNum)
            text = ReduceString(text);

        return text;
    }

    string ReduceString(string text) {
        char[] split = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            split[i] = map[text[i]];
        };

        return new string(split);
    }
}
