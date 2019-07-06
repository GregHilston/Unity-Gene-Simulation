﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action : ScriptableObject {
    public abstract void act(StateController controller);
    public abstract void DrawGizmos();
}