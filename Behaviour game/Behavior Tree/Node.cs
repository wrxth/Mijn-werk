using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// het basis node script waarop alle andere nodes op zijn gebaseerd
public abstract class Node : MonoBehaviour
{
    protected NodeState ns;

    public NodeState nodeState { get { return ns; } }

    // moet in elke script die node inherit worden gebruikt
    public abstract NodeState Evalute();
}

// de nodestates die gereturned kunnen worden
public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}
