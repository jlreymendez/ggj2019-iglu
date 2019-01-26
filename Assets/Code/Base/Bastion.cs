using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludilo;

public class Bastion : MonoBehaviour {

    #region Public
    public bool HasLight {
        get { return _hasLight.Value; }
    }

    public bool HasFamily {
        get { return _hasFamily.Value; }
    }

    public void TurnLight() {
        if (_hasLight) { return; }

        _hasLight.Value = true;

        _onTurnOn.Invoke();
    }

    public void MoveIn() {
        _hasFamily.Value = true;
        _onMoveIn.Invoke();
    }

    public void MoveOut() {
        _hasFamily.Value = false;
        _onMoveOut.Invoke();
    }
    #endregion

    #region UnityAPI
    [SerializeField] BastionReference _currentBastion;
    [SerializeField] BoolReference _hasLight;
    [SerializeField] BoolReference _hasFamily;
    [SerializeField] UnityEvent _onTurnOn;
    [SerializeField] UnityEvent _onMoveIn;
    [SerializeField] UnityEvent _onMoveOut;

    void Awake() {
        if (_currentBastion != null) {
            _currentBastion.Value = this;
        }
    }
    #endregion

    #region Private
    FloatReference _lightStrength;
    #endregion
}
