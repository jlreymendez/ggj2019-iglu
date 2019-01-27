using UnityEngine;

public class BothLovesHouse : Thought {

  protected override bool IsMet(Bastion bastion) {
    return bastion.HasFamily && bastion.HasLight && bastion.FinalHome;
  }

}
