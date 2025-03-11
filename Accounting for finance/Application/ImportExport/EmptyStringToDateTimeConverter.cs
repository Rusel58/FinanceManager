using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Конвертер, позволяющий интерпретировать пустую строку как DateTime.MinValue или null (для DateTime?).
    /// </summary>
    public class EmptyStringToDateTimeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(DateTime) || type == typeof(DateTime?);
        }

        public object? ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
        {
            var scalar = parser.Consume<Scalar>();
            if (string.IsNullOrWhiteSpace(scalar.Value))
            {
                return type == typeof(DateTime?) ? null : DateTime.MinValue;
            }
            return DateTime.Parse(scalar.Value);
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer nestedObjectSerializer)
        {
            emitter.Emit(new Scalar(value?.ToString() ?? ""));
        }
    }
}
