using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carColor : MonoBehaviour
{
    // Kode refactored herfra:
    // https://answers.unity.com/questions/1116643/set-random-material-from-folder.html#answer-1116652

    // Start is called before the first frame update
    void Start()
    {
        //allow Resource.LoadAll to just return a list of objects (filtered to only have materials in)
        UnityEngine.Object[] carColors = Resources.LoadAll("carMaterials", typeof(Material));
        Debug.Log(carColors.Length);

        
         //Randomly choose material for car
         GetComponent<Renderer>().material = (Material)carColors[UnityEngine.Random.Range(0, carColors.Length)];
    }
}
