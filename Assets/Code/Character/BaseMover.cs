using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class BaseMover : MonoBehaviour {

    #region Public
    public void MoveBase(Collider collider) {
      var bastion = collider.GetComponentInParent<Bastion>();
      // Only move when visiting current bastion and target bastion is set.
      if (
        _currentBastion.Value == null ||
        bastion != _currentBastion.Value ||
        _targetBastion.Value == null||
        _targetBastion.Value == _currentBastion.Value
      ) {
        return;
      }

      // TODO: If target bastion doesn't allow family inform?
      if (!_targetBastion.Value.AllowFamily) {
        return;
      }

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
