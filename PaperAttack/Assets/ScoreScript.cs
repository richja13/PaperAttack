
using System.Collections;

using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreScript : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;
 
    [Header("Settings")]
    [Range(0f, 4f)]
    public float _time = 2f;
    [Range(0f, 9f)]
    public float _distance = 2f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0.1f;
    public Vector3 StartPosition;

    public bool StartShake;
    
    private RectTransform recttranfsorm;

    public void Start()
    {
        recttranfsorm = this.gameObject.GetComponent<RectTransform>();
        StartPosition = recttranfsorm.anchoredPosition;
    }

    public void Update()
    { 
        Debug.Log(StartPosition);
        
    

        if (StartShake)
        {
            StartShake = false;
            Begin();
         
        }
        
    }

    


    private void Awake()
    {
        _startPos = StartPosition;
        //_startPos = new Vector3(502f, 459.5f,0f);
    }
 
    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }
 
    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }
 
    private IEnumerator Shake()
    {
        _timer = 0f;
 
        while (_timer < _time)
        {
            _timer += Time.deltaTime;
 
            _randomPos = StartPosition + (Random.insideUnitSphere * _distance);
 
            recttranfsorm.anchoredPosition = _randomPos;
 
            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }
 
        recttranfsorm.anchoredPosition = StartPosition;

    }
 
}

