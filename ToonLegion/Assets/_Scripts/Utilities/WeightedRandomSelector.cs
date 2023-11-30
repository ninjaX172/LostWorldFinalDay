using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WeightedRandomSelector<T>
{
    private List<T> objects = new List<T>();
    private List<int> weights = new List<int>();

    public int Count => objects.Count;

    public void AddMany(List<T> objs, List<int> weights)
    {
        if (objs.Count != weights.Count)
            throw new System.Exception("The number of objects and weights must be the same.");
        for (var i = 0; i < objs.Count; i++)
        {
            Add(objs[i], weights[i]);
        }
    }

    public void Add(T obj, int weight)
    {
        objects.Add(obj);
        weights.Add(weight);
    }

    public void Remove(T obj)
    {
        var index = objects.IndexOf(obj);
        objects.RemoveAt(index);
        weights.RemoveAt(index);
    }

    public void Update(T obj, int weight)
    {
        var index = objects.IndexOf(obj);
        weights[index] = weight;
    }

    public int GetObjectWeight(T obj)
    {
        var index = objects.IndexOf(obj);
        return weights[index];
    }

    public T GetRandomWeightedItem()
    {
        if (objects.Count != weights.Count)
            throw new System.Exception("The number of objects and weights must be the same.");
        var totalWeight = weights.Sum();
        var randomValue = Random.Range(0, totalWeight);
        var weightSum = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            weightSum += weights[i];
            if (randomValue < weightSum)
            {
                return objects[i];
            }
        }

        throw new System.Exception("Should never get here.");
    }

    public bool Contains(T obj)
    {
        if (objects.Contains(obj)){
            return true;
        }
        return false;
    }


}

public class PercentageRandomSelector<T>
{
    private List<T> objects = new List<T>();
    private List<float> weights = new List<float>();

    public int Count => objects.Count;

    public void AddMany(List<T> objs, List<float> weights)
    {
        if (objs.Count != weights.Count)
            throw new System.Exception("The number of objects and weights must be the same.");
        for (var i = 0; i < objs.Count; i++)
        {
            Add(objs[i], weights[i]);
        }
    }

    public void Add(T obj, float weight)
    {
        objects.Add(obj);
        weights.Add(weight);
    }

    public List<T> GetRandomChoices(int numberOfChoices)
    {
        var choices = new List<T>();
        for (int i = 0; i < numberOfChoices; i++)
        {
            choices.Add(GetRandomChoice());
        }
        return choices;
    }

    public T GetRandomChoice()
    {
        double value = Random.Range(0f, 1f);
        double cumulative = 0;

        for (int i = 0; i < objects.Count; i++)
        {
            cumulative += weights[i];
            if (value < cumulative)
            {
                return objects[i];
            }
        }
        return objects.Last();
    }
}