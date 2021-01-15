using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class PlayerEvents 
{
    public delegate void PlayerHealthSetEvent(HealthComponent healthComponent);
    public static event PlayerHealthSetEvent OnPlayerHealthSet;
    public static void Invoke_OnPlayerHealthSet(HealthComponent healthComponent)
    {
        OnPlayerHealthSet?.Invoke(healthComponent);
    }
    
}
