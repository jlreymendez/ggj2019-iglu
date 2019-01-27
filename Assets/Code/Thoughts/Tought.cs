using UnityEngine;
using UnityEngine.UI;

public abstract class Thought : MonoBehaviour {

  public Sprite Icon {
    get { return _icon; }
  }

  public bool Evaluate(Bastion bastion) {
    if (bastion.Emptied) { return false; }

    return IsMet(bastion);
  }

  [SerializeField] protected Sprite _icon;
  [SerializeField] protected BastionReference _newHome;

  abstract protected bool IsMet(Bastion bastion);

}
