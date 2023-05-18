using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EndLevelLogic : MonoBehaviour
{
    [SerializeField] private SceneAsset _startMenu;
    [SerializeField] private AudioSource _endSound;

    [SerializeField] private float _timeToWait;

    private void Start()
    {
        _endSound.Play();

        StartCoroutine(OpenStartMenu());
    }

    private IEnumerator OpenStartMenu()
    {
        yield return new WaitForSeconds(_timeToWait);

        SceneManager.LoadScene(_startMenu.name);
    }
}
