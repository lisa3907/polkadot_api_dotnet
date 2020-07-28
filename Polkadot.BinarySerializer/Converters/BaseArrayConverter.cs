﻿using System;
using System.IO;
using Polkadot.BinarySerializer;

namespace Polkadot.BinarySerializer.Converters
{
    public abstract class BaseArrayConverter : IBinaryConverter
    {

        public abstract void Serialize(Stream stream, object value, IBinarySerializer serializer, object[] param);

        public abstract object Deserialize(Type type, Stream stream, IBinarySerializer deserializer, object[] param);

        public object DeserializeArray(Type type, Stream stream, IBinarySerializer deserializer, object[] param, int size)
        {
            var elementType = type.GetElementType();
            if (elementType == null)
            {
                throw new ArgumentException($"Type {nameof(type.FullName)} is not an array, unable to convert.");
            }

            if (elementType == typeof(byte))
            {
                var byteArray = new byte[size];
                stream.Read(byteArray, 0, size);
                return byteArray;
            }

            var array = Array.CreateInstance(elementType, size);
            var itemConverterType = param[0] as Type;
            IBinaryConverter converter = null;
            if (itemConverterType != null)
            {
                converter = (IBinaryConverter) deserializer.CreateObject(itemConverterType);
            }

            for (int i = 0; i < size; i++)
            {
                var item = converter == null
                    ? deserializer.Deserialize(elementType, stream)
                    : converter.Deserialize(elementType, stream, deserializer, (object[]) param[1]);
                array.SetValue(item, i);
            }

            return array;
        }
    }
}