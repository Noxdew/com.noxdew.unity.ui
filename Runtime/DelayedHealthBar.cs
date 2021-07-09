using UnityEngine;
using UnityEngine.UI;

namespace Noxdew.UI
{
  [RequireComponent(typeof(Slider))]
  public class DelayedHealthBar : MonoBehaviour
  {
    public Slider healthSlider;
    public Slider delayedHealthSlider;

    [SerializeField]
    private float delayInSeconds = 0.5f;

    [SerializeField]
    private bool resetDelayOnHit = false;

    [SerializeField]
    private float speed = 3f;

    private float delayTimer = 0;
    private float previousValue = 0;

    private void Awake() {
      HealthChanged(healthSlider.value);
      delayTimer = delayInSeconds;
    }

    private void OnEnable() {
      healthSlider.onValueChanged.AddListener(HealthChanged);
    }

    private void OnDisable() {
      healthSlider.onValueChanged.RemoveListener(HealthChanged);
    }

    private void Update() {
      if (delayTimer < delayInSeconds) {
        delayTimer = Mathf.Clamp(delayTimer + Time.deltaTime, 0, delayInSeconds);
      } else {
        if (delayedHealthSlider.value != healthSlider.value) {
          delayedHealthSlider.value = Mathf.Clamp(delayedHealthSlider.value - speed * Time.deltaTime, healthSlider.value, delayedHealthSlider.value);
        }
      }
    }

    private void HealthChanged(float value) {
      if (delayedHealthSlider.value < value) {
        delayedHealthSlider.value = value;
        delayTimer = delayInSeconds;
        return;
      }

      if (resetDelayOnHit || delayedHealthSlider.value == previousValue) {
        delayTimer = 0;
      }

      previousValue = value;
    }
  }
}
