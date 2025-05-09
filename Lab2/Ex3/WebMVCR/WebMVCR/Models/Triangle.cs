namespace WebMVCR.Models
{
    public class Triangle : Shape
    {
        //public double Sta { get; set; }
        public double Stb { get; set; }
        public double Stc { get; set; }

        //public string Name
        //{
        //    get
        //    {
        //        return String.Format("\"Треугольник со сторонами {0}, {1} и {2}\"", St, Stb, Stc);
        //    }
        //}
        override public string Name 
        { 
            get 
            { 
                return String.Format("\"Треугольник со сторонами {0}, {1} и {2}\"", St, Stb, Stc); 
            }
        }
        public double Perimeter
        {
            get
            {
                double p = St + Stb + Stc; 
                return p;
            }
        }
        public double Area
        {
            get
            {
                double sq = Math.Sqrt(Perimeter / 2 * (Perimeter / 2 - St) * (Perimeter / 2 - Stb) * (Perimeter / 2 - Stc));
                return sq;
            }
        }

        public Triangle(double a, double b, double c) 
        { 
            St = a; 
            Stb = b; 
            Stc = c; 
        }
    }
}
