using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StateReactor : MonoBehaviour
{

    public Transform MapBase;
    public Color activeRoute = Color.green;
    public Color occupied = Color.blue;
    public Color impaired = Color.yellow;
    public Color drivable = Color.cyan;

    [Serializable]
    public class UnityStringEvent : UnityEvent<string> { }

    public UnityStringEvent DateNotify;

    [Serializable]
    public class UnityColorEvent : UnityEvent<Color> { }

    public UnityColorEvent ActiveRouteColor;
    public UnityColorEvent OccupiedColor;
    public UnityColorEvent ImpairedColor;
    public UnityColorEvent DrivableColor;

    private void OnEnable()
    {
        ActiveRouteColor?.Invoke(activeRoute);
        OccupiedColor?.Invoke(occupied);
        ImpairedColor?.Invoke(impaired);
        DrivableColor?.Invoke(drivable);
    }

    public void HandleDepoState(DepoState state)
    {
        DateNotify?.Invoke(state.sentAt.ToString());
        foreach (var track in state.state.elements.tracks)
        {
            var element = GameObject.Find(track.name);
            if (!element || !element.transform.IsChildOf(MapBase.transform))
            {
                Debug.LogWarning($"can't find obj for '{track.name}'");
                continue;
            }
            UpdateColor(track.occupied ? occupied :
                        track.activeRoute ? activeRoute :
                        track.impaired ? impaired :
                        track.drivable ? drivable : new Color(1f, 1f, 1f, 0f), element.GetComponent<Image>());

        }
    }

    private void UpdateColor(Color color, Image element)
    {
        element.color = color;
        var elements = element.GetComponentsInChildren<Image>().Where(elem => elem != element).ToArray();
        Array.ForEach(elements, e => UpdateColor(color, e));
    }
}
