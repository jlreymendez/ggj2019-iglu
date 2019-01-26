using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludilo;

public class Bastion : MonoBehaviour {

    #region Public
    public bool HasLight {
        get { return _homeEnergy > 0; }
    }

    public bool HasFamily {
        get { return _hasFamily.Value; }
    }

    public void Enter() {
        _onEnter.Invoke();
    }

    public void Exit() {
        _onExit.Invoke();
    }

    public void TurnLight() {
        if (_hasLight) { return; }

        _homeEnergy = _maxEnergy.Value;

        _onTurnOn.Invoke();
    }

    public float GiveLight(float currentLight) {
        if (!_hasFamily) { return 0f; }

        var reduction = Mathf.Min(1 - currentLight, _homeEnergy);
        _homeEnergy -= reduction;
        if (_homeEnergy <= 0f) {
            _onTurnOff.Invoke();
            if (_hasFamily) {
                _hasFamily.Value = false;
                _onFamilyDied.Raise();
            }
        }

        return reduction;
    }

    public void MoveIn() {
        _hasFamily.Value = true;
        _onMoveIn.Invoke();
    }

    public void MoveOut() {
        _homeEnergy = 0f;
        _hasFamily.Value = false;
        _onMoveOut.Invoke();
        _onFamilyMoved.Raise();
    }
    #endregion

    #region UnityAPI
    [SerializeField] FloatReference _maxEnergy;
    [SerializeField] BastionReference _currentBastion;
    [SerializeField] BoolReference _hasLight;
    [SerializeField] BoolReference _hasFamily;
    [SerializeField] GameEvent _onFamilyDied;
    [SerializeField] GameEvent _onFamilyMoved;
    [SerializeField] UnityEvent _onEnter;
    [SerializeField] UnityEvent _onExit;
    [SerializeField] UnityEvent _onTurnOn;
    [SerializeField] UnityEvent _onTurnOff;
    [SerializeField] UnityEvent _onMoveIn;
    [SerializeField] UnityEvent _onMoveOut;

    void Awake() {
        if (_hasFamily) {
            _currentBastion.Value = this;
            _homeEnergy = _maxEnergy;
        }
    }
    #endregion

    #region Private
    float _homeEnergy = 0f;
    #endregion
}
