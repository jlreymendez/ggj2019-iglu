using UnityEngine;

public class ColorSetter : MonoBehaviour {


  public void SetColor(int index) {
    _target.color = _colors[index];
  }

  [SerializeField] Color[] _colors;

  void Awake() {
    _target = GetComponent<SpriteRenderer>();
  }

  SpriteRenderer _target;
}