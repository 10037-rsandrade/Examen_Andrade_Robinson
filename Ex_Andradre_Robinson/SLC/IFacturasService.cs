using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IHorarioService
    {
        // Crear una factura
        Facturas CreateFactura(Facturas facturas);

        // Eliminar un factura por ID
        bool Delete(int id);

        // Obtener todos las facturas
        List<Facturas> GetAll();

        // Obtener una factura por ID
        Facturas GetById(int id);

        // Actualizar una factura
        bool UpdateFactura(Facturas horarios);
    }
}
