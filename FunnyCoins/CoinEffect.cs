using LabApi.Features.Wrappers;

namespace FunnyCoins.Effects
{
    public interface ICoinEffect
    {
        string Id { get; }
        bool IsGood { get; }
        int DefaultWeight { get; }

        string DefaultMessage { get; }

        void Execute(Player player);
    }
}