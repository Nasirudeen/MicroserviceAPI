using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceAPI.Models
{
    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }

        public State(int StatedId, string StateName)
        {
            StateId = StateId;
            StateName = StateName;
        }

        public static List<State> GetAllStates()
        {
            List<State> listStates = new List<State>();
            listStates.Add(new State(1, "LAGOS"));
            listStates.Add(new State(2, "OYO"));
            listStates.Add(new State(3, "KWARA"));
            listStates.Add(new State(4, "OSUN"));
            return listStates;
        }
    }
}
