using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private AudioSource _slowMotionSound;
    [SerializeField] private GameObject _slowMotionPanel;

    [SerializeField] private float _timeDilation;
    [SerializeField] private float _duration;

    private Coroutine _currentCoroutine;

    private float _defaultTimeDilation = 1;
    private bool _isPlaying;

    public void Play()
    {
        if(_isPlaying == false)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _slowMotionSound.Play();
            _slowMotionPanel.SetActive(true);
            Time.timeScale = _timeDilation;
            ChangeAudioSourcesPitch(_timeDilation);

            _isPlaying = true;
            _currentCoroutine = StartCoroutine(StopAfterTime(_duration));
        }       
    }

    private IEnumerator StopAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        _slowMotionPanel.SetActive(false);
        Time.timeScale = _defaultTimeDilation;
        ChangeAudioSourcesPitch(_defaultTimeDilation);

        _isPlaying = false;
    }

    private void ChangeAudioSourcesPitch(float timeDilation)
    {
        foreach (var audioSource in _audioSources)
            audioSource.pitch = timeDilation;
    }
}
