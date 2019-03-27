﻿using UnityEngine;
[RequireComponent(typeof(Walker))]
///<summary>
/// This component makes the agent follow the target.
/// Ensures that we're always facing the target.
///</summary>
public class Follower_AI : MonoBehaviour {

    // The object to follow
    public Transform target;

    private Walker walker;

    private void Start()
    {
        walker = GetComponent<Walker>();
    }

    /// <summary>
    /// Makes this agent walk to the target
    /// </summary>
    public void Follow()
    {
        Walker.WalkDirection dir = (target.position - transform.position).x > 0 ? Walker.WalkDirection.RIGHT : Walker.WalkDirection.LEFT;
        walker.Walk(dir);
    }
    /// <summary>
    /// Makes this agent stop following the targer
    /// </summary>
    public void StopFollowing()
    {
        walker.Stop();
    }
}
