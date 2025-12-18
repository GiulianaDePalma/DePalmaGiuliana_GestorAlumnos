using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DePalmaGiuliana_GestorAlumnos.src
{
    // Clase que actúa como intermediaria entre la interfaz gráfica (Form)
    // y las distintas funcionalidades del sistema
    public class Menu
    {
        // Delegado utilizado para mostrar mensajes en la interfaz
        private readonly Action<string> imprimir;

        // Instancia que maneja la creación, lectura, modificación y eliminación de archivos
        private readonly GestorArchivos gestor;

        // Instancia que maneja la conversión entre formatos de archivos
        private readonly Conversor conversor;

        // Instancia que se encarga de generar reportes a partir de archivos
        private readonly GeneradorReportes generador;

        // Constructor que recibe el método de impresión desde el Form
        // e inicializa todas las clases funcionales
        public Menu(Action<string> imprimir)
        {
            this.imprimir = imprimir;

            gestor = new GestorArchivos(imprimir);
            conversor = new Conversor(imprimir);
            generador = new GeneradorReportes(imprimir);
        }

        // Ejecuta la acción correspondiente según la opción seleccionada
        // desde el menú de la interfaz gráfica
        public void EjecutarOpcion(int opcion)
        {
            switch (opcion)
            {
                // Crear un nuevo archivo de alumnos
                case 1:
                    gestor.CrearArchivo();
                    break;

                // Leer y mostrar un archivo existente
                case 2:
                    gestor.LeerArchivo();
                    break;

                // Modificar el contenido de un archivo
                case 3:
                    gestor.ModificarArchivo();
                    break;

                // Eliminar un archivo del sistema
                case 4:
                    gestor.EliminarArchivo();
                    break;

                // Convertir un archivo de un formato a otro
                case 5:
                    conversor.ConvertirFormato();
                    break;

                // Generar un reporte ordenado de alumnos
                case 6:
                    generador.GenerarReporte();
                    break;

                // Opción inválida
                default:
                    imprimir("Opción inválida.");
                    break;
            }
        }
    }
}
