using Stacking;

namespace Base
{
    public class TransferZoneForGiving : TransferZone
    {
        public override Transfer CreateTransfer(Bag _comerBag)
        {
            return new Transfer(_comerBag, Bag);
        }

        public override int Priority() => Bag.IsFull ? 0 : int.MaxValue - Bag.Count;
    }
}