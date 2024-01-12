using UnityEngine;
using UnityEngine.UI;

public class GeoChili : QuestionHandler {
    int good = 0, bad = 0;
    [SerializeField]
    Text questionText, answerText, goodText, badText;
    [SerializeField]
    string[] _dataName = new string[10], _dataValue = new string[10];

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
        int index = Random.Range(0, _dataName.Length);


        _question =  _dataName[index];
        questionText.text = _question;

        _answer = _dataValue[index];
        _answer = _answer.ToLower();
    }
}
