using System.Collections.Generic;
using UnityEngine;

public class WeightedRandomBag
{
    private class Entry
    {
        public double weight;
        public double accumulatedWeight;
        public object item;
    }

    private double accumulatedWeight;
    private List<Entry> entries = new List<Entry>();

    public void addEntry(object T, double weight)
    {
        this.accumulatedWeight += weight;
        Entry e = new Entry
        {
            item = T,
            weight = weight,
            accumulatedWeight = this.accumulatedWeight
        };
        this.entries.Add(e);
    }

    public object getRandom(bool doRemoveEntry=false)
    {
        double r = Random.Range(0, (float)accumulatedWeight);

        Entry e = null;
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].accumulatedWeight >= r)
            {
                e = entries[i];
                break;
            }
        }

        if (e != null)
        {
            if (doRemoveEntry)
            {
                removeEntry(e);
            }
            return e.item;
        }

        return e;
    }

    private void removeEntry(Entry e)
    {
        List<Entry> oldEntries = entries;
        entries = new List<Entry>();
        accumulatedWeight = 0;
        foreach (var i in oldEntries)
        {
            if (i == e)
            {
                continue;
            }
            addEntry(i, i.weight);
        }
    }
}