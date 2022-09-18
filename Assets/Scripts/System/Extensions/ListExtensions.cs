using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    private static System.Random rng = new System.Random(); 
    
    public static List<T> Clone<T>(this List<T> list) {
        return list.ToArray().ToList();
    }
    
    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
