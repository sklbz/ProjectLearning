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
    string answer, question;

    [SerializeField]
    AudioClip clipCorrect, clipUncorrect;

    bool isInitialized = false;

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

        Debug.Log("Some test text. é");
        Debug.Log(NormalizeText("Some test text. é"));

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

        questionText.text = question;
    }

    public override void Submit() {
        string input = answerText.text;
        Debug.Log(NormalizeText(input));

        EmitSound(clipCorrect);

        UpdateValues();

        GenerateQuestion();
    }

    void UpdateValues() {
        Debug.Log($"good:{good}\tbad:{bad}\nQ: {question}\tA: {answer}");

        goodText.text = good.ToString();
        badText.text = bad.ToString();
    }

    string NormalizeText( string text ) {
        text = text.ToLower();
        

        return text.Filter(true, true, false, false, false);
    }
}
