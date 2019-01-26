using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class BaseMover : MonoBehaviour {

    #region Public
    public void MoveBase(Collider collider) {
      var bastion = collider.GetComponentInParent<Bastion>();
      // Only move when visiting current bastion and target bastion is set.
      Debug.Log(_currentBastion.Value);
      Debug.Log(_targetBastion.Value);
      Debug.Log(bastion == _currentBastion.Value);
      if (
        _currentBastion.Value == null ||
        bastion != _currentBastion.Value ||
        _targetBastion.Value == null
      ) {
        return;
      }
      Debug.Log("Moverse");
      _currentBastion.Value.MoveOut();
      _targetBastion.Value.MoveIn();
      _currentBastion.Value = _targetBastion;
      _targetBastion.Value = null;
    }
    #endregion

    #region UnityAPI
    [SerializeField] BastionReference _targetBastion;
    [SerializeField] BastionReference _currentBastion;
    #endregion
}
