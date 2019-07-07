using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/HuntFoodDecision")]
public class HuntFoodDecision : Decision {
    public override bool decide(StateController controller) {
        Hunger hunger = controller.GetComponent<Hunger>();
        Thirst thirst = controller.GetComponent<Thirst>();
        Reproduce reproduce = controller.GetComponent<Reproduce>();

        if (hunger.currentHunger >= thirst.currentThirst && hunger.currentHunger >= reproduce.currentReproduce) {
            return true;
        } else {
            return false;
        }
    }
}