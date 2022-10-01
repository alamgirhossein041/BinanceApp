using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceApp.JsonConverters
{
    // Source: https://stackoverflow.com/questions/57626878/the-json-value-could-not-be-converted-to-system-int32
    public class DecimalToStringConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.String => this.ReadDecimalFromString(ref reader),
                JsonTokenType.Number => reader.GetDecimal(),
                _ => throw new InvalidOperationException($"Cannot get the value of a token type '{reader.TokenType}' as a decimal.")
            };
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        private decimal ReadDecimalFromString(ref Utf8JsonReader reader)
        {
            ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
            if (Utf8Parser.TryParse(span, out decimal number, out int bytesConsumed) && span.Length == bytesConsumed)
            {
                return number;
            }

            if (decimal.TryParse(reader.GetString(), out number))
            {
                return number;
            }

            throw new InvalidOperationException($"Cannot get the value of a string token as a decimal.");
        }
    }
}