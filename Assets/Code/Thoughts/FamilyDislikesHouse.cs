using UnityEngine;

public class FamilyDislikesHouse : Thought {

  protected override bool IsMet(Bastion bastion) {
    return bastion.HasFamily && bastion.HasLight && !bastion.FinalHome;
  }

}
