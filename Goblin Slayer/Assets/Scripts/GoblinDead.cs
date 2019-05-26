using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDead : MonoBehaviour, IDead
{
    public int rageAmount = 2;
    public RageDoll rageDoll;
    private PlayerBaseSounds sounds;
    private static Transform _rageDollPool = null;
    private static Rage _playerRage = null;
    
    private void Start()
    {
        if (_rageDollPool == null)
            _rageDollPool = GameObject.Find(("RageDollPool")).transform;
        sounds = GetComponentInChildren<PlayerBaseSounds>();
        if(_playerRage == null)
            _playerRage = GameObject.Find("Player").GetComponent<Rage>();
    }

    public void OnDead()
    {
        sounds.PlayEffect(sounds.dead);
        GetComponent<GoblinState>().GoblinIsDead();
        GameplayManager.OnEnemyDead();
        _playerRage.AddRage(rageAmount);
        //Instantiate(rageDoll, transform.position, transform.rotation, _rageDollPool);
        Destroy(gameObject);
    }
}
