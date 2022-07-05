using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ContentUI))]
public class EventAction : MonoBehaviour
{
    public enum State
    {
        Reset, Open, Variant, Result
    }

    public static EventAction Instance { get; private set; }

    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private float timeBeforeShow;
    [Space(10)]
    [SerializeField] private TMP_Text actionDescriptionText;
    [SerializeField] private VariantView variant0;
    [SerializeField] private VariantView variant1;

    private ContentUI content;
    private Action action;

    private State state;
    private float timer;

    private void Awake()
    {
        Instance = this;
        content = GetComponent<ContentUI>();
        successScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;

        if (timer <= 0 & state == State.Open)
        {
            content.Show();
            state = State.Variant;
        }

        if (timer <= 0 & state == State.Result)
        {
            state = State.Reset;
            Screens.Instance.InstantlyOpen("Home");
        }
    }

    public void StartEvent(Event e)
    {
        state = State.Open;
        successScreen.SetActive(false);
        loseScreen.SetActive(false);
        content.Hide();

        action = e.GetRandomAction();
        actionDescriptionText.text = action.Description;
        variant0.SetVariant(action.Variants[0]);

        bool haveSecondVariant = action.Variants.Length == 2;
        variant1.gameObject.SetActive(haveSecondVariant);
        if (haveSecondVariant)
            variant1.SetVariant(action.Variants[1]);

        timer = timeBeforeShow;
    }

    public void VariantSelected(bool success)
    {
        state = State.Result;
        timer = timeBeforeShow;
        content.Hide();

        if (success)
            successScreen.SetActive(true);
        else
            loseScreen.SetActive(true);
    }
}