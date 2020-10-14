using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_2_Pokodigon
{
    public class Pokemon
    {
        private string name;
        private string type;
        private int hp;
        private Attacks atk1 = null;
        private Attacks atk2 = null;

        public string Name
        {
            get
            {
                return name;
            }
        }
        public int HP
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
        public Attacks Atk1
        {
            get
            {
                return atk1;
            }
        }
        public Attacks Atk2
        {
            get
            {
                return atk2;
            }
        }

        public Pokemon(string name, string type)
        {
            int index = 0;
            this.name = name;
            this.type = type;
            hp = 150;
            foreach (Attacks atk in Repository.AtkV)
            {
                if(atk.TypeOfAtk == type)
                {
                    break;
                }
                index++;
            }
            atk1 = Repository.AtkV[index];
            atk2 = Repository.AtkV[index + 1];
        }
    }
}