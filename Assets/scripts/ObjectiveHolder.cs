using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHolder : MonoBehaviour
{
    [HideInInspector]
    public Objective objective;
    GameObject check;
    TextMeshProUGUI text;

    private void Awake()
    {
        check = transform.Find("check").gameObject;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateObjective();
    }

    public void UpdateObjective()
    {
        text.text = objective.text;
        check.SetActive(objective.completed);
    }
}
