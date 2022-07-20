using System.Collections.Generic;
using UnityEngine;

public class EventsGenerator : MonoBehaviour, IUpdatable
{
    [SerializeField] private Events events;
    [SerializeField] private int eventsCountMaximum;
    [SerializeField] private Event[] currentEvents;
    [SerializeField] private List<Runnable> onUpdate;

    public List<Runnable> OnUpdate
    {
        get
        {
            if (onUpdate == null) onUpdate = new List<Runnable>();
            return onUpdate;
        }
    }
    public Event[] CurrentEvents { get { return currentEvents; } }

    public void OnStep()
    {
        UpdateEvents();
    }

    private void Awake()
    {
        events.OnLoad.Add(new Runnable(UpdateEvents));
        StepSystem.Instance.AddListener(this);
    }

    private void UpdateEvents()
    {
        if (events.Array.Length > 0)
        {
            List<Event> generatedEvents = new List<Event>();

            for (int i = 0; i < eventsCountMaximum; i++)
            {
                int eventIndex = Random.Range(0, events.Array.Length);
                bool duplicate = false;

                foreach (Event e in generatedEvents)
                {
                    if (e == events.Array[eventIndex])
                        duplicate = true;
                }

                if (duplicate == false)
                    generatedEvents.Add(events.Array[eventIndex]);
            }

            currentEvents = generatedEvents.ToArray();

            if (onUpdate != null)
            {
                foreach (Runnable runnable in onUpdate)
                    runnable.Run();
            }
        }
    }
}