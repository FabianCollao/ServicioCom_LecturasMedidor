using Servicio_ComunicacionesUtils;
using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacion.COM
{
    public class HebraCliente
    {
        private ClienteCom clienteCom;
        private ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
            //Solicitando datos al medidor(simulador)
            clienteCom.Escribir("Ingrese número de medidor: ");
            Int32 nroMedidor =Convert.ToInt32(clienteCom.Leer());

            clienteCom.Escribir("Ingrese Fecha: ");
            string fecha = clienteCom.Leer();

            clienteCom.Escribir("Ingrese valor de Consumo: ");
            decimal valorConsumo = Convert.ToDecimal(clienteCom.Leer());

            Lectura lectura = new Lectura()
            {
                NroMedidor = nroMedidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };
            //Bloque lock para la concurrencia al sobreescribir el mismo archivo
            lock (lecturasDAL )
            {
                lecturasDAL.AgregarLectura( lectura );
            }
            //Mensaje para que el simulador del medidor no siga esperando mensajes del servidor
            clienteCom.Escribir("adios");
            clienteCom.Desconectar();
        }
    }
}
