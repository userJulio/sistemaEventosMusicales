using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public class vehiculo
    {
        public string? Marca { get; set; }
        public int AnioCreacion { get; set; }
        public virtual void DarReversa()
        {
            Console.WriteLine("DAR REVERSA POR DEFECTO");      
        }
    }
    public class carro : vehiculo
    {
        public void encenderRadio()
        {
            Console.WriteLine("Radio encendidad");
        }
        public override void DarReversa()
        {
            base.DarReversa();
            Console.WriteLine("CARRO DIO REVERSA");
        }

    }
    public class biciclete : vehiculo
    {
        public override void DarReversa()
        {
            Console.WriteLine("bicicleta DIO REVERSA");
        }

    }
}
