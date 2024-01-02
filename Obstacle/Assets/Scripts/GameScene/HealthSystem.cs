using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {
  [SerializeField] int maxHealth = 100;
  int xCurrentHealth;

  int currentHealth {
    get {
      return xCurrentHealth;
    }
    set {
      if (currentHealth != value) {
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs {
          health = value,
        });
      }

      xCurrentHealth = value;
    }
  }

  [SerializeField] float invulnerabilityTime = 1f;

  bool xIsInvulnerable = false;

  bool isInvulnerable {
    get {
      return xIsInvulnerable;
    }
    set {
      if (xIsInvulnerable != value) {
        OnIsInvulnerableChanged?.Invoke(this, new OnIsInvulnerableChangedEventArgs {
          isInvulnerable = value,
        });
      }

      xIsInvulnerable = value;
    }
  }

  public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;

  public class OnHealthChangedEventArgs : EventArgs {
    public int health;
  }

  public event EventHandler<OnIsInvulnerableChangedEventArgs> OnIsInvulnerableChanged;

  public class OnIsInvulnerableChangedEventArgs : EventArgs {
    public bool isInvulnerable;
  }

  public event EventHandler OnDeath; // Event for death

  void Start() {
    currentHealth = maxHealth;
  }

  public void TakeDamage(int damage) {
    if (isInvulnerable) return;

    currentHealth -= damage;
    SoundManager.Instance.PlayerTakeDamage();

    if (currentHealth <= 0) {
      Die();
    } else {
      StartCoroutine(Invulnerability());
    }
  }

  public void Heal(int healingAmount) {
    var newHealt = currentHealth + healingAmount;
    currentHealth = Mathf.Min(newHealt, maxHealth);
  }

  private IEnumerator Invulnerability() {
    isInvulnerable = true;
    yield return new WaitForSeconds(invulnerabilityTime);
    isInvulnerable = false;
  }

  private void Die() {
    OnDeath?.Invoke(this, EventArgs.Empty);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public bool IsInvulnerabile() {
    return isInvulnerable;
  }
}