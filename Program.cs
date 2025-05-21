using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

struct Tarea
{
    public string Descripcion;
    public DateTime FechaDeVencimiento;
    public bool Completada;
    public string CreadoPor;
};


class Program
{
    

    public static void Main(string[] args)
    {
        List<Tarea> tareas = new List<Tarea>();


      
        //menu
        while (true) {
            Console.WriteLine("""
                
                ----------MENU PRINCIPAL-----------
                1. Crear nueva tarea.
                2. Editar tarea.
                3. Ordenar tareas.
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
                    crearNuevaTarea(tareas);
                    break;
                case 2:
                    editarTarea();
                    break;
                case 3:
                    ordenarTareas();
                    break;
                case 4:
                    MostrarTareasAVencer(tareas);
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

     private static void MostrarTareasAVencer(List<Tarea> tareas)
    {
        Console.WriteLine("Lista de tareas: ");
        foreach(var tarea in tareas)
        {
            Console.WriteLine($"Tarea: {tarea.Descripcion}-vence: {tarea.FechaDeVencimiento}-Creador por: {tarea.CreadoPor}");
        }
        Console.WriteLine("Ingrese cualquier tecla para volver al menu principal");
        Console.ReadKey();
    }

    private static void ordenarTareas()
    {
        throw new NotImplementedException();
    }

    private static void editarTarea()
    {
        throw new NotImplementedException();
    }

    private static void crearNuevaTarea(List<Tarea> listaTareas)
    {
        Console.WriteLine("Descripción de la tarea: ");
        string descripcion = Console.ReadLine();

        Console.WriteLine("fecha de vencimiento: ");
        DateTime fechaVencimiento = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Ingresa tu nombre de usuario:");
        string creador = Console.ReadLine();

        Tarea tareaNueva = new Tarea
        {
            Descripcion = descripcion,
            FechaDeVencimiento = fechaVencimiento,
            Completada = false,
            CreadoPor = creador
        };

        listaTareas.Add(tareaNueva);
        Console.WriteLine("Tarea agragada con exito");
        Console.WriteLine("Presiona cualquier tecla para volver al menu principal");
        Console.ReadKey();

    }
} 