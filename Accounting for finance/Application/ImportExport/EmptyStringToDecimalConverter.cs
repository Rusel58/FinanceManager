using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
     /// Конвертер, позволяющий интерпретировать пустую строку как 0 (для decimal) или null (для decimal?),
     /// а также обрабатывать десятичные числа, где в качестве разделителя используется запятая.
     /// </summary>
    public class EmptyStringToDecimalConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(decimal) || type == typeof(decimal?);
        }

        public object? ReadYaml(IParser parser, Type type, ObjectDeserializer nestedObjectDeserializer)
        {
            var scalar = parser.Consume<Scalar>();
            if (string.IsNullOrWhiteSpace(scalar.Value))
            {
                return type == typeof(decimal?) ? null : 0m;
            }

            // Заменяем запятую на точку, если она используется в качестве десятичного разделителя
            string value = scalar.Value.Replace(",", ".");
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
            throw new Exception($"Невозможно преобразовать значение '{scalar.Value}' в decimal.");
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer nestedObjectSerializer)
        {
            string strValue = Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
            emitter.Emit(new Scalar(strValue));
        }
    }
}
