using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    QuestionState _state;

    Allemand _deu;
    QuestionManager _geo;
    GeoChili _chili;
    GeoFranceVilleCentre _franceVilleCentre;
    GeoFranceAAV _franceAAV;
    HistRevolutionFrancaise _histRevolFrancaise;
    HistEuropeXIXe _histEuropeXIX;
    HistIIIeRep _histIIIeRep;
    DynamicDataQuiz _dynamic;
    QuestionHandler _handler;

    float time = 0f;
    bool _time = false;

    [SerializeField]
    Text Chrono;

    void Awake() {
        DynamicFind();

        Init();        

        switch (_state)
        {
            case QuestionState.ALLEMAND:
                _handler = _deu;

                break;

            case QuestionState.GEOGRAPHIE:
                _handler = _geo;

                break;

            case QuestionState.CHILI:
                _handler= _chili;

                break;

            case QuestionState.GEO_VILLE_CENTRE:
                _handler = _franceVilleCentre;

                break;

            case QuestionState.GEO_AAV:
                _handler = _franceAAV;

                break;
            case QuestionState.HIST_REVOLUTION_DATE:
                _handler = _histRevolFrancaise;

                break;
            case QuestionState.HIST_EUROPE_XIX:
                _handler = _histEuropeXIX;

                break;
            case QuestionState.HIST_IIIe_REP:
                _handler = _histIIIeRep;
                
                break;
            case QuestionState.DYNAMIC:
                _handler = _dynamic;

                break;
        }

        _handler.enabled = true;
        _handler.GenerateQuestion();
    }

    public void Submit() {
        _handler.Submit();

        if(!_time)
            LaunchChrono();
    }

    void DynamicFind() {
        _deu = GetComponent<Allemand>();
        _chili = GetComponent<GeoChili>();
        _geo = GetComponent<QuestionManager>();
        _franceAAV = GetComponent<GeoFranceAAV>();
        _franceVilleCentre = GetComponent<GeoFranceVilleCentre>();
        _histRevolFrancaise = GetComponent<HistRevolutionFrancaise>();
        _histEuropeXIX = GetComponent<HistEuropeXIXe>();
        _histIIIeRep = GetComponent<HistIIIeRep>();
        _dynamic = GetComponent<DynamicDataQuiz>();
    }

    void Init() {
        _geo.enabled = false;
        _deu.enabled = false;
        _chili.enabled = false;
        _franceAAV.enabled = false;
        _franceVilleCentre.enabled = false;
        _histEuropeXIX.enabled = false;
        _histRevolFrancaise.enabled = false;
        _histIIIeRep.enabled = false;
        _dynamic.enabled = false;
    }

    void Update() {
        if (Input.GetButtonDown("Submit"))
            Submit();
    }

    void LateUpdate() {
        if (!_time)
            return;

        time += Time.fixedDeltaTime;
        Chrono.text = $"{time:0.00}";
    }

    private void LaunchChrono() {
        _time = true;
    }
}
