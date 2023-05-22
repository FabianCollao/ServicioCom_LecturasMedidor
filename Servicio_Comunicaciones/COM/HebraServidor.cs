using ServicioComunicacion.COM;
using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using Servicio_ComunicacionesUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servicio_Comunicaciones.COM
{
    public class HebraServidor
    {
        private ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();
        private int puerto;
        public HebraServidor(int puerto)
        {
            this.puerto = puerto;
        }
        public void Ejecutar()
        {
            ServerSocket servidor = new ServerSocket(this.puerto);
            Console.WriteLine("S: Servidor iniciado en puerto {0}", this.puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando cliente....");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("ERROR, no se puede iniciar server en {0}", puerto);
            }
        }
    }
}
