using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using Servicio_Comunicaciones.COM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Servicio_ComunicacionesUtils;
using System.Configuration;
using System.Dynamic;

namespace Servicio_Comunicaciones
{
    public class Program
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        //Colores de la aplicación
        static ConsoleColor colorApp1 = ConsoleColor.Cyan;
        static ConsoleColor colorApp2 = ConsoleColor.Cyan;
        static ConsoleColor colorApp3= ConsoleColor.DarkCyan;
        static ConsoleColor colorExito = ConsoleColor.Green;
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
        static bool Menu()
        {
            bool continuar = true;
            ImprimirColor("Selecciona una opcion",colorApp1,true);
            ImprimirColor(" 1. Ingresar \n 2. Mostrar \n 0. Salir",colorApp3,true);
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    IngresarDatos();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    ImprimirColor("Ingrese de Nuevo",colorError,true);
                    break;
            }
            return continuar;
        }

        //Ejecución principal del programa
        static void Main(string[] args)
        {
            //Saludo de bienvenida
            string nombre_usuario = System.Environment.UserName;
            ImprimirColor("Bienvenido "+nombre_usuario,colorAlerta,true);
            //Puerto configurado por defecto desde App.config
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            //Cambiando puerto por defecto durante la ejecucion
            ImprimirColor("Ingresa el puerto: pulsa enter para usar por defecto el puerto ", colorApp1, false);
            ImprimirColor(Convert.ToString(puerto),colorAlerta,true);
            int puertoIngresado;
            try
            {
                puertoIngresado = Convert.ToInt32(Console.ReadLine().Trim());
            }
            catch (Exception)
            {
                puertoIngresado = puerto; //por defecto el 3000
            }
            HebraServidor hebra = new HebraServidor(puertoIngresado);
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            while (Menu()) ;
            // Cerrando app
            ImprimirColor("Cerrando aplicación...",colorError,true);
           Thread.Sleep(3000);
            System.Environment.Exit(0);
        }

        //Ingresando datos desde la aplicación al archivo
        static void IngresarDatos()
        {
            bool continuar=true;
            Int32 nroMedidor=0;
            decimal valorConsumo=0M;
            //Nro medidor
            do
            {
                ImprimirColor("Ingrese número del Medidor: ", colorAlerta, true);
                continuar = true;
                try
                {
                    nroMedidor = Convert.ToInt32(Console.ReadLine().Trim());
                }
                catch (Exception ex)
                {
                    ImprimirColor(ex.Message,colorError,true);
                    continuar = false;
                }
            } while (continuar==false);

            //Fecha
            ImprimirColor("Ingrese Fecha: ", colorAlerta, true);
            string fecha = Console.ReadLine().Replace(" ","-").Trim() ;
            //Valor de Consumo
            do 
            {

                ImprimirColor("Ingrese el valor del Consumo: ", colorAlerta, true);
                continuar = true;
                try
                {
                    valorConsumo = Convert.ToDecimal(Console.ReadLine().Trim());

                }
                catch (Exception ex)
                {
                    ImprimirColor(ex.Message, colorError, true);
                    continuar = false;
                }
            } while (continuar==false);

            Lectura lectura = new Lectura()
            {
                NroMedidor = nroMedidor,
                Fecha = fecha,
                ValorConsumo = valorConsumo
            };
            lock (lecturasDAL)
            {
                lecturasDAL.AgregarLectura(lectura);
            }


        }

        static void Mostrar()
        {
            List<Lectura> lecturas = null;
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }
            Console.WriteLine();
            ImprimirColor("Nro Medidor", colorApp3, false);
            ImprimirColor("|", colorError, false);
            ImprimirColor("Fecha Registro", colorApp2, false);
            ImprimirColor("|", colorError, false);
            ImprimirColor("Valor Consumo", colorAlerta, true);
            foreach (Lectura lectura in lecturas)
            {
                Console.Write("  ");
                ImprimirColor(lectura.NroMedidor + "", colorApp3, false);
                ImprimirColor("|", colorError, false);
                ImprimirColor(lectura.Fecha, colorApp2, false);
                ImprimirColor("|", colorError, false);
                ImprimirColor(lectura.ValorConsumo + "", colorAlerta, true);
            }
            Console.WriteLine();
        }
    }
}
