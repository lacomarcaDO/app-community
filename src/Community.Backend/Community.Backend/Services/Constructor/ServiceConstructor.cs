using Community.Backend.Database.Repositories.Constructor;
using Community.Backend.Services.Base;
using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Community.Backend.Services.Constructor
{
    /// <summary>
    /// interfaz para el manejo global de constructores de servicios de consultas en base de datos
    /// </summary>
    public interface IServiceConstructor
    {
        /// <summary>
        /// Obtiene una instancia del servicio que se solicite
        /// </summary>
        /// <typeparam name="Tservice">interface del servicio que implementa IServicioBase<Tmodel> del cual se desea obtener una nueva instancia del mismo</typeparam>
        /// <typeparam name="Tmodel">modelo del cual se ha creado el servicio a inicializar</typeparam>
        /// <returns>Retorna el servicio del cual se ha hecho solicitud de una nueva instancia</returns>
        Tservice GetServicio<Tservice, Tmodel>() where Tservice : IBaseService<Tmodel> where Tmodel : class, IBaseModel;
    }
    public class ServiceConstructor : IServiceConstructor
    {
        public IRepositoryConstructor Constructor { get; protected set; }
        /// <summary>
        /// Constructor de clase constructora de servicios
        /// </summary>
        /// <param name="_constructor">Instancia de la implementacion del constructor de instancias a repositorio de forma global global</param>
        public ServiceConstructor(IRepositoryConstructor _constructor)
        {
            Constructor = _constructor;
        }

        Tservice IServiceConstructor.GetServicio<Tservice, Tmodel>()
        {
            Type tConcreteService = null;
            Assembly.GetExecutingAssembly().GetTypes().ToList().ForEach(t =>
            {
                if (typeof(Tservice).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                {
                    tConcreteService = t;
                }
            });
            return (Tservice)Activator.CreateInstance(tConcreteService, Constructor);
        }
    }
}