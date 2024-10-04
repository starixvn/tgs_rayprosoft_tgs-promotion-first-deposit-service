using NATS.Client.Core;

namespace Tgs.Promotion.First.Deposit.Service.NatsSerializers;

public class ProtobufSerializerRegistry : INatsSerializerRegistry
{
    public static readonly ProtobufSerializerRegistry Default = new();

    public INatsSerialize<T> GetSerializer<T>()
    {
        return ProtobufSerializer<T>.Default;
    }

    public INatsDeserialize<T> GetDeserializer<T>()
    {
        return ProtobufSerializer<T>.Default;
    }
}