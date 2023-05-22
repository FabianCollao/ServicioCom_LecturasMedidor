using SimuladorMedidorElectUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Simulador_Medidor_Elect
{
    public class Program
    {

        //Colores de la aplicación
        static ConsoleColor colorClienteNombre = ConsoleColor.DarkGreen;
        static ConsoleColor colorServerMsg = ConsoleColor.Cyan;
        static ConsoleColor colorClienteMsg = ConsoleColor.Green;
        static ConsoleColor colorApp = ConsoleColor.Blue;
        static ConsoleColor colorSucces = ConsoleColor.Green;
        static ConsoleColor colorError = ConsoleColor.Red;
        static ConsoleColor colorAlerta = ConsoleColor.Yellow;

        //Funcion para personalizar color del texto de la consola, también el salto de linea.
        static void ImprimirColor(string msg, ConsoleColor color, bool saltoLinea)
        {
            Console.ForegroundColor = color;
            if (saltoLinea)
            { Console.WriteLine(msg); }
            else
            { Console.Write(msg); }
            Console.ResetColor();
        }
        //Función de intercambio de datos por sockets
        static void generarComunicacion(ClienteSocket clienteSocket)
        {
            //Variables solo de esta conexión
            string msg_server = "";
            string msg_client = "";
            bool seguir;
            do
            {
                //Obtenemos el mensaje del servidor
                msg_server = clienteSocket.Leer();
                //Validacion para terminar la conexion 
                
                if (msg_server!="adios" && clienteSocket.conectado())
                {
                    seguir = true;  
                    ImprimirColor(msg_server, colorApp, true);  //mostrando mensaje del servidor
                    ImprimirColor("Tú: ", colorClienteNombre, false);
                    Console.ForegroundColor = colorClienteMsg;
                    //Ingresando datos
                    msg_client = Console.ReadLine().Trim(); 
                    Console.ResetColor();
                    //Enviamos la respuesta al servidor
                    clienteSocket.Escribir(msg_client);
                }
                else
                {
                    ImprimirColor("**Desconectado del servidor**", colorError, true);
                    clienteSocket.Desconectar();
                    seguir = false;
                }
            } while (seguir);

        }
        static void Main(string[] args)
        {
            //Variables para conectarse al servidor
            int puerto;
            string servidor="";


            //Espera infinitamente una conexión con algún cliente
            while (true)
            {
                //Ingreso de ip y puerto del servidor para conectarse
                ImprimirColor("Ingresa la ip del servidor :", colorApp, false);
                servidor = Console.ReadLine().Trim();
                ImprimirColor("Ingresa el puerto :", colorApp, false);
                try
                {
                    puerto = Convert.ToInt32(Console.ReadLine().Trim());
                }
                catch (Exception ex)
                {
                    puerto = 3000;
                    ImprimirColor("Usando por predeterminado el puerto "+puerto,colorAlerta,true);
                }
                ImprimirColor("Conectando a servidor ", colorApp, false);
                ImprimirColor(servidor + ":" + Convert.ToString(puerto), colorServerMsg, true);

                //Creamos el socket del cliente para conectarnos al servidor
                ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

                //Validamos si podemos iniciar una conexión
                if (clienteSocket.Conectar())
                {
                    //Intercambiamos datos por medio de clientesocket en la función de generarComunicacion
                    ImprimirColor("**Conexión exitosa**", colorSucces, true);
                    generarComunicacion(clienteSocket);
                }
                else
                {   
                    //Error de conexión
                    ImprimirColor("No se pudo conectar", colorError, true);
                    ImprimirColor("Intenta de nuevo ;)", colorAlerta, true);
                }
            }

        }
    }
}
