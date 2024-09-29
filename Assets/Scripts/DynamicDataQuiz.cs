using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DynamicDataQuiz : QuestionHandler
{
    DataHolder holder;
    readonly string[][] data = new string[2][];

    int good = 0, bad = 0;
    Text questionText, answerText, goodText, badText;
    string answer, question, normalizedAnswer;

    [SerializeField]
    AudioClip clipCorrect, clipUncorrect;

    [SerializeField]
    bool reduceToAlphaNum = true;

    bool isInitialized = false;
    
    readonly Dictionary<char, char> map = new();

    void Init() {
        holder = GetComponent<DataHolder>();
        Debug.Log(data[0]);
        data[0] = new string[0];
        data[1] = new string[0];
        data[0] = holder.Data1;
        data[1] = holder.Data2;

        questionText = FindObjectOfType<QuestionText>().GetComponent<Text>();
        answerText = FindObjectOfType<AnswerText>().GetComponent<Text>();
        goodText = FindObjectOfType<GoodText>().GetComponent<Text>();
        badText = FindObjectOfType<BadText>().GetComponent<Text>();

        InitMap();

        Debug.Log("Some test text. é");
        Debug.Log(NormalizeText("Some test text. é ï, '"));

        isInitialized = true;
    }

    public override void GenerateQuestion() {
        if (!isInitialized)
            Init();

        int whichListIsQuestion = Random.Range(0, 2);
        int whichListIsAnswer = 1 - whichListIsQuestion;

        string[] questionList = data[whichListIsQuestion];
        string[] answerList = data[whichListIsAnswer];

        int index = Random.Range(0, data[0].Length);

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

        for (int i = 0; i < letters.Length; i++)
        {
            map[letters[i]] = letters[i];
        }

        map['é'] = 'e';
        map['è'] = 'e';
        map['ï'] = 'i';
        map['à'] = 'a';
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
