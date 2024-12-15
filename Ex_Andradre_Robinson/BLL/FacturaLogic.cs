using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class FacturaLogic
    {
        public Facturas Create(Facturas factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException("El objeto factura no puede ser nulo.");
            }

            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Validar si ya existe una factura con el mismo ClienteID y FechaEmision
                Facturas _result = repository.Retrieve<Facturas>(f => f.ClienteID == factura.ClienteID && f.FechaEmision == factura.FechaEmision);

                if (_result != null)
                {
                    throw new Exception($"Ya existe una factura para el cliente con ID {factura.ClienteID} en la fecha {factura.FechaEmision}.");
                }

                return repository.Create(factura);
            }
        }

        public Facturas RetrieveById(int id)
        {
            Facturas _factura = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                _factura = repository.Retrieve<Facturas>(f => f.FacturaID == id);
            }
            return _factura;
        }

        public bool Update(Facturas factura)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                var existingFactura = repository.Retrieve<Facturas>(f => f.FacturaID == factura.FacturaID);

                if (existingFactura == null)
                {
                    throw new Exception("La factura no existe.");
                }

                existingFactura.ClienteID = factura.ClienteID;
                existingFactura.FechaEmision = factura.FechaEmision;
                existingFactura.Total = factura.Total;
                existingFactura.Estado = factura.Estado;

                return repository.Update(existingFactura);
            }
        }

        public bool Delete(int id)
        {
            bool _delete = false;
            var _factura = RetrieveById(id);
            if (_factura != null)
            {
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    _delete = repository.Delete(_factura);
                }
            }
            return _delete;
        }

        public List<Facturas> RetrieveAll()
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                var facturas = repository.Filter<Facturas>(f => f.FacturaID > 0).ToList();
                return facturas;
            }
        }
    }
}
