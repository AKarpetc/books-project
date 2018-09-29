using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace book_editor.web.App_Start
{
    public class ImplementationRegistration
    {
        private const string PROJECTS_PREFIX = "book-editor";
        private List<Assembly> GetAssembly()
        {
            var collection = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(PROJECTS_PREFIX)).ToList();
            return collection;
        }
        private Type[] GetTypes(RegisterAssemblyType assemblyType)
        {
            List<Type> types = new List<Type>();
            if (assemblyType == RegisterAssemblyType.All)
            {
                foreach (var ass in GetAssembly())
                {
                    types.AddRange(ass.GetTypes());
                }
            }
            else if (assemblyType == RegisterAssemblyType.FromGeneric)
            {
                Assembly assembly = _parentType.Assembly;
                types.AddRange(assembly.GetTypes());

            }
            else if (assemblyType == RegisterAssemblyType.Curent)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                types.AddRange(assembly.GetTypes());

            }
            if (assemblyType == RegisterAssemblyType.FromGeneric)
            {
                foreach (var ass in _listAssembly)
                {
                    types.AddRange(ass.GetTypes());
                }
            }

            return types.ToArray();
        }
        private Type _parentType;
        private List<Assembly> _listAssembly;
        private readonly ContainerBuilder _builder;
        /// <summary>
        /// Метод регистрирующий все классы в обобщении где обобщенный класс имеет одного родителя
        /// </summary>
        /// <typeparam name="T">Родитель</typeparam>
        /// <param name="service">Сервис принимающий обобщение</param>
        /// <param name="iservice">Интерфейс</param>
        /// <param name="assemblyType">Тип поиска наследников</param>
        /// <param name="listAssembly">список наследников</param>
        public void RegisterGenericImplementation<T>(Type service, Type iservice, RegisterAssemblyType assemblyType = RegisterAssemblyType.FromGeneric, List<Assembly> listAssembly = null) where T : class
        {
            _parentType = typeof(T);
            _listAssembly = listAssembly ?? new List<Assembly>();
            var types = GetTypes(assemblyType);
            var nededType = types.Where(t =>
                                        _parentType.IsAssignableFrom(t) &&
                                        _parentType.Name != t.Name
                                        ).ToList();
            nededType.ForEach(x =>
            {
                var comonSystemDictService = service;
                var iсomonSystemDictService = iservice;
                Type[] typeArgs = { x };
                var comonSystemDictServiceDynamic = comonSystemDictService.MakeGenericType(typeArgs);
                var iсomonSystemDictServiceDynamic = iсomonSystemDictService.MakeGenericType(typeArgs);
                _builder.RegisterType(comonSystemDictServiceDynamic).As(iсomonSystemDictServiceDynamic).InstancePerLifetimeScope();
            });

        }
        /// <summary>
        /// Метод регистрирующий все реализации/наследников родительского класса
        /// </summary>
        /// <typeparam name="T">Родитель</typeparam>
        /// <param name="assemblyType">Тип поиска наследников</param>
        /// <param name="listAssembly">список наследников</param>
        public void RegisterImplementation<T>(RegisterAssemblyType assemblyType = RegisterAssemblyType.FromGeneric, List<Assembly> listAssembly = null)
        {
            _parentType = typeof(T);
            _listAssembly = listAssembly ?? new List<Assembly>();
            var types = GetTypes(assemblyType);
            var nededType = types.Where(t =>
              _parentType.IsAssignableFrom(t)
              && _parentType.Name != t.Name
             ).ToList();
            nededType.ForEach(x =>
            {
                _builder.RegisterType(x);
            });

        }

        public ImplementationRegistration(ContainerBuilder builder)
        {
            _builder = builder;
        }

    }
    /// <summary>
    /// Искать наследников в
    /// </summary>
    public enum RegisterAssemblyType
    {
        /// <summary>
        /// во всех сборка проекта
        /// </summary>
        All,
        /// <summary>
        /// Только в текущей сборки
        /// </summary>
        Curent,
        /// <summary>
        /// Только сборка где находится родитель обобщений
        /// </summary>
        FromGeneric,
        /// <summary>
        /// с передаваемого списка
        /// </summary>
        FromList,
    }
}