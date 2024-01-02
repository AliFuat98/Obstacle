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
      xCurrentHealth = value;
      OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs {
        health = value,
      });
    }
  }


  [SerializeField] float invulnerabilityTime = 1f;
  bool isInvulnerable = false;

  public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
  public class OnHealthChangedEventArgs : EventArgs {
    public int health;
  }

  public event EventHandler OnDeath; // Event for death

  void Start() {
    currentHealth = maxHealth;
  }

  public void TakeDamage(int damage) {
    if (isInvulnerable) return;

    currentHealth -= damage;

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
    OnDeath?.Invoke(this,EventArgs.Empty);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public bool IsInvulnerabile() {
    return isInvulnerable;
  }
}
