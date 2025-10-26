using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GestionAcademicaLINQ
{
    class Program
    {
        static void Main()
        {
            string rutaArchivo = "C:\\Users\\Abner\\OneDrive\\Documents\\Visual Studio 2022\\Progra 2-linq\\Progra-II-LINQ\\Ejercicio3-LINQ\\Ejercicio3-LINQ\\notas.txt";
            
            List<Estudiante> estudiantes = CargarEstudiantes(rutaArchivo);

            // a) Estudiantes aprobados (nota >= 70)
            var aprobados = estudiantes.Where(e => e.Nota >= 70);

            // b) Top 5 de estudiantes por curso
            var top5PorCurso = estudiantes
                .GroupBy(e => e.Curso)
                .SelectMany(g => g.OrderByDescending(e => e.Nota).Take(5));

            // c) Promedio por curso
            var promedioPorCurso = estudiantes
                .GroupBy(e => e.Curso)
                .Select(g => new { Curso = g.Key, Promedio = g.Average(e => e.Nota) });

            // d) Top 10 de todos los cursos
            var top10General = estudiantes.OrderByDescending(e => e.Nota).Take(10);

            // e) Ranking general
            var ranking = estudiantes
                .OrderByDescending(e => e.Nota)
                .Select((e, i) => new { Posicion = i + 1, Estudiante = e });

            // f) Mejor estudiante por curso
            var mejorPorCurso = estudiantes
                .GroupBy(e => e.Curso)
                .Select(g => g.OrderByDescending(e => e.Nota).First());

            // g) Intervalos de notas
            var intervalo0_59 = estudiantes.Where(e => e.Nota < 60);
            var intervalo60_79 = estudiantes.Where(e => e.Nota >= 60 && e.Nota <= 79);
            var intervalo80_100 = estudiantes.Where(e => e.Nota >= 80);

            // === Mostrar resultados en consola ===
            Console.WriteLine("=== Estudiantes Aprobados ===");
            foreach (var e in aprobados) Console.WriteLine(e);

            Console.WriteLine("\n=== Top 5 por Curso ===");
            foreach (var e in top5PorCurso) Console.WriteLine(e);

            Console.WriteLine("\n=== Promedio por Curso ===");
            foreach (var p in promedioPorCurso) Console.WriteLine($"{p.Curso}: {p.Promedio:F2}");

            Console.WriteLine("\n=== Top 10 General ===");
            foreach (var e in top10General) Console.WriteLine(e);

            Console.WriteLine("\n=== Ranking General ===");
            foreach (var r in ranking) Console.WriteLine($"#{r.Posicion} - {r.Estudiante.Nombre} ({r.Estudiante.Nota})");

            Console.WriteLine("\n=== Mejor Estudiante por Curso ===");
            foreach (var e in mejorPorCurso) Console.WriteLine($"{e.Curso}: {e.Nombre} ({e.Nota})");

            Console.WriteLine("\n=== Intervalos de Notas ===");
            Console.WriteLine("0-59:");
            foreach (var e in intervalo0_59) Console.WriteLine(e);
            Console.WriteLine("\n60-79:");
            foreach (var e in intervalo60_79) Console.WriteLine(e);
            Console.WriteLine("\n80-100:");
            foreach (var e in intervalo80_100) Console.WriteLine(e);
        }

        static List<Estudiante> CargarEstudiantes(string ruta)
        {
            List<Estudiante> lista = new();

            foreach (var linea in File.ReadAllLines(ruta))
            {
                var partes = linea.Split(',');
                lista.Add(new Estudiante
                {
                    Id = int.Parse(partes[0]),
                    Nombre = partes[1],
                    Curso = partes[2],
                    Nota = double.Parse(partes[3])
                });
            }

            return lista;
        }
    }
}
