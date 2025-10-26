namespace GestionAcademicaLINQ
{
    public class Estudiante : Persona
    {
        public string Curso { get; set; }
        public double Nota { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Nombre} | Curso: {Curso} | Nota: {Nota}";
        }
    }
}
