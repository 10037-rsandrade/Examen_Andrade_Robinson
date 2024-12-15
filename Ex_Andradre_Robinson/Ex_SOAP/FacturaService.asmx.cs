using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entities;
using BLL;

namespace Ex_SOAP
{
    /// <summary>
    /// Descripción breve de FacturaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class FacturaService : System.Web.Services.WebService
    {
        private FacturaLogic _logic = new FacturaLogic();

        [WebMethod]
        public List<SerializableFactura> GetAllFacturas()
        {
            var facturas = _logic.RetrieveAll();
            return facturas.ConvertAll(f => new SerializableFactura(f));
        }

        [WebMethod]
        public SerializableFactura GetFacturaById(int id)
        {
            return new SerializableFactura(_logic.RetrieveById(id));
        }

        [WebMethod]
        public void CreateFactura(SerializableFactura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException("El objeto factura no puede ser nulo.");
            }

            if (factura.ClienteID <= 0 || factura.Total <= 0 || string.IsNullOrEmpty(factura.Estado))
            {
                throw new ArgumentException("Los campos ClienteID, Total y Estado son obligatorios y deben ser válidos.");
            }

            _logic.Create(factura.ToFactura());
        }

        [WebMethod]
        public void UpdateFactura(SerializableFactura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException("El objeto factura no puede ser nulo.");
            }

            if (factura.FacturaID <= 0)
            {
                throw new ArgumentException("El ID de la factura no es válido.");
            }

            if (factura.ClienteID <= 0 || factura.Total <= 0 || string.IsNullOrEmpty(factura.Estado))
            {
                throw new ArgumentException("Los campos ClienteID, Total y Estado son obligatorios y deben ser válidos.");
            }

            _logic.Update(factura.ToFactura());
        }

        [WebMethod]
        public void DeleteFactura(int id)
        {
            _logic.Delete(id);
        }
    }

    public class SerializableFactura
    {
        public int FacturaID { get; set; }
        public int ClienteID { get; set; }
        public DateTime? FechaEmision { get; set; } // Change DateTime to DateTime?

        public decimal Total { get; set; }
        public string Estado { get; set; }

        public SerializableFactura() { }

        public SerializableFactura(Facturas factura)
        {
            FacturaID = factura.FacturaID;
            ClienteID = factura.ClienteID;
            FechaEmision = factura.FechaEmision; // No change needed here
            Total = factura.Total;
            Estado = factura.Estado;
        }

        public Facturas ToFactura()
        {
            return new Facturas
            {
                FacturaID = this.FacturaID,
                ClienteID = this.ClienteID,
                FechaEmision = this.FechaEmision, // No change needed here
                Total = this.Total,
                Estado = this.Estado
            };
        }
    }
}
