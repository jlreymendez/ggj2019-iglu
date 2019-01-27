using UnityEngine;
using UnityEngine.Events;
using Ludilo;

public class ColliderTriggerEvents : MonoBehaviour {

  #region UnityAPI
  [SerializeField] ColliderEvent _onEnter;
  [SerializeField] ColliderEvent _onExit;
  [SerializeField] StringReference _targetLayer;


  void OnTriggerEnter(Collider collider) {
    //Debug.LogFormat("Enter {0}", LayerMask.LayerToName(collider.gameObject.layer));
    if (collider.gameObject.layer == LayerMask.NameToLayer(_targetLayer.Value)) {
      _onEnter.Invoke(collider);
    }
  }

  void OnTriggerExit(Collider collider) {
    if (collider.gameObject.layer == LayerMask.NameToLayer(_targetLayer.Value)) {
      _onExit.Invoke(collider);
    }
  }
  #endregion
}

[System.Serializable]
public class ColliderEvent : UnityEvent<Collider> {}