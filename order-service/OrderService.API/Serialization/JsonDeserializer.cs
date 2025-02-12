using Confluent.Kafka;
using System.Text.Json;

namespace OrderService.API.Serialization;



public class JsonDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)=>
        JsonSerializer.Deserialize<T>(data);
    
}