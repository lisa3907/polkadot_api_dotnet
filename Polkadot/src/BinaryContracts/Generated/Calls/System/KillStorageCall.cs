using Polkadot.BinarySerializer;
using Polkadot.DataStructs;
using Polkadot.BinarySerializer.Converters;
using Polkadot.BinaryContracts.Nft;
using System.Numerics;

namespace Polkadot.BinaryContracts.Calls.System
{
    public class KillStorageCall : IExtrinsicCall
    {
        // Rust type Vec<Key>
        [Serialize(0)]
        [PrefixedArrayConverter]
        public Key[] Keys { get; set; }



        public KillStorageCall() { }
        public KillStorageCall(Key[] @keys)
        {
            this.Keys = @keys;
        }

    }
}