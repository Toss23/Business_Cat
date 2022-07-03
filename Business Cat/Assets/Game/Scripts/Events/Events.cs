using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    [SerializeField] private Event[] events;
    [SerializeField] private List<Runnable> onLoad;

    public List<Runnable> OnLoad
    {
        get
        {
            if (onLoad == null) onLoad = new List<Runnable>();
            return onLoad;
        }
    }
    public Event[] Array { get { return events; } }

    private void Start()
    {
        Debug.Log("[Events] Loading events...");

        events = Resources.LoadAll<Event>("Events");

        Debug.Log("[Events] Events loaded: " + events.Length);

        if (onLoad != null)
        {
            foreach (Runnable runnable in onLoad)
                runnable.Run();
        }
    }
}