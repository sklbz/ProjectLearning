using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class QuestionHandler : MonoBehaviour
{
    /*int good = 0, bad = 0;
    [SerializeField]
    Text questionText, answerText, goodText, badText;

    [SerializeField]
    string[] _dataName = new string[15], _dataHab = new string[15];

    string _answer, _question;

    [SerializeField]
    AudioClip _clipCorrect, _clipUncorrect;*/
    public AudioSource _source;

    void Start() {
        _source = GetComponent<AudioSource>();
    }

    public abstract void GenerateQuestion();
    public abstract void Submit();

    public void EmitSound(AudioClip clip) {
        _source.PlayOneShot(clip);
    }
}
