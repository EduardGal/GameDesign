using UnityEngine;
using System.Collections;
 
 public class Destroy : MonoBehaviour
 {
     public float lifetime;
 
     void Start ()
     {
         Destroy (gameObject, lifetime);
     }
 }