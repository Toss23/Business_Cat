using System.Collections.Generic;
using UnityEngine;

public class EventsGenerator : MonoBehaviour
{
    [SerializeField] private Events events;
    [SerializeField] private int eventsCount;
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

    private void Awake()
    {
        events.OnLoad.Add(new Runnable(UpdateEvents));
    }

    private void UpdateEvents()
    {
        if (events.Array.Length > 0)
        {
            currentEvents = new Event[eventsCount];

            for (int i = 0; i < eventsCount; i++)
            {
                int eventIndex = Random.Range(0, events.Array.Length);
                currentEvents[i] = events.Array[eventIndex];
            }

            if (onUpdate != null)
            {
                foreach (Runnable runnable in onUpdate)
                    runnable.Run();
            }
        }
    }
}