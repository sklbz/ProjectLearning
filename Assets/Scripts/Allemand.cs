using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Allemand : QuestionHandler
{
    int good = 0, bad = 0;
    [SerializeField]
    Text questionText, answerText, goodText, badText;
    [SerializeField]
    string[] _deutsch = new string[10], _francais = new string[10];

    string _answer, _question;

    [SerializeField]
    AudioClip _clipCorrect, _clipUncorrect;
    /*AudioSource _source;

    void Start() {
        _source = GetComponent<AudioSource>();
        Debug.Log(_source);
    }*/

    public override void Submit() {
        if (answerText.text.ToLower() == _answer)
        {
            good++;
            _source.PlayOneShot(_clipCorrect);
        }
        else
        {
            bad++;
            _source.PlayOneShot(_clipUncorrect);
        }

        UpdateValues();

        GenerateQuestion();
    }

    void UpdateValues() {
        Debug.Log($"good:{good}\tbad:{bad}\nQ: {_question}\tA: {_answer}");

        goodText.text = good.ToString();
        badText.text = bad.ToString();
    }

    public override void GenerateQuestion() {
        int index = Random.Range(0, _deutsch.Length);

        bool isQuestionDeutsch = Random.Range(0, 2) == 1;

        _question = isQuestionDeutsch ? _deutsch[index] : _francais[index];
        questionText.text = _question;

        _answer = isQuestionDeutsch ? _francais[index] : _deutsch[index];
        _answer = _answer.ToLower();
    }
}
