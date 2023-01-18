using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AutomonousAgent : Agent
{
    public Perseption flockPerception;
    public ObstaclePerception obstacleAvoidance;
    public AutonomousAgentData data;
    
    public float wanderAngle { get; set; } = 0;

    void Update()
    {
        // seek/flee
        var gameObjects = perseption.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (gameObjects.Length > 0) 
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);
        }

        // flocking
        gameObjects = flockPerception.GetGameObjects();
        if(gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this, gameObjects) * data.cohesionWeight);
            movement.ApplyForce(Steering.Separation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment(this, gameObjects) * data.alignmentWeight);
        }

        // obstacle avoidance 
        if (obstacleAvoidance.IsObstacleInFront())
        {
            Vector3 direction = obstacleAvoidance.GetOpenDirection();
            movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
        }

        // wander 
        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20)); 
    }
}
