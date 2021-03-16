using UnityEngine;
using UnityEngine.Tilemaps;


public class Signaling : MonoBehaviour
{
    [SerializeField] private Tilemap _interior;
    [SerializeField] private Color _targetColor;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _smoothingDuration;

    private Color _startColor;
    private bool _isActive = false;
    private float _lastTime = 0;

    private float NormalizedTime => _lastTime / _smoothingDuration;
   
    private float LastTime
    {
        get
        {
            return _lastTime;
        }
        set
        {
            if (value < 0)
            {
                _lastTime = 0;
            }
            else if (value > _smoothingDuration)
            {
                _lastTime = _smoothingDuration;
            }
            else
            {
                _lastTime = value;
            }
        }
    }

    private void Start()
    {
        _startColor = _interior.color;
    }

    private void Update()
    {
        LastTime = _isActive ? LastTime += Time.deltaTime : LastTime -= Time.deltaTime;
        PlaytAlarm();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isActive = false;
    }

    private void BlinkAlarm()
    {
        if (_isActive)
        {
            float normalizedTime = Mathf.PingPong(Time.time, 1f);
            _interior.color = Color.Lerp(_startColor, _targetColor, normalizedTime);
        }
        else
        {
            _interior.color = Color.Lerp(_startColor, _interior.color, NormalizedTime);
        }
    }

    private void PlayAlarmSound()
    {
        _sound.volume = NormalizedTime;
        if (_isActive)
        {
            if (!_sound.isPlaying)
            {
                _sound.Play();
            }
        }
        else
        {
            if (NormalizedTime == 0)
            {
                _sound.Stop();
            }
        }
    }

    private void PlaytAlarm()
    {
        PlayAlarmSound();
        BlinkAlarm();
    }
}
