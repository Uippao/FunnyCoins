using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace FunnyCoins.Effects
{
    public interface ICoinEffect
    {
        string Id { get; }
        bool IsGood { get; }
        int DefaultWeight { get; }

        string DefaultMessage { get; }

        IEnumerable<EffectMessageDefinition> DefaultMessages { get; }

        bool HandlesOwnMessage { get; }

        void Execute(Player player);
    }

    public class EffectMessageDefinition
    {
        public string Key { get; }
        public string DefaultTemplate { get; }
        public float DefaultDuration { get; }

        public EffectMessageDefinition(string key, string defaultTemplate, float defaultDuration = 4f)
        {
            Key = key;
            DefaultTemplate = defaultTemplate;
            DefaultDuration = defaultDuration;
        }
    }
    
    public abstract class SimpleCoinEffect : ICoinEffect
    {
        public abstract string Id { get; }
        public abstract bool IsGood { get; }
        public abstract int DefaultWeight { get; }
        public abstract string DefaultMessage { get; }

        public virtual float DefaultMessageDuration => 4f;

        public bool HandlesOwnMessage => false;

        public IEnumerable<EffectMessageDefinition> DefaultMessages =>
            new[]
            {
                new EffectMessageDefinition("default", DefaultMessage, DefaultMessageDuration)
            };

        public abstract void Execute(Player player);
    }
}