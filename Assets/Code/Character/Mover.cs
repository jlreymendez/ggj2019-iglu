using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class Mover : MonoBehaviour {

    #region UnityAPI
    [SerializeField] FloatReference _speed;

    void Awake() {
    }

    void Update() {
        var vInput = Input.GetAxis("Vertical");
        var hInput = Input.GetAxis("Horizontal");
        var position = new Vector3(
            transform.position.x + hInput * _speed * Time.deltaTime,
            0,
            transform.position.z + vInput * _speed * Time.deltaTime
        );
        transform.position = position;
    }
    #endregion

    #region Private
    #endregion
}
