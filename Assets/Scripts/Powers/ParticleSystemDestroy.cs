﻿ using UnityEngine;
 using System.Collections;
 
 //https://answers.unity.com/questions/219609/auto-destroying-particle-system.html
 
 public class ParticleSystemDestroy : MonoBehaviour 
 {
     private ParticleSystem ps;
 
 
     public void Start() 
     {
         ps = GetComponent<ParticleSystem>();
     }
 
     public void Update() 
     {
         if(ps)
         {
             if(!ps.IsAlive())
             {
                 Destroy(gameObject);
             }
         }
     }
 }