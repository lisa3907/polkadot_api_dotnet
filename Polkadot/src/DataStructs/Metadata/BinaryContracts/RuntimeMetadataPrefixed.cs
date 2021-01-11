﻿using OneOf;
using Polkadot.BinarySerializer;
using Polkadot.BinarySerializer.Converters;
using Polkadot.DataStructs.Metadata.BinaryContracts.V12;

namespace Polkadot.DataStructs.Metadata.BinaryContracts
{
    /// <summary>
    /// From frame\metadata\src\lib.rs
    /// </summary>
    public class RuntimeMetadataPrefixed
    {
        [Serialize(0)]
        public int Reserved { get; set; }
        
        [Serialize(1)]
        [OneOfConverter]
        public OneOf<
            RuntimeMetadataDeprecated,// V0
            RuntimeMetadataDeprecated,// V1
            RuntimeMetadataDeprecated,// V2
            RuntimeMetadataDeprecated,// V3
            RuntimeMetadataDeprecated,// V4
            RuntimeMetadataDeprecated,// V5
            RuntimeMetadataDeprecated,// V6
            RuntimeMetadataDeprecated,// V7
            RuntimeMetadataDeprecated,// V8
            RuntimeMetadataDeprecated,// V9
            RuntimeMetadataDeprecated,// V10
            RuntimeMetadataDeprecated,// V11
            RuntimeMetadataV12        // V12
        > RuntimeMetadata { get; set; }
    }
}