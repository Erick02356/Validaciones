using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PropietarioService
    {
        private readonly PropietarioRepository _PropietarioRepository;

        public PropietarioService()
        {
            _PropietarioRepository = new PropietarioRepository();
        }
        //COSULTAR LOS PROPIETARIOS EN LA BASE DE DATOS 

        public List<Propietario> ObtenerTodos()
        {
            return _PropietarioRepository.ObtenerTodos();
        }

        public  void Crear (Propietario propietario)
        {
            _PropietarioRepository.Crear(propietario);
        }

        public void Editar(Propietario propietario)
        {
            _PropietarioRepository.Editar(propietario);
        }

        // Método para Eliminar un propietario
        public void Eliminar(int id)
        {
            _PropietarioRepository.Eliminar(id);
        }

    }
}
