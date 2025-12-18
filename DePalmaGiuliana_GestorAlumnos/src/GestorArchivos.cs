using DePalmaGiuliana_GestorAlumnos.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DePalmaGiuliana_GestorAlumnos.src
{
    // Clase responsable de todas las operaciones relacionadas con archivos
    // creación, lectura, modificación, eliminación y conversión de formatos
    public class GestorArchivos
    {
        // Delegado utilizado para mostrar mensajes en la interfaz (TextBox multiline)
        private readonly Action<string> imprimir;

        // Constructor que recibe el método de impresión desde el Form
        public GestorArchivos(Action<string> imprimir)
        {
            this.imprimir = imprimir;
        }

        // Permite crear un archivo nuevo en el formato seleccionado
        // solicitando los datos de los alumnos
        public void CrearArchivo()
        {
            imprimir("=== CREAR ARCHIVO ===");

            // Solicita el nombre base del archivo
            string nombre = Input("Nombre del archivo (sin extensión):");
            if (string.IsNullOrWhiteSpace(nombre))
            {
                imprimir("Nombre inválido.");
                return;
            }

            // Solicita el formato del archivo
            string formato = Input("Formato (TXT, CSV, JSON, XML):").ToUpper().Trim();
            if (formato != "TXT" && formato != "CSV" && formato != "JSON" && formato != "XML")
            {
                imprimir("Formato inválido.");
                return;
            }

            // Solicita la cantidad de alumnos a ingresar
            int cantidad;
            if (!int.TryParse(Input("Cantidad de alumnos:"), out cantidad) || cantidad <= 0)
            {
                imprimir("Cantidad inválida.");
                return;
            }

            // Lista donde se almacenan los alumnos ingresados
            List<Alumno> alumnos = new();

            // Carga de datos de cada alumno
            for (int i = 0; i < cantidad; i++)
            {
                imprimir($"--- Alumno {i + 1} ---");

                Alumno a = new Alumno();
                a.Legajo = Input("Legajo:");
                a.Apellido = Input("Apellido:");
                a.Nombres = Input("Nombres:");
                a.NumeroDocumento = Input("Documento:");

                // Validación del email
                string email;
                do
                {
                    email = Input("Email:");
                    if (!EmailValido(email))
                        imprimir("Email inválido. Debe contener @ y dominio.");
                }
                while (!EmailValido(email));

                a.Email = email;
                a.Telefono = Input("Teléfono:");

                alumnos.Add(a);
            }

            // Construcción de la ruta final del archivo
            string ruta = $"{nombre}.{formato.ToLower()}";

            // Guarda el archivo según el formato elegido
            GuardarArchivo(ruta, alumnos, formato);

            imprimir($"Archivo '{ruta}' creado con éxito.");
        }

        // Muestra un InputBox para solicitar datos al usuario
        private string Input(string mensaje)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(mensaje, "Ingreso de datos").Trim();
        }

        // Valida que el email contenga formato básico correcto
        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Contains("@") && email.Contains(".");
        }

        // Permite leer y mostrar un archivo existente
        public void LeerArchivo()
        {
            try
            {
                imprimir("=== LEER ARCHIVO ===");

                string archivo = Input("Ingrese el nombre del archivo (con extensión):");
                if (string.IsNullOrWhiteSpace(archivo))
                {
                    imprimir("Nombre inválido.");
                    return;
                }

                if (!File.Exists(archivo))
                {
                    imprimir($"El archivo '{archivo}' no existe.");
                    return;
                }

                // Determina el formato según la extensión
                string ext = Path.GetExtension(archivo).ToLower();
                List<Alumno> alumnos = ext switch
                {
                    ".txt" => LeerTXT(archivo),
                    ".csv" => LeerCSV(archivo),
                    ".json" => LeerJSON(archivo),
                    ".xml" => LeerXML(archivo),
                    _ => null
                };

                if (alumnos == null || alumnos.Count == 0)
                {
                    imprimir("No hay registros o el formato no es válido.");
                    return;
                }

                // Encabezado de la tabla
                imprimir("==========================================================================================");
                imprimir("| Legajo  | Apellido       | Nombres            | Documento    | Email                       | Teléfono    |");
                imprimir("==========================================================================================");

                int contador = 0;

                // Muestra los alumnos en formato de tabla
                foreach (var a in alumnos)
                {
                    string linea =
                        $"| {a.Legajo.PadRight(7)}" +
                        $"| {a.Apellido.PadRight(14)}" +
                        $"| {a.Nombres.PadRight(18)}" +
                        $"| {a.NumeroDocumento.PadRight(12)}" +
                        $"| {a.Email.PadRight(27)}" +
                        $"| {a.Telefono.PadRight(12)}|";

                    imprimir(linea);
                    contador++;

                    // Paginación cada 20 registros
                    if (contador % 20 == 0)
                    {
                        imprimir("---- Pulse ENTER para continuar ----");
                        MessageBox.Show("Mostrando 20 registros.\nPulse Aceptar para continuar.");
                    }
                }

                imprimir("==========================================================================================");
                imprimir($"Total de alumnos: {alumnos.Count}");
            }
            catch (Exception ex)
            {
                imprimir("Ocurrió un error al leer el archivo:");
                imprimir(ex.Message);
            }
        }

        // Lee un archivo TXT
        public List<Alumno> LeerTXT(string ruta)
        {
            List<Alumno> lista = new();

            foreach (var linea in File.ReadAllLines(ruta))
            {
                string[] campos = linea.Split('|');

                if (campos.Length == 6)
                {
                    lista.Add(new Alumno
                    {
                        Legajo = campos[0],
                        Apellido = campos[1],
                        Nombres = campos[2],
                        NumeroDocumento = campos[3],
                        Email = campos[4],
                        Telefono = campos[5]
                    });
                }
            }

            return lista;
        }

        // Lee un archivo CSV
        public List<Alumno> LeerCSV(string ruta)
        {
            List<Alumno> lista = new();
            string[] lineas = File.ReadAllLines(ruta);

            // Se omite la primera línea (encabezado)
            for (int i = 1; i < lineas.Length; i++)
            {
                string[] c = lineas[i].Split(',');

                if (c.Length == 6)
                {
                    lista.Add(new Alumno
                    {
                        Legajo = c[0],
                        Apellido = c[1],
                        Nombres = c[2],
                        NumeroDocumento = c[3],
                        Email = c[4],
                        Telefono = c[5]
                    });
                }
            }

            return lista;
        }

        // Lee un archivo JSON
        public List<Alumno> LeerJSON(string ruta)
        {
            string contenido = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Alumno>>(contenido);
        }

        // Lee un archivo XML
        public List<Alumno> LeerXML(string ruta)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Alumno>));
            using FileStream fs = new FileStream(ruta, FileMode.Open);
            return (List<Alumno>)ser.Deserialize(fs);
        }

        // Permite modificar el contenido de un archivo existente
        public void ModificarArchivo()
        {
            imprimir("=== MODIFICAR ARCHIVO ===");

            string archivo = Input("Ingrese el nombre del archivo (con extensión):");

            if (string.IsNullOrWhiteSpace(archivo))
            {
                imprimir("Nombre inválido.");
                return;
            }

            if (!File.Exists(archivo))
            {
                imprimir($"El archivo '{archivo}' no existe.");
                return;
            }

            string ext = Path.GetExtension(archivo).ToLower();

            // Carga los alumnos según el formato
            List<Alumno> alumnos = ext switch
            {
                ".txt" => LeerTXT(archivo),
                ".csv" => LeerCSV(archivo),
                ".json" => LeerJSON(archivo),
                ".xml" => LeerXML(archivo),
                _ => null
            };

            if (alumnos == null)
            {
                imprimir("Formato no soportado.");
                return;
            }

            bool salir = false;
            bool guardar = false;

            // Submenú de modificación
            while (!salir)
            {
                imprimir("");
                imprimir("=== SUB-MENÚ DE MODIFICACIÓN ===");
                imprimir("1. Agregar alumno");
                imprimir("2. Modificar alumno por legajo");
                imprimir("3. Eliminar alumno por legajo");
                imprimir("4. Guardar cambios y salir");
                imprimir("5. Cancelar (salir sin guardar)");

                string opcion = Input("Ingrese una opción:");

                switch (opcion)
                {
                    case "1":
                        AgregarAlumno(alumnos);
                        break;
                    case "2":
                        EditarAlumno(alumnos);
                        break;
                    case "3":
                        EliminarAlumno(alumnos);
                        break;
                    case "4":
                        guardar = true;
                        salir = true;
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        imprimir("Opción inválida.");
                        break;
                }
            }

            // Guarda cambios creando backup
            if (guardar)
            {
                string backup = archivo + ".bak";
                File.Copy(archivo, backup, true);
                imprimir($"Backup creado: {backup}");

                GuardarArchivo(archivo, alumnos, ext.Substring(1).ToUpper());
                imprimir("Cambios guardados.");
            }
            else
            {
                imprimir("Cambios descartados.");
            }
        }

        // Agrega un alumno nuevo
        private void AgregarAlumno(List<Alumno> alumnos)
        {
            imprimir("=== AGREGAR ALUMNO ===");

            string legajo = Input("Legajo:");

            if (string.IsNullOrWhiteSpace(legajo))
            {
                imprimir("Legajo inválido.");
                return;
            }

            if (alumnos.Any(a => a.Legajo == legajo))
            {
                imprimir("Ese legajo ya existe.");
                return;
            }

            Alumno nuevo = new Alumno
            {
                Legajo = legajo,
                Apellido = Input("Apellido:"),
                Nombres = Input("Nombres:"),
                NumeroDocumento = Input("Documento:"),
                Email = Input("Email:"),
                Telefono = Input("Teléfono:")
            };

            alumnos.Add(nuevo);
            imprimir("Alumno agregado.");
        }

        // Modifica un alumno existente
        private void EditarAlumno(List<Alumno> alumnos)
        {
            string leg = Input("Legajo del alumno a modificar:");

            var alum = alumnos.FirstOrDefault(a => a.Legajo == leg);

            if (alum == null)
            {
                imprimir("No existe ese legajo.");
                return;
            }

            imprimir("Deje vacío para mantener el valor anterior.");

            string nuevo;

            nuevo = Input($"Apellido ({alum.Apellido}):");
            if (!string.IsNullOrWhiteSpace(nuevo)) alum.Apellido = nuevo;

            nuevo = Input($"Nombres ({alum.Nombres}):");
            if (!string.IsNullOrWhiteSpace(nuevo)) alum.Nombres = nuevo;

            nuevo = Input($"Documento ({alum.NumeroDocumento}):");
            if (!string.IsNullOrWhiteSpace(nuevo)) alum.NumeroDocumento = nuevo;

            nuevo = Input($"Email ({alum.Email}):");
            if (!string.IsNullOrWhiteSpace(nuevo))
            {
                if (!EmailValido(nuevo))
                {
                    imprimir("Email inválido. No se actualizó el email.");
                }
                else
                {
                    alum.Email = nuevo;
                }
            }

            nuevo = Input($"Teléfono ({alum.Telefono}):");
            if (!string.IsNullOrWhiteSpace(nuevo)) alum.Telefono = nuevo;

            imprimir("Alumno actualizado.");
        }

        // Elimina un alumno por legajo
        private void EliminarAlumno(List<Alumno> alumnos)
        {
            string leg = Input("Legajo del alumno a eliminar:");
            var alum = alumnos.FirstOrDefault(a => a.Legajo == leg);

            if (alum == null)
            {
                imprimir("No existe ese legajo.");
                return;
            }

            string confirm = Input($"Escriba CONFIRMAR para eliminar a {alum.Apellido}, {alum.Nombres}:");

            if (confirm.ToUpper() == "CONFIRMAR")
            {
                alumnos.Remove(alum);
                imprimir("Alumno eliminado.");
            }
            else
            {
                imprimir("Operación cancelada.");
            }
        }

        // Elimina un archivo del sistema
        public void EliminarArchivo()
        {
            try
            {
                imprimir("=== ELIMINAR ARCHIVO ===");

                string archivo = Input("Ingrese el nombre del archivo (con extensión):");

                if (string.IsNullOrWhiteSpace(archivo))
                {
                    imprimir("Nombre inválido.");
                    return;
                }

                if (!File.Exists(archivo))
                {
                    imprimir($"El archivo '{archivo}' no existe.");
                    return;
                }

                // Muestra información del archivo
                FileInfo info = new FileInfo(archivo);

                imprimir("----------------------------------------------");
                imprimir($"Nombre: {info.Name}");
                imprimir($"Tamaño: {info.Length / 1024.0:F2} KB");
                imprimir($"Creado: {info.CreationTime}");
                imprimir($"Última modificación: {info.LastWriteTime}");
                imprimir("----------------------------------------------");

                string confirmacion = Input("Escriba CONFIRMAR para eliminar el archivo:");

                if (confirmacion.ToUpper() == "CONFIRMAR")
                {
                    File.Delete(archivo);
                    imprimir($"Archivo '{archivo}' eliminado correctamente.");
                }
                else
                {
                    imprimir("Operación cancelada.");
                }
            }
            catch (Exception ex)
            {
                imprimir("Error al intentar eliminar el archivo:");
                imprimir(ex.Message);
            }
        }

        public void GuardarArchivo(string ruta, List<Alumno> alumnos, string formato)
        {
            switch (formato)
            {
                case "TXT":
                    GuardarTXT(ruta, alumnos);
                    break;

                case "CSV":
                    GuardarCSV(ruta, alumnos);
                    break;

                case "JSON":
                    GuardarJSON(ruta, alumnos);
                    break;

                case "XML":
                    GuardarXML(ruta, alumnos);
                    break;
            }
        }

        private static void GuardarTXT(string ruta, List<Alumno> alumnos)
        {
            File.WriteAllLines(ruta, alumnos.Select(a => a.ToString()));
        }

        private static void GuardarCSV(string ruta, List<Alumno> alumnos)
        {
            using StreamWriter sw = new StreamWriter(ruta);
            sw.WriteLine("Legajo,Apellido,Nombres,NumeroDocumento,Email,Telefono");

            foreach (var a in alumnos)
            {
                sw.WriteLine($"{a.Legajo},{a.Apellido},{a.Nombres},{a.NumeroDocumento},{a.Email},{a.Telefono}");
            }
        }
        private static void GuardarJSON(string ruta, List<Alumno> alumnos)
        {
            string json = JsonSerializer.Serialize(
                alumnos,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(ruta, json);
        }
        private static void GuardarXML(string ruta, List<Alumno> alumnos)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Alumno>));
            using FileStream fs = new FileStream(ruta, FileMode.Create);
            ser.Serialize(fs, alumnos);
        }


    }
}
