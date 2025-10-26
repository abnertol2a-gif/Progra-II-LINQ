using System;

namespace Ejercicio2_LINQ
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Nombre} ({Categoria}) | Precio: {Precio:C} | Stock: {Stock}";
        }
    }
}