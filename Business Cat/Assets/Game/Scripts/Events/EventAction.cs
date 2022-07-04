using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ContentUI))]
public class EventAction : MonoBehaviour
{
    public static EventAction Instance { get; private set; }

    [SerializeField] private float timeBeforeShow;
    [Space(10)]
    [SerializeField] private TMP_Text actionDescriptionText;
    [SerializeField] private VariantView variant1;
    [SerializeField] private VariantView variant2;

    private ContentUI content;
    private Action action;

    private float timer;

    private void Awake()
    {
        Instance = this;
        content = GetComponent<ContentUI>();
    }

    private void Update()
    {
        if (timer <= 0)
        {
            content.Show();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void StartEvent(Event e)
    {
        content.Hide();

        action = e.GetRandomAction();
        actionDescriptionText.text = action.Description;


        timer = timeBeforeShow;
    }
}