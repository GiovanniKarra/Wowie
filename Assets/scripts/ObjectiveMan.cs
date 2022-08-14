using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMan : MonoBehaviour
{
    public static ObjectiveMan inst;

    public Objective[] objectives;
    ObjectiveHolder[] holders;
    GameObject objective;

    private void Awake()
    {
        inst = this;
        objective = Resources.Load<GameObject>(@"prefabs\Objective");
        holders = new ObjectiveHolder[objectives.Length];
    }

    private void Start()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            holders[i] = Instantiate(objective, transform).GetComponent<ObjectiveHolder>();
            holders[i].objective = objectives[i];
            holders[i].gameObject.SetActive(!objectives[i].secret);
        }
    }

    public void Trigger(OBJECTIVE obj)
    {
        Objective toTrigger = objectives[(int)obj];
        ObjectiveHolder holderToTrigger = holders[(int)obj];

        if (toTrigger.secret) holderToTrigger.gameObject.SetActive(true);

        toTrigger.completed = true;
        holderToTrigger.UpdateObjective();
    }
}
