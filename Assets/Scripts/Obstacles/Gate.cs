using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private enum OperationType
    {
        add,
        subtract,
        multiply,
        division
    }

    [Header("Operation")]
    [SerializeField] OperationType gateOperation;
    [SerializeField] int value;

    [Header("References")]
    [SerializeField] TextMeshPro operationText;
    [SerializeField] MeshRenderer forceField;
    [SerializeField] Material[] operationTypeMaterial;

    private void Awake()
    {
        AssignOperation();
    }

    private void AssignOperation()
    {
        string text = "";
        if(gateOperation == OperationType.add) 
            text += "+";
        if(gateOperation == OperationType.subtract)
            text += "-";
        if(gateOperation == OperationType.multiply)
            text += "x";
        if (gateOperation == OperationType.division)
            text += "%";

        text += value.ToString();
        operationText.text = text;

        if(gateOperation == OperationType.add || gateOperation == OperationType.multiply)
        {
            forceField.material = operationTypeMaterial[0];
        } else
        {
            forceField.material = operationTypeMaterial[1];
        }
    }

    public void ExecuteOperation()
    {
        if (gateOperation == OperationType.add)
            GameEvent.instance.playerSize.Value += value;
        if (gateOperation == OperationType.subtract)
            GameEvent.instance.playerSize.Value -= value;
        if (gateOperation == OperationType.multiply)
            GameEvent.instance.playerSize.Value *= value;
        if (gateOperation == OperationType.division)
            GameEvent.instance.playerSize.Value /= value;
    
        GetComponent<BoxCollider>().enabled = false;
        forceField.gameObject.SetActive(false);
    }
}
