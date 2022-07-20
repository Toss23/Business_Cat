using System.Collections.Generic;
using UnityEngine;

public class StepSystem : MonoBehaviour
{
    public static StepSystem Instance;

    private int stepsCount;
    private List<IUpdatable> onNextStep;

    private void Awake()
    {
        onNextStep = new List<IUpdatable>();
        Instance = this;
    }

    public void NextStep()
    {
        stepsCount++;

        if (onNextStep != null)
        {
            foreach (IUpdatable updatable in onNextStep)
                updatable.OnStep();
        }
    }

    public void AddListener(IUpdatable updatable)
    {
        if (onNextStep == null) onNextStep = new List<IUpdatable>();
        onNextStep.Add(updatable);
    }
}