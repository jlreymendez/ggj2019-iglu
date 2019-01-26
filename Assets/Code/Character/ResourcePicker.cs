using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class ResourcePicker : MonoBehaviour {

    #region Public
    public void PickResource(Collider collider) {
      // If player is not carrying resource grab it.
      if (!_carryingResource.Value) {
        _carryingResource.Value = true;
        Destroy(collider.gameObject);

        if (_resourcePicked != null) {
          _resourcePicked.Raise();
        }
      }
    }

    public void UseResource(Collider collider) {
      // If player is not carrying resource grab it.
      var bastion = collider.GetComponentInParent<Bastion>();
      if (_carryingResource.Value && !bastion.HasLight) {
        _carryingResource.Value = true;
        bastion.TurnLight();

        if (_resourceUsed != null) {
          _resourceUsed.Raise();
          _targetBastion.Value = bastion;
        }
      }
    }
    #endregion

    #region UnityAPI
    [SerializeField] BastionReference _targetBastion;
    [SerializeField] BoolReference _carryingResource;
    [SerializeField] GameEvent _resourcePicked;
    [SerializeField] GameEvent _resourceUsed;
    #endregion
}
