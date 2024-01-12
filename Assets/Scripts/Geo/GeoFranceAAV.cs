using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GeoFranceAAV : QuestionHandler
{
    int good = 0, bad = 0;
    [SerializeField]
    Text questionText, answerText, goodText, badText;

    [SerializeField]
    string[] _dataName = new string[15], _dataHab = new string[15];

    string _answer, _question;

    [SerializeField]
    AudioClip _clipCorrect, _clipUncorrect;
    /*AudioSource _source;

    void Start() {
        _source = GetComponent<AudioSource>();
    }*/

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
        Debug.Log($"good:{good}\tbad:{bad}\nQ: {_question}\tA: {_answer}");

        goodText.text = good.ToString();
        badText.text = bad.ToString();
    }

    public override void GenerateQuestion() {
        int index = Random.Range(0, _dataName.Length);
        string rank = $"{index + 1}eme";

        bool isName = Random.Range(0, 8) == 1;

        _question = !isName ? _dataName[index] : rank.ToString();
        questionText.text = isName ?  _question : $"{_question} AAV";
        _answer = !isName ? _dataHab[index] : _dataName[index];
        _answer = _answer.ToLower();
    }
}
