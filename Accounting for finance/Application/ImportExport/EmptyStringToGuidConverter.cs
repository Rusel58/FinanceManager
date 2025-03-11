using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Конвертер, позволяющий интерпретировать пустую строку как Guid.Empty или null (для Guid?).
    /// </summary>
    public class EmptyStringToGuidConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(Guid) || type == typeof(Guid?);
        }

        public object? ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
        {
            var scalar = parser.Consume<Scalar>();
            if (string.IsNullOrWhiteSpace(scalar.Value))
            {
                return type == typeof(Guid?) ? null : Guid.Empty;
            }
            return Guid.Parse(scalar.Value);
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer nestedObjectSerializer)
        {
            emitter.Emit(new Scalar(value?.ToString() ?? ""));
        }
    }
}
