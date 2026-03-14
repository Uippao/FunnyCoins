using System;
using System.Collections.Generic;
using CustomPlayerEffects;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
using LabApi.Features.Wrappers;

namespace FunnyCoins
{
    public class Utils
    {
        public static readonly IReadOnlyList<Action<Player>> BadStatusEffects = new Action<Player>[]
        {
            p => p.EnableEffect<Flashed>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 10f)
            ),
            p => p.EnableEffect<Deafened>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Burned>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Hemorrhage>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Slowness>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<CardiacArrest>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Poisoned>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Sinkhole>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Hypothermia>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Disabled>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Blurred>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            ),
            p => p.EnableEffect<Asphyxiated>(
                (byte)UnityEngine.Random.Range(1, 4),
                UnityEngine.Random.Range(5f, 30f)
            )
        };
    }
}