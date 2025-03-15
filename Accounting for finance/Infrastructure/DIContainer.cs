using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accounting_for_finance.infrastructure
{
    /// <summary>
    /// Простейший DI-контейнер для регистрации и разрешения зависимостей с поддержкой
    /// автоматического разрешения зависимостей через конструктор.
    /// </summary>
    public static class DIContainer
    {
        private static readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        /// <summary>
        /// Регистрирует фабричный метод для создания экземпляра заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип, для которого регистрируется фабрика.</typeparam>
        /// <param name="factory">Функция, создающая экземпляр типа T.</param>
        public static void Register<T>(Func<T> factory)
        {
            _registrations[typeof(T)] = () => factory();
        }

        /// <summary>
        /// Регистрирует конкретный экземпляр для заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип, для которого регистрируется экземпляр.</typeparam>
        /// <param name="instance">Созданный экземпляр типа T.</param>
        public static void RegisterInstance<T>(T instance)
        {
            _registrations[typeof(T)] = () => instance;
        }

        /// <summary>
        /// Получает экземпляр для заданного типа.
        /// Если тип не зарегистрирован, контейнер попытается автоматически создать его, используя конструктор.
        /// </summary>
        /// <typeparam name="T">Тип, который требуется разрешить.</typeparam>
        /// <returns>Экземпляр типа T.</returns>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Рекурсивно разрешает зависимости для заданного типа.
        /// </summary>
        /// <param name="type">Тип, который требуется разрешить.</param>
        /// <returns>Экземпляр указанного типа.</returns>
        private static object Resolve(Type type)
        {
            // Если тип зарегистрирован, используем фабричный метод.
            if (_registrations.TryGetValue(type, out var factory))
            {
                return factory();
            }

            // Если тип не зарегистрирован, пытаемся создать его автоматически через конструктор.
            // Выбираем конструктор с максимальным количеством параметров.
            ConstructorInfo constructor = type.GetConstructors()
                                              .OrderByDescending(c => c.GetParameters().Length)
                                              .FirstOrDefault();
            if (constructor == null)
            {
                throw new Exception($"Невозможно найти публичный конструктор для типа {type.FullName}");
            }

            // Разрешаем все параметры конструктора.
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] parameterInstances = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parameterInstances[i] = Resolve(parameters[i].ParameterType);
            }

            // Создаем экземпляр типа.
            object instance = constructor.Invoke(parameterInstances);
            return instance;
        }
    }
}
