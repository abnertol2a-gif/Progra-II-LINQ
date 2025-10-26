using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ejercicio2_LINQ
{
    class Program
    {
        static void Main()
        {
            string rutaArchivo = "C:\\Users\\Abner\\OneDrive\\Documents\\Visual Studio 2022\\Progra 2-linq\\Progra-II-LINQ\\inventario.txt";
            List<Producto> productos = CargarProductos(rutaArchivo);

            // a) Productos con stock menor a 10
            var bajoStock = productos.Where(p => p.Stock < 10);

            // b) Productos ordenados por precio descendente
            var ordenadosPorPrecio = productos.OrderByDescending(p => p.Precio);

            // c) Total del valor del inventario
            var valorTotal = productos.Sum(p => p.Precio * p.Stock);

            // d) Agrupar productos por categoría
            var agrupadosPorCategoria = productos.GroupBy(p => p.Categoria);

            // Mostrar resultados en consola
            Console.WriteLine("=== Productos con stock menor a 10 ===");
            foreach (var p in bajoStock) Console.WriteLine(p);

            Console.WriteLine("\n=== Productos ordenados por precio descendente ===");
            foreach (var p in ordenadosPorPrecio) Console.WriteLine(p);

            Console.WriteLine($"\n=== Valor total del inventario: {valorTotal:C} ===");

            Console.WriteLine("\n=== Productos agrupados por categoría ===");
            foreach (var grupo in agrupadosPorCategoria)
            {
                Console.WriteLine($"\nCategoría: {grupo.Key}");
                foreach (var p in grupo)
                    Console.WriteLine($"  - {p.Nombre} (${p.Precio})");
            }
        }

        static List<Producto> CargarProductos(string ruta)
        {
            List<Producto> lista = new();

            foreach (var linea in File.ReadAllLines(ruta))
            {
                var partes = linea.Split(',');

                lista.Add(new Producto
                {
                    Id = int.Parse(partes[0]),
                    Nombre = partes[1],
                    Categoria = partes[2],
                    Precio = decimal.Parse(partes[3]),
                    Stock = int.Parse(partes[4])
                });
            }

            return lista;
        }
    }
}
