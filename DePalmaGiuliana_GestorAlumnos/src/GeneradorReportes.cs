using DePalmaGiuliana_GestorAlumnos.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DePalmaGiuliana_GestorAlumnos.src
{
    public class GeneradorReportes
    {
        // Acción que se utilizará para imprimir mensajes en la interfaz
        private readonly Action<string> imprimir;

        // Instancia del GestorArchivos que se utilizará para leer los archivos
        private readonly GestorArchivos gestor;

        // Constructor de la clase que recibe la acción para imprimir mensajes
        public GeneradorReportes(Action<string> imprimir)
        {
            this.imprimir = imprimir;
            // Inicialización de la instancia del GestorArchivos
            gestor = new GestorArchivos(imprimir);
        }

        // Método para generar el reporte de alumnos agrupados por apellido
        public void GenerarReporte()
        {
            // Mensaje inicial para informar que se está generando el reporte
            imprimir("=== GENERAR REPORTE ===");

            // Solicita al usuario el nombre del archivo fuente
            string archivo = Input("Ingrese el archivo fuente con extensión:");

            // Verifica si el nombre del archivo es inválido (vacío o nulo)
            if (string.IsNullOrWhiteSpace(archivo))
            {
                imprimir("Nombre inválido.");
                return; // Finaliza el método si el nombre es inválido
            }

            // Verifica si el archivo existe en la ruta indicada
            if (!File.Exists(archivo))
            {
                imprimir($"El archivo '{archivo}' no existe.");
                return; // Finaliza el método si el archivo no existe
            }

            // Obtiene la extensión del archivo y la convierte a minúsculas
            string ext = Path.GetExtension(archivo).ToLower();

            // Dependiendo de la extensión del archivo, se leen los datos de una forma u otra
            List<Alumno> alumnos = ext switch
            {
                ".txt" => gestor.LeerTXT(archivo),  // Llama al método LeerTXT si es un archivo TXT
                ".csv" => gestor.LeerCSV(archivo),  // Llama al método LeerCSV si es un archivo CSV
                ".json" => gestor.LeerJSON(archivo), // Llama al método LeerJSON si es un archivo JSON
                ".xml" => gestor.LeerXML(archivo),   // Llama al método LeerXML si es un archivo XML
                _ => null // Si no es ninguno de esos formatos, retorna null
            };

            // Verifica si no se pudieron leer alumnos o si el archivo está vacío
            if (alumnos == null || alumnos.Count == 0)
            {
                imprimir("El archivo no contiene alumnos o el formato no es soportado.");
                return; // Finaliza el método si no se obtienen alumnos
            }

            // Agrupa los alumnos por apellido, y los ordena alfabéticamente por apellido y nombre
            var grupos = alumnos
                .OrderBy(a => a.Apellido)   // Ordena por apellido
                .ThenBy(a => a.Nombres)     // Luego ordena por nombre
                .GroupBy(a => a.Apellido);  // Agrupa por apellido

            // StringBuilder para ir construyendo el reporte en formato texto
            StringBuilder sb = new StringBuilder();

            // Títulos del reporte
            sb.AppendLine("===============================================");
            sb.AppendLine("      REPORTE DE ALUMNOS POR APELLIDO");
            sb.AppendLine($"      Fecha: {DateTime.Now}");  // Muestra la fecha actual
            sb.AppendLine("===============================================");
            sb.AppendLine(); // Línea en blanco

            int totalGeneral = 0; // Contador para el total de alumnos

            // Itera a través de los grupos de alumnos
            foreach (var grupo in grupos)
            {
                // Agrega el apellido del grupo al reporte
                sb.AppendLine($"APELLIDO: {grupo.Key}");
                sb.AppendLine("-----------------------------------------------");  // Línea de separación

                // Itera a través de los alumnos de cada grupo
                foreach (var a in grupo)
                {
                    // Agrega los detalles del alumno al reporte
                    sb.AppendLine($"Legajo: {a.Legajo}");
                    sb.AppendLine($"Nombre: {a.Nombres}");
                    sb.AppendLine($"Documento: {a.NumeroDocumento}");
                    sb.AppendLine($"Email: {a.Email}");
                    sb.AppendLine($"Teléfono: {a.Telefono}");
                    sb.AppendLine(); // Línea en blanco después de los datos del alumno
                }

                // Agrega el subtotal de alumnos en ese grupo (por apellido)
                sb.AppendLine($"Subtotal {grupo.Key}: {grupo.Count()} alumno(s)");
                sb.AppendLine(); // Línea en blanco después del subtotal

                totalGeneral += grupo.Count(); // Suma la cantidad de alumnos al total general
            }

            // Agrega la sección final del reporte con el total de alumnos
            sb.AppendLine("===============================================");
            sb.AppendLine($"Total de alumnos: {totalGeneral}");
            sb.AppendLine("===============================================");

            // Imprime el reporte en la consola
            imprimir(sb.ToString());

            // Pregunta al usuario si desea guardar el reporte
            string guardar = Input("¿Guardar reporte en TXT? (S/N):").ToUpper();

            // Si la respuesta es "S", guarda el reporte en un archivo de texto
            if (guardar == "S")
            {
                string nombre = Input("Nombre del archivo (sin extensión):"); // Solicita el nombre del archivo
                File.WriteAllText(nombre + "_reporte.txt", sb.ToString()); // Guarda el reporte con el nombre indicado
                imprimir("Reporte guardado correctamente."); // Informa que el reporte se guardó
            }
        }

        // Método para pedir al usuario una entrada de texto
        private string Input(string mensaje)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(mensaje, "Reporte").Trim(); // Usa un cuadro de texto para obtener datos
        }
    }
}

