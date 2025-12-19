# DePalmaGiuliana_GestorAlumnos

# De Palma Giuliana - Legajo: B00114920-T1

## Descripción del proyecto

Aplicación de escritorio desarrollada en **C# con Windows Forms (.NET 8)** que permite gestionar alumnos a través de archivos en distintos formatos.  
El sistema permite crear, leer, modificar, eliminar, convertir archivos y generar reportes, mostrando los resultados en una interfaz gráfica con un área de salida tipo consola.

---

## Requisitos del sistema

- Sistema operativo: Windows
- **.NET 8 SDK** instalado
- Visual Studio 2022 o superior (recomendado)

---

## Instrucciones de compilación

1. Abrir **Visual Studio**
2. Seleccionar **Abrir un proyecto o solución**
3. Abrir la carpeta `DePalmaGiuliana_GestorAlumnos`
4. Abrir el archivo `.sln`
5. Verificar que el framework seleccionado sea **.NET 8**
6. Compilar el proyecto con **Build → Build Solution**
7. Ejecutar con **F5**

---

## Instrucciones de uso básico

1. Al ejecutar la aplicación se muestra la ventana principal.
2. Utilizar el menú superior para acceder a las opciones:
   - Crear archivo
   - Leer archivo
   - Modificar archivo
   - Eliminar archivo
   - Convertir formato
   - Generar reporte
3. Ingresar los datos solicitados mediante ventanas de ingreso.
4. Los resultados y mensajes del sistema se muestran en el área de texto principal.
5. Para salir de la aplicación, utilizar la opción **Salir** del menú.

---

## Problemas conocidos

- La validación de email es básica (verifica presencia de `@` y `.`).
- Los archivos se guardan en el mismo directorio donde se ejecuta la aplicación.

---

## Extras implementados

- Validación de email en creación y modificación de alumnos.
- Generación automática de backups (`.bak`) antes de guardar modificaciones.
- Paginación de resultados al leer archivos con muchos registros.
- Conversión entre formatos TXT, CSV, JSON y XML.
- Generación de reportes agrupados por apellido.
