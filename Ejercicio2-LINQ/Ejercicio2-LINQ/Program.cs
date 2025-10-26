using Ejercicio2_LINQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InventarioLINQ
{
    class Program
    {
        static void Main()
        {
            // Usamos ruta absoluta para evitar errores de acceso
            string rutaArchivo = "C:\\Users\\Abner\\OneDrive\\Documents\\Visual Studio 2022\\Progra 2-linq\\Progra-II-LINQ\\inventario.txt";
            string rutaSalida = "C:\\Users\\Abner\\OneDrive\\Documents\\Visual Studio 2022\\Progra 2-linq\\Progra-II-LINQ\\resultados.txt";

            List<Producto> productos = CargarProductos(rutaArchivo);

            // a) Productos con stock menor a 10
            var bajoStock = productos.Where(p => p.Stock < 10);

            // b) Productos ordenados por precio descendente
            var ordenadosPorPrecio = productos.OrderByDescending(p => p.Precio);

            // c) Total del valor del inventario
            var valorTotal = productos.Sum(p => p.Precio * p.Stock);

            // d) Agrupar productos por categoría
            var agrupadosPorCategoria = productos.GroupBy(p => p.Categoria);

            // ======================
            // 📟 Mostrar resultados en consola (mantiene el comportamiento del commit 2)
            // ======================
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

            // ======================
            // 📝 Exportar resultados a archivo (nuevo en commit 3)
            // ======================
            using (StreamWriter sw = new StreamWriter(rutaSalida))
            {
                sw.WriteLine("=== Productos con stock menor a 10 ===");
                foreach (var p in bajoStock) sw.WriteLine(p);

                sw.WriteLine("\n=== Productos ordenados por precio descendente ===");
                foreach (var p in ordenadosPorPrecio) sw.WriteLine(p);

                sw.WriteLine($"\n=== Valor total del inventario: {valorTotal:C} ===");

                sw.WriteLine("\n=== Productos agrupados por categoría ===");
                foreach (var grupo in agrupadosPorCategoria)
                {
                    sw.WriteLine($"\nCategoría: {grupo.Key}");
                    foreach (var p in grupo)
                        sw.WriteLine($"  - {p.Nombre} (${p.Precio})");
                }
            }

            Console.WriteLine($"\nResultados exportados correctamente a '{rutaSalida}'.");
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
