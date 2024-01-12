using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    questionState _state;

    Allemand _deu;
    QuestionManager _geo;
    GeoChili _chili;
    GeoFranceVilleCentre _franceVilleCentre;
    GeoFranceAAV _franceAAV;
    QuestionHandler _handler;

    void Awake() {
        _deu = GetComponent<Allemand>();
        _chili = GetComponent<GeoChili>();
        _geo = GetComponent<QuestionManager>();
        _franceAAV = GetComponent<GeoFranceAAV>();
        _franceVilleCentre = GetComponent<GeoFranceVilleCentre>();

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
        }

        _handler.enabled = true;
        _handler.GenerateQuestion();
    }

    public void Submit() {
        _handler.Submit();
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
}
