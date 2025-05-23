using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

struct Tarea
{
    public string Descripcion;
    public DateTime FechaDeVencimiento;
    public bool Estado;
   
};


class Program
{
    static Tarea[] listaTareas = new Tarea[0];
    static int cantidadDeTareas = 0;

    public static void Main(string[] args)
    {
         
        

        //menu
        while (true) {
            Console.WriteLine("""
                
                ----------MENU PRINCIPAL-----------
                1. Crear nueva tarea.
                2. Editar tarea.
                3. Ordenar y filtrar tareas.
                4. Mostrar tareas proximas a vencer 
                5. Marcar tarea como completada
                6. Salir
                -----------------------------------
                
                """);

            Console.WriteLine("Ingresa opcion ");
            var opcion = int.Parse(Console.ReadLine());

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
                    MostrarTareasAVencer();
                    break;
                case 5:
                    MarcarTareaComoCompletada();
                    break;
                case 6:
                    Console.WriteLine("Programa finalizado");
                    return;
                default:
                    Console.WriteLine("Opcion no valida");
                    break;
            }
        }
        

    }


    private static void MarcarTareaComoCompletada()
    {
        throw new NotImplementedException();
    }

     private static void MostrarTareasAVencer()
    {
        throw new NotImplementedException();
    }

    private static void ordenarTareas(Tarea[]listaTareas)
    {
        //validar que la lista de tareas no este vacia
        if (listaTareas == null|listaTareas.Length == 0 )
        {
            Console.WriteLine("(!) La lista de tareas esta vacia. Intenta agregar una nueva tarea antes");
            return;
        }
        else Console.WriteLine("-----------------------Lista de tareas ordenas por fecha de vencimiento---------------------");

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

        //recorrer y mostrar al usuario toda la lista de tareas
        for (int i = 0; i < listaTareas.Length; i++)
        {
            Console.WriteLine($"{i + 1} Descripcion: {listaTareas[i].Descripcion}|Vence: {listaTareas[i].FechaDeVencimiento.ToShortDateString()}|Estado: {(listaTareas[i].Estado?"Completada":"En proceso")}");
        }
        Console.WriteLine();
        Console.Write("Presiona cualquier tecla para volver al menu principal");
        Console.ReadKey();
    }

    private static void editarTarea()
    {
        throw new NotImplementedException();
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
                Console.WriteLine("(!) Debes ingresa un valor valido");
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
                Estado = false
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
} 