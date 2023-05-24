# ServicioCom_LecturasMedidor

Este proyecto en C# consiste en dos aplicaciones de consola: una para el Servicio de Comunicación (Servidor) y otra para el Simulador de Medidor Eléctrico (Cliente). 
Utiliza sockets y bibliotecas de clases para su implementación.

## Funcionamiento

El funcionamiento del proyecto es el siguiente:

1. El cliente (Simulador de Medidor Eléctrico) genera lecturas de valores de consumo, junto con el número de medidor y la fecha correspondiente.
2. El cliente establece una conexión con el servidor (Mediante Sockets y TCP/IP).
3. El cliente ingresa y envía las lecturas al servidor a través del socket de comunicación y luego se desconecta.
4. El servidor recibe las lecturas y utiliza la capa de acceso a datos (DAL) para guardar los datos en el archivo `lecturas.txt`.
5. El servidor desconecta al cliente una vez recibe los datos.
6. El Servidor soporta hasta 10 clientes conectados simultáneamente.
7. La Aplicación del Servidor puede mostrar las lecturas almacenadas en el archivo, e ingresar lecturas nuevas.

## Requisitos

Para compilar y ejecutar este proyecto, se requiere:

- Microsoft Visual Studio (o cualquier otro IDE compatible con C#).

## Instrucciones de uso

1. Clone este repositorio en su máquina local o descargue el código fuente.
2. Abra el proyecto en Microsoft Visual Studio.
3. Compile el proyecto.
4. Ejecute la aplicación del servidor.
5. Ejecute la aplicación del cliente.
6. El cliente generará lecturas de valores de consumo y las enviará al servidor.
7. El servidor recibirá las lecturas de consumo y las guardará en el archivo `lecturas.txt`.
