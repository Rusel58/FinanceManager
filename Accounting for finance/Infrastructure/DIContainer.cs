using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.infrastructure
{
    /// <summary>
    /// Простейший DI-контейнер для регистрации и разрешения зависимостей.
    /// </summary>
    public static class DIContainer
    {
        private static readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        /// <summary>
        /// Регистрирует фабричный метод для создания экземпляра заданного типа.
        /// </summary>
        public static void Register<T>(Func<T> factory)
        {
            _registrations[typeof(T)] = () => factory();
        }

        /// <summary>
        /// Получает экземпляр для заданного типа.
        /// </summary>
        public static T Resolve<T>()
        {
            if (_registrations.TryGetValue(typeof(T), out var factory))
            {
                return (T)factory();
            }
            throw new Exception($"Тип {typeof(T).Name} не зарегистрирован в DI-контейнере.");
        }

        /// <summary>
        /// Регистрирует конкретный экземпляр для заданного типа.
        /// </summary>
        public static void RegisterInstance<T>(T instance)
        {
            _registrations[typeof(T)] = () => instance;
        }
    }
}
