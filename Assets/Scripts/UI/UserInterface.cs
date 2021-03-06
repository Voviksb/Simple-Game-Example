using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _enemiesText;
    [SerializeField] private TextMeshProUGUI _timeTillNextWave;
    [SerializeField] private TextMeshProUGUI _raidTimeLeft;
    [SerializeField] private EnemiesSpawner _levelSpawner;
    [SerializeField] private WavesHandler _wavesHandler;
    [SerializeField] private Raid _raid;


    private void Awake()
    {
        _wavesHandler.OnWavesChangedEvent += OnWavesChanged;
        _levelSpawner.OnEnemiesCountChangeEvent += OnEnemiesCountChange;
        _raid.OnTimerCountChangedEvent += OnRaidTimerChanged;
    }

    protected virtual void OnEnemiesCountChange(int enemiesCount)
    {
        _enemiesText.text = "Enemies left: " + enemiesCount;
    }

    private void OnWavesChanged(int waveNumber)
    {
        _waveText.text = "Wave: " + waveNumber;
    }

    public void OnRaidTimerChanged(string str)
    {
        _raidTimeLeft.text = str;
    }

    public void SwitchTimer()
    {
        _timeTillNextWave.gameObject.active = !_timeTillNextWave.gameObject.active;
    }

    public void TimeTillNextWave(int secondsLeft)
    {
        _timeTillNextWave.text = secondsLeft.ToString();
    }

    private void OnDisable()
    {
        _wavesHandler.OnWavesChangedEvent -= OnWavesChanged;
        _levelSpawner.OnEnemiesCountChangeEvent -= OnEnemiesCountChange;
        _raid.OnTimerCountChangedEvent -= OnRaidTimerChanged;
    }
}
