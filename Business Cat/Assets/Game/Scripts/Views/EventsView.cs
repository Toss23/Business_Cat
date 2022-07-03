using System.Collections.Generic;
using UnityEngine;

public class EventsView : MonoBehaviour
{
    [SerializeField] private EventsGenerator eventsGenerator;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject eventPrefab;

    private List<GameObject> eventObjects;

    private void Awake()
    {
        eventObjects = new List<GameObject>();
        eventsGenerator.OnUpdate.Add(new Runnable(UpdateView));
    }

    private void UpdateView()
    {
        Event[] currentEvents = eventsGenerator.CurrentEvents;

        foreach (GameObject gameObject in eventObjects)
            Destroy(gameObject);

        eventObjects.Clear();

        foreach (Event currentEvent in currentEvents)
        {
            GameObject eventObject = Instantiate(eventPrefab, parent);
            eventObject.name = "Event";

            EventView eventView = eventObject.GetComponent<EventView>();
            eventView.SetEvent(currentEvent);

            eventObjects.Add(eventObject);
        }
    }
}