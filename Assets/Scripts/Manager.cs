using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    questionState _state;

    Allemand _deu;
    QuestionManager _geo;
    GeoChili _chili;
    GeoFranceVilleCentre _franceVilleCentre;
    GeoFranceAAV _franceAAV;
    HistRevolutionFrancaise _histRevolFrancaise;
    HistEuropeXIXe _histEuropeXIX;
    QuestionHandler _handler;

    float time = 0f;
    bool _time = false;

    [SerializeField]
    Text Chrono;

    void Awake() {
        _deu = GetComponent<Allemand>();
        _chili = GetComponent<GeoChili>();
        _geo = GetComponent<QuestionManager>();
        _franceAAV = GetComponent<GeoFranceAAV>();
        _franceVilleCentre = GetComponent<GeoFranceVilleCentre>();
        _histRevolFrancaise = GetComponent<HistRevolutionFrancaise>();

        Init();        

        switch (_state)
        {
            case questionState.ALLEMAND:
                _handler = _deu;

                break;

            case questionState.GEOGRAPHIE:
                _handler = _geo;

                break;

            case questionState.CHILI:
                _handler= _chili;

                break;

            case questionState.GEO_VILLE_CENTRE:
                _handler = _franceVilleCentre;

                break;

            case questionState.GEO_AAV:
                _handler = _franceAAV;

                break;
            case questionState.HIST_REVOLUTION_DATE:
                _handler = _histRevolFrancaise;

                break;
            case questionState.HIST_EUROPE_XIX:
                _handler = _histEuropeXIX;

                break;
            default:
                EditorApplication.Exit(-1);

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

    void Init() {
        _geo.enabled = false;
        _deu.enabled = false;
        _chili.enabled = false;
        _franceAAV.enabled = false;
        _franceVilleCentre.enabled = false;
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
