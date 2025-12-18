using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DePalmaGiuliana_GestorAlumnos.src.Models;

namespace DePalmaGiuliana_GestorAlumnos.src
{
    public class Conversor
    {
        // Acción utilizada para imprimir mensajes en la interfaz del usuario
        private readonly Action<string> imprimir;

        // Instancia del GestorArchivos que se utiliza para leer y guardar archivos
        private readonly GestorArchivos gestor;

        // Constructor de la clase Conversor
        public Conversor(Action<string> imprimir)
        {
            this.imprimir = imprimir;
            // Inicialización de la instancia del GestorArchivos
            this.gestor = new GestorArchivos(imprimir);
        }

        // Método para convertir un archivo de un formato a otro
        public void ConvertirFormato()
        {
            try
            {
                // Imprime un mensaje indicando que se iniciará la conversión
                imprimir("=== CONVERSIÓN DE FORMATOS ===");

                // Solicita el nombre del archivo de origen (incluyendo la extensión)
                string archivoOrigen = Input("Ingrese el archivo origen con extensión:");

                // Verifica si el nombre del archivo está vacío o nulo
                if (string.IsNullOrWhiteSpace(archivoOrigen))
                {
                    imprimir("Nombre inválido.");
                    return; // Finaliza el método si el nombre del archivo es inválido
                }

                // Verifica si el archivo existe en la ubicación especificada
                if (!File.Exists(archivoOrigen))
                {
                    imprimir($"El archivo '{archivoOrigen}' no existe.");
                    return; // Finaliza el método si el archivo no se encuentra
                }

                // Obtiene la extensión del archivo y la convierte a minúsculas para procesarlo
                string ext = Path.GetExtension(archivoOrigen).ToLower();

                // Dependiendo de la extensión del archivo, se leerán los datos de diferentes formas
                List<Alumno> alumnos = ext switch
                {
                    ".txt" => gestor.LeerTXT(archivoOrigen),  // Lee archivo en formato TXT
                    ".csv" => gestor.LeerCSV(archivoOrigen),  // Lee archivo en formato CSV
                    ".json" => gestor.LeerJSON(archivoOrigen), // Lee archivo en formato JSON
                    ".xml" => gestor.LeerXML(archivoOrigen),   // Lee archivo en formato XML
                    _ => null  // Si el archivo no tiene una extensión válida, retorna null
                };

                // Verifica si los datos no fueron leídos correctamente o si el formato no es soportado
                if (alumnos == null)
                {
                    imprimir("Formato no soportado.");
                    return; // Finaliza el método si el formato no es soportado
                }

                // Solicita al usuario que ingrese el formato de destino para la conversión
                string destino = Input("Formato destino (TXT, CSV, JSON, XML):")
                                  .Trim()
                                  .ToUpper();

                // Verifica que el formato destino sea válido
                if (destino != "TXT" && destino != "CSV" && destino != "JSON" && destino != "XML")
                {
                    imprimir("Formato destino inválido.");
                    return; // Finaliza el método si el formato de destino es inválido
                }

                // Obtiene el nombre base del archivo (sin la extensión) para crear el archivo destino
                string nombreBase = Path.GetFileNameWithoutExtension(archivoOrigen);
                // Crea el nombre del archivo destino, agregando la nueva extensión
                string archivoDestino = nombreBase + "." + destino.ToLower();

                // Utiliza el método GuardarArchivo del GestorArchivos para guardar el archivo convertido
                gestor.GuardarArchivo(archivoDestino, alumnos, destino);

                // Informa al usuario que la conversión fue exitosa
                imprimir($"Conversión realizada con éxito. Archivo generado: {archivoDestino}");
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, imprime el mensaje de error
                imprimir("Error durante la conversión:");
                imprimir(ex.Message);
            }
        }

        // Método para solicitar al usuario un texto mediante un cuadro de entrada
        private string Input(string mensaje)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(mensaje, "Conversión")
                   .Trim(); // Retorna el texto ingresado por el usuario, eliminando espacios
        }
    }
}
