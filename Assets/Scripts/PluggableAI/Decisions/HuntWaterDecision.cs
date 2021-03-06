﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/HuntWaterDecision")]
public class HuntWaterDecision : Decision {
    public override bool decide(StateController controller) {
        Thirst thirst = controller.GetComponent<Thirst>();
        Hunger hunger = controller.GetComponent<Hunger>();
        Reproduce reproduce = controller.GetComponent<Reproduce>();

        if(thirst.currentThirst >= hunger.currentHunger && thirst.currentThirst >= reproduce.currentReproduce) {
            return true;
        } else {
            return false;
        }
    }
}