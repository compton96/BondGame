using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HasCooldown
{
    int Id { get; }

    float CooldownDuration
    { get; }
}
