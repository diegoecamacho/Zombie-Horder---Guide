using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Character;
using TMPro;
using UnityEngine;

public class HealthInformationUI : MonoBehaviour

{
    [SerializeField] private TextMeshProUGUI CurrentHealthText;
    [SerializeField] private TextMeshProUGUI TotalHealthText;

    private HealthComponent PlayerHealthComponent;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        CurrentHealthText.text = PlayerHealthComponent.Health.ToString();
        TotalHealthText.text = PlayerHealthComponent.MaxHealth.ToString();
    }
    
    private void OnPlayerHealthSet(HealthComponent healthcomponent)
    {
        PlayerHealthComponent = healthcomponent;
    }
    
    private void OnEnable()
    {
        PlayerEvents.OnPlayerHealthSet += OnPlayerHealthSet;
    }
    
    private void OnDisable()
    {
        PlayerEvents.OnPlayerHealthSet -= OnPlayerHealthSet;
    }
}
