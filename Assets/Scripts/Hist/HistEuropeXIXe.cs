using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class HistEuropeXIXe : QuestionHandler {
    int good = 0, bad = 0;
    [SerializeField]
    int starting_index = 0;
    [SerializeField]
    Text questionText, answerText, goodText, badText;

    [SerializeField]
    string[] _eventName = new string[15], _eventDate = new string[15];

    string _answer, _question;

    [SerializeField]
    AudioClip _clipCorrect, _clipUncorrect;

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
        int index = Random.Range(starting_index, _eventName.Length);

        bool isName = Random.Range(0, 8) == 1;

        _question = !isName ? _eventName[index] : _eventDate[index];
        questionText.text = _question;
        _answer = !isName ? _eventDate[index] : _eventName[index];
        _answer = _answer.ToLower();
    }
}
