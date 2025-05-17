using System;
using System.ComponentModel;
using System.ComponentModel.Design;

struct Tarea
{
    public string Descripcion;
    public DateTime FechaDeVencimiento;
    public bool Completada;
    public string CreadoPor;


}



class Program
{
    public static void Main(string[] args)
    {

        String nombreUsuario;
        String id;
        String correo;


        //validar nombre
        /*do
        {
            Console.WriteLine("Ingresa tu nombre");
            nombreUsuario = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                Console.WriteLine("Error el nombre no puede estar vacio. Intenta de nuevo.");
        } while (string.IsNullOrWhiteSpace(nombreUsuario));

        
        Console.WriteLine("Ingresa tu id");
        id = Console.ReadLine();
        */


        Console.WriteLine("""
                1. Crear nueva tarea.
                2. Editar tarea.
                3. Ordenar tareas.
                4. Mostrar tareas proximas a vencer 
                5. Marcar tarea como completadas
                6. Salir

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
                ordenarTareas();
                break;
            case 4:
                MostrarTareasAVencer();
                break;
            case 5:
                MarcarTareaComoCompletada();
                break;
            case 6:
                Console.WriteLine("Presiona cualquier tecla para cerrar programa......");
                Console.ReadKey();
                break;

            default:
                Console.WriteLine("Opcion no valida");
                break;
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

    private static void ordenarTareas()
    {
        throw new NotImplementedException();
    }

    private static void editarTarea()
    {
        throw new NotImplementedException();
    }

    private static void crearNuevaTarea()
    {
        throw new NotImplementedException();
    }
}