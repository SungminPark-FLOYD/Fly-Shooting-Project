using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomCountViewer : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private TextMeshProUGUI textBoomCount;
   
    void Awake()
    {
        textBoomCount = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        textBoomCount.text = "x " + weapon.Boom;
    }
}
