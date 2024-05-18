using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HistIIIeRep : QuestionHandler {
    int good = 0, bad = 0;
    [SerializeField]
    int starting_index = 0, ending_index;
    [SerializeField]
    Text questionText, answerText, goodText, badText;

    [SerializeField]
    string[] _eventName = new string[15], _eventDate = new string[15];

    string _answer, _question;

    [SerializeField]
    AudioClip _clipCorrect, _clipUncorrect;

    void Start() {
        if (ending_index == 0) ending_index = _eventDate.Length;
    }

    public override void Submit() {
        if (answerText.text.ToLower() == _answer)
        {
            _source.PlayOneShot(_clipCorrect);
            good++;
        }
        else
        {
            _source.PlayOneShot(_clipUncorrect);
            bad++;
        }

        UpdateValues();

        GenerateQuestion();
    }

    void UpdateValues() {
        Debug.Log($"good:{good}\tbad:{bad}\t\tQ: {_question}\tA: {_answer}");

        goodText.text = good.ToString();
        badText.text = bad.ToString();
    }

    public override void GenerateQuestion() {
        int index = Random.Range(starting_index, ending_index);

        bool isName = Random.Range(0, 2) == 1;

        _question = !isName ? _eventName[index] : _eventDate[index];
        questionText.text = _question;
        _answer = !isName ? _eventDate[index] : _eventName[index];
        _answer = _answer.ToLower();
    }
}
