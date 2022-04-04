using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceAPI.Model
{
    public class Lga
    {
        public int LgaId { get; set; }
        public int StateId { get; set; }
        public string LgaName { get; set; }

        public Lga(int LgaId, int StateId, string LgaName)
        {
            LgaId = LgaId;
            StateId = StateId;
            LgaName = LgaName;
        }
        public static List<Lga> GetAllLgas()
        {
            List<Lga> listLga = new List<Lga>();
            listLga.Add(new Lga(1, 1, "Alimosho"));
            listLga.Add(new Lga(2, 1, "Ikeja"));
            listLga.Add(new Lga(3, 1, "Maryland"));
            listLga.Add(new Lga(4, 1, "Ojodu"));
            listLga.Add(new Lga(5, 1, "Ojokoro"));
            listLga.Add(new Lga(1, 2, "Eleyele"));
            listLga.Add(new Lga(2, 2, "Ibadan East"));
            listLga.Add(new Lga(3, 2, "Ibadan North"));
            listLga.Add(new Lga(4, 2, "Ibadan South"));
            listLga.Add(new Lga(5, 2, "Oluyode"));
            listLga.Add(new Lga(1, 3, "Ekiti"));
            listLga.Add(new Lga(2, 3, "Ifelodun"));
            listLga.Add(new Lga(3, 3, "Irepodun"));
            listLga.Add(new Lga(4, 3, "Isin"));
            listLga.Add(new Lga(5, 3, "Offa"));
            listLga.Add(new Lga(1, 4, "Atakunmosa West"));
            listLga.Add(new Lga(2, 4, "Ayedaade"));
            listLga.Add(new Lga(3, 4, "Ayedire"));
            listLga.Add(new Lga(4, 4, "Boluwaduro"));
            listLga.Add(new Lga(5, 4, "Boripe"));
            return listLga;
                   }
    }
}