using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Contracts;

struct Tarea
{
    public string Descripcion;
    public DateTime FechaDeVencimiento;
    public EstadoTarea Estado;
   
};

enum EstadoTarea
{
    EnProceso,
    Completada,
    Vencida
}
struct Usuario
{
    public string NombreUsuario;
    public string NumeroIdentificacion;
    public string CorreoElectronico;
}


class Program
{
    static Tarea[] listaTareas = new Tarea[0];
    static int cantidadDeTareas = 0;
    static Usuario usuario;
    

    public static void Main(string[] args)
    {
        Console.WriteLine("----------Bienvendio a nuestro programa de gestion de tareas----------");
        Console.WriteLine("A continuacion debes registrar tus datos");
        usuario = RegistrarUsuario();
        Console.Clear();
        Console.WriteLine($"Nombre: {usuario.NombreUsuario}\nDUI o Carnet: {usuario.NumeroIdentificacion}\nCorreo: {usuario.CorreoElectronico}");
        Console.WriteLine("Presiona cualquier tecla para continuar al menú...");
        Console.ReadKey();

        //menu
        while (true) {
            Console.Clear();
            Console.WriteLine("""
                
                ----------MENU PRINCIPAL-----------
                1. Crear nueva tarea.
                2. Editar tarea.
                3. Ordenar tareas por fecha y estado. 
                4. Marcar tarea como completada
                5. Salir
                -----------------------------------
                
                """);

            Console.WriteLine("Ingresa opcion ");
            var opcion = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (opcion)
            {
                case 1:
                    crearNuevaTarea();
                    break;
                case 2:
                    editarTarea();
                    break;
                case 3:
                    ordenarTareas(listaTareas);
                    break;
                case 4:
                    MarcarTareaComoCompletada(listaTareas);
                    break;
                case 5:
                    Console.WriteLine("Programa finalizado");
                    return;
                default:
                    Console.WriteLine("Opcion no valida");
                    break;
            }
        }

        
    }


    private static void MarcarTareaComoCompletada(Tarea[] listaTareas)
    {
        int numeroDeTarea;
        bool entradaValida;

        //asegurar que el arreglo de tareas no este vacía
        if (listaTareas == null || listaTareas.Length == 0)
        {
            Console.WriteLine("(!) La lista de tareas está vacia. Intenta agregar una tarea antes");
            Console.Write("Presiona cualquier tecla para volver al menu principal y agregar una tarea");
            Console.ReadKey();
            return;

        }
            //mostrarle la lista de tareas al usuario para que sepa mejor cual quiere marcar
            Console.WriteLine("-------------------------Lista de tareas------------------------");
            for (int i = 0; i < listaTareas.Length; i++)
            {
                Console.WriteLine($"""
                Tarea {i + 1}
                Descripción: {listaTareas[i].Descripcion}
                Vence: {listaTareas[i].FechaDeVencimiento}
                Estado: {listaTareas[i].Estado}
                ---------------------------------------------------------------
                """);
            }

            //sacar el dato de la trea a modificar 
            do
            {
                Console.WriteLine("Ingresa el numero de tarea a la cual quieres marcar como completada");
                entradaValida = int.TryParse(Console.ReadLine(), out numeroDeTarea);
                if (!entradaValida || numeroDeTarea <= 0 || numeroDeTarea > listaTareas.Length)
                {
                    Console.WriteLine("(!) Valor invalido. Ingrese un numero de tarea valido");
                }
            }
            while (!entradaValida || numeroDeTarea <= 0 || numeroDeTarea > listaTareas.Length);

            //asegurar que esa tarea no haya sido marcada como completada antes.
            if (listaTareas[numeroDeTarea - 1].Estado == EstadoTarea.Completada)
            {
                Console.WriteLine("Esa tarea ya ha sido marcada como completada");
                Console.Write("Presiona cualquier tecla para volver al menu principal");
                Console.ReadKey();
                return;

            }

            //modificar el estado como true "Completada"
            listaTareas[numeroDeTarea - 1].Estado = EstadoTarea.Completada;
            Console.WriteLine("Tarea marcada con éxito");
            Console.Write("Presiona cualquier tecla para volver al menu principal");
        Console.ReadKey();
    }
    private static void ordenarTareas(Tarea[]listaTareas)
    {
        //validar que la lista de tareas no este vacia
        if (listaTareas == null|| listaTareas.Length == 0 )
        {
            Console.WriteLine("(!) La lista de tareas esta vacia. Intenta agregar una nueva tarea antes");
            return;
        }
        else Console.WriteLine("-----------------------Lista de tareas ordendas por fecha de vencimiento y Estado---------------------");

        // Ordenamiento manual: Bubble Sort (simple para arreglos)
        for (int i = 0; i < listaTareas.Length - 1; i++)
        {
            for (int j = i + 1; j < listaTareas.Length; j++)
            {
                if (listaTareas[i].FechaDeVencimiento > listaTareas[j].FechaDeVencimiento)
                {
                    // Intercambio de tareas (sin usar listas)
                    Tarea temp = listaTareas[i];
                    listaTareas[i] = listaTareas[j];
                    listaTareas[j] = temp;
                }
            }
        }
        //imprimir los datos del usuario
        MostrarDatosUsuario();
        //recorrer y mostrar al usuario toda la lista de tareas
        for (int i = 0; i < listaTareas.Length; i++)
        {
            Console.WriteLine($"""
                ---------------------------------------------------------------
                |Tarea {i + 1} 
                |Descripción: {listaTareas[i].Descripcion}
                |Vence: {listaTareas[i].FechaDeVencimiento.ToShortDateString()} 
                |Estado: [{listaTareas[i].Estado}] {gestionarVencimiento(listaTareas[i].FechaDeVencimiento, listaTareas[i].Estado)}
                ---------------------------------------------------------------
                """);        
        }
        Console.WriteLine();
        // salidas finales con uso de lambdas y el metodo Count para mas facilidad
        Console.WriteLine($"Total de tareas: {listaTareas.Length}");
        Console.WriteLine($"Total de tareas vencidas: {listaTareas.Count(t => t.Estado == EstadoTarea.Vencida)}");
        Console.WriteLine($"Tareas a vencer en los proximos 5 dias: {listaTareas.Count(t => (t.FechaDeVencimiento - DateTime.Today).Days <= 5 && (t.FechaDeVencimiento - DateTime.Today).Days > 0)}");
        Console.WriteLine();
        Console.Write("Presiona cualquier tecla para volver al menu principal");
        Console.ReadKey();
    }

    private static void editarTarea()
    {
        int tareaAEditar;
        bool entradaValida;
        //validar que la lista de tareas no este vacia
        if (listaTareas == null || listaTareas.Length == 0)
        {
            Console.WriteLine("(!) La lista de tareas esta vacia. Intenta agregar una nueva tarea antes");
            return;
        }
        //mostrar al usuario la lista de tareas
        Console.WriteLine("-----------------------Lista de tareas---------------------");
        //recorrer y mostrar al usuario toda la lista de tareas
        for (int i = 0; i < listaTareas.Length; i++)
        {
            Console.WriteLine($"""
                
                |Tarea {i + 1} 
                |Descripcion: {listaTareas[i].Descripcion}
                |Vence: {listaTareas[i].FechaDeVencimiento.ToShortDateString()}
                |sEstado: {(listaTareas[i].Estado)}
                ------------------------------------------------------------------------
                """);
                
        }
       
        do
        {
            Console.Write("Ingresa el numero de la tarea que quieres editar: ");
            entradaValida = int.TryParse(Console.ReadLine(), out tareaAEditar);
            if (!entradaValida || tareaAEditar < 1 || tareaAEditar > listaTareas.Length)
            {
                Console.WriteLine("(!) Debes ingresar un numero valido");
            }
        }
        while (!entradaValida || tareaAEditar < 1 || tareaAEditar > listaTareas.Length);

        tareaAEditar--; // Ajustar el índice para que coincida con el arreglo
        Console.WriteLine($"*****Editando tarea {tareaAEditar + 1}:*****");
        //validacion para la descripcion
        String Nuevadescripcion;
        do
        {
            Console.Write("Descripcion: ");
            Nuevadescripcion = Console.ReadLine();
            if (Nuevadescripcion == "" || Nuevadescripcion == " ")
            {
                Console.WriteLine("(!) La descripcion de la tarea no puede estar vacia.");
            }
        }
        while (Nuevadescripcion == "" || Nuevadescripcion == " ");

        DateTime NuevafechaDeVencimiento;
        do
        {
            Console.Write("Fecha de vencimiento (YYYY-MM-DD): ");
            entradaValida = DateTime.TryParse(Console.ReadLine(), out NuevafechaDeVencimiento);
            if (!entradaValida)
            {
                Console.WriteLine("(!) Fecha invalida. Intenta nuevamente.");
            }
        }
        while (!entradaValida);

        //actualizar la tarea
        listaTareas[tareaAEditar].Descripcion = Nuevadescripcion;
        listaTareas[tareaAEditar].FechaDeVencimiento = NuevafechaDeVencimiento;
        listaTareas[tareaAEditar].Estado = NuevafechaDeVencimiento < DateTime.Today ? EstadoTarea.Vencida: EstadoTarea.EnProceso; // Reiniciar el estado a "En proceso"
        Console.WriteLine("Tarea editada con exito");
        Console.WriteLine();
        Console.Write("Presiona cualquier tecla para volver al menu principal");
        Console.ReadKey();
    }

    private static void crearNuevaTarea()
    {
        int nuevasTareas;
        String descripcion;
        DateTime fechaDeVencimiento;
        bool entradaValida;

        do
        {
            Console.Write("¿Cuantas tareas quieres agregar esta vez? ");
            entradaValida = int.TryParse(Console.ReadLine(), out nuevasTareas);

            if (!entradaValida||nuevasTareas<=0)
            {
                Console.WriteLine("(!) Debes ingresar un valor valido");
            }
        }
        while (!entradaValida||nuevasTareas<=0);

        //creacion de nuevo arreglo con espacio 
        Tarea[] nuevoArreglo = new Tarea[cantidadDeTareas + nuevasTareas];
        //copiar las tareas existentes al nuevo arreglo
        for (int i = 0; i < cantidadDeTareas; i++)
        {
            nuevoArreglo[i] = listaTareas[i];
        }

        //agregar nuevas tareas
        for (int i = cantidadDeTareas; i < cantidadDeTareas + nuevasTareas; i++)
        {
            Console.WriteLine($"*****ingresando tarea {i + 1}:*****");
            //validacion para la descripcion
            do
            {
                Console.Write("Descripccion: ");
                descripcion=Console.ReadLine();
                if (descripcion==""||descripcion==" ")
                {
                    Console.WriteLine("(!) La descripcion de la tarea no puede estar vacia.");
                }
            }
            while (descripcion==""||descripcion==" ");

            do
            {
                Console.Write("Fecha de vencimeinto (YYYY-MM.DD):");
                entradaValida=DateTime.TryParse(Console.ReadLine(), out fechaDeVencimiento);
                if (!entradaValida)
                {
                    Console.WriteLine("(!) Fecha invalida. intenta nuevamente.");
                }
            }
            while (!entradaValida);

            nuevoArreglo[i] = new Tarea
            {
                Descripcion = descripcion,
                FechaDeVencimiento = fechaDeVencimiento,
                Estado = fechaDeVencimiento < DateTime.Today ? EstadoTarea.Vencida : EstadoTarea.EnProceso
            };

        }
        //actualizar el arreglo y el contador de tareas
        listaTareas = nuevoArreglo;
        cantidadDeTareas += nuevasTareas;

        Console.WriteLine("Todas las tareas han sido agregadas con exito");
        Console.WriteLine();
        Console.Write("Presiona cualquier tecla para volver al menu principal");
        Console.ReadKey();
    }
    static Usuario RegistrarUsuario()
    {
        Usuario usuario = new Usuario();
        bool valido = true;
        string nombreUsuario, numeroIdentificacion, correoElectronico;

        do
        {
            Console.WriteLine("Ingrese su nombre:");
            nombreUsuario = (Console.ReadLine());

            if (nombreUsuario == null || nombreUsuario.Length == 0)
            {
                Console.WriteLine("Error el nombre no puede estar vacio. Inténtelo de nuevo.");
                valido = false;
            }
            else if (nombreUsuario.Length < 3)
            {
                Console.WriteLine("Error el nombre debe tener al menos 3 caracteres. Inténtelo de nuevo.");
                valido = false;
            }

        } while (!valido);
        Console.Clear();

        // ingreso y validacion de el dui o carnet
        do
        {
            Console.WriteLine("Ingresa su DUI o Carnet(sin guiones):");
            numeroIdentificacion = Console.ReadLine();
            valido = true; // Reiniciar la validez para cada iteración

            if (numeroIdentificacion == null || numeroIdentificacion.Length == 0)
            {
                Console.WriteLine("Error el DUI no puede estar vacio. Inténtelo de nuevo.");
                valido = false;
            }
            else if (numeroIdentificacion.Length != 9)
            {
                Console.WriteLine("Error el DUI debe tener exactamente 9 numeros. Inténtelo de nuevo.");
                valido = false;
            }
            else
            {
                for (int i = 0; i < numeroIdentificacion.Length; i++)
                {

                    if (numeroIdentificacion[i] < '0' || numeroIdentificacion[i] > '9')
                    {
                        Console.WriteLine("Error solo se permiten numeros enteros positivos. Inténtelo de nuevo");
                        valido = false;
                        break;
                    }
                }

            }
        } while (!valido);
        Console.Clear();


        // ingreso y validacion de el correo electronico
        do

        {
            valido = true; // Reiniciar la validez para cada iteración

            Console.WriteLine("Ingresa su correo electronico:");
            correoElectronico = Console.ReadLine();

            if (correoElectronico == null || correoElectronico.Length == 0)
                Console.WriteLine("Error el correo no puede estar vacio. Intente de nuevo.");
            else if (correoElectronico.Length < 10)
            {
                Console.WriteLine("Error el correo debe tener al menos 10 caracteres. Intente de nuevo.");
                valido = false;

            }
            else
                for (int i = 0; i < correoElectronico.Length; i++)
                {
                    if (correoElectronico[i] == ' ')
                    {
                        Console.WriteLine("Error el correo no puede contener espacios. Intente de nuevo.");
                        valido = false;
                        break;
                    }
                }

            if (correoElectronico[0] == '@' || correoElectronico[0] == '.' ||
                    correoElectronico[correoElectronico.Length - 1] == '@' ||
                    correoElectronico[correoElectronico.Length - 1] == '.')
            {
                Console.WriteLine("Error: el correo no puede empezar o terminar con '@' o '.'. Intente de nuevo.");
                valido = false;
            }

            else
            {
                // Contar arrobas y puntos
                int cantidadArrobas = 0;
                int cantidadPuntos = 0;

                for (int i = 0; i < correoElectronico.Length; i++)
                {
                    if (correoElectronico[i] == '@')
                        cantidadArrobas++;
                    else if (correoElectronico[i] == '.')
                        cantidadPuntos++;
                }

                // Validando que debe tener exactamente una arroba
                if (cantidadArrobas == 0)
                {
                    Console.WriteLine("Error: el correo debe contener un '@'. Intente de nuevo.");
                    valido = false;
                }
                else if (cantidadArrobas > 1)
                {
                    Console.WriteLine("Error: el correo no puede contener más de un '@'. Intente de nuevo.");
                    valido = false;
                }
                // Validar que debe tener al menos un punto
                else if (cantidadPuntos == 0)
                {
                    Console.WriteLine("Error: el correo debe contener al menos un '.'. Intente de nuevo.");
                    valido = false;
                }
                else
                {
                    // Validar que debe haber al menos un punto después del @
                    int posicionArroba = -1;
                    for (int i = 0; i < correoElectronico.Length; i++)
                    {
                        if (correoElectronico[i] == '@')
                        {
                            posicionArroba = i;
                            break;
                        }
                    }

                    bool hayPuntoDespuesDeArroba = false;
                    for (int i = posicionArroba + 1; i < correoElectronico.Length; i++)
                    {
                        if (correoElectronico[i] == '.')
                        {
                            hayPuntoDespuesDeArroba = true;
                            break;
                        }
                    }

                    if (!hayPuntoDespuesDeArroba)
                    {
                        Console.WriteLine("Error: el correo debe contener un '.' después del '@'. Intente de nuevo.");
                        valido = false;
                    }
                }
            }
        } while (!valido);
        usuario = new Usuario
        {
            NombreUsuario = nombreUsuario,
            NumeroIdentificacion = numeroIdentificacion,
            CorreoElectronico = correoElectronico
        };
        return usuario;

    }

    private static string gestionarVencimiento(DateTime fechaDeVencimiento, EstadoTarea estado)
    {
        if (estado == EstadoTarea.Completada)
            return "";
        int diasRestantes = (fechaDeVencimiento - DateTime.Today).Days;

        if (diasRestantes > 0)
            return $"(Vence en {diasRestantes} dias)";
        else if (diasRestantes == 0)
            return $"((!) La tarea vence hoy)";
        else
            return $"(Vencio hace {-diasRestantes} dias)";
    }

    static void MostrarDatosUsuario()
    {
        Console.WriteLine($"Nombre: {usuario.NombreUsuario}\nDUI o Carnet: {usuario.NumeroIdentificacion}\nCorreo: {usuario.CorreoElectronico}");
    }




}