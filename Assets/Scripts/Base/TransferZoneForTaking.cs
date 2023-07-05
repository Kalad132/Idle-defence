using Stacking;

namespace Base
{
    public class TransferZoneForTaking : TransferZone
    {
        public override Transfer CreateTransfer(Bag _comerBag)
        {
            return new Transfer(Bag, _comerBag);
        }

        public override int Priority() => Bag.Count;
    }
}