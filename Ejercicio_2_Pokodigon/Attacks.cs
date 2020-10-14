using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ejercicio_2_Pokodigon
{
    public class Attacks
    {
        private string typeOfAtk;
        private string atkName;
        private int value;

        public string AtkName
        {
            get
            {
                return atkName;
            }

            set
            {
                atkName = value;
            }
        }
        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
        public string TypeOfAtk
        {
            get
            {
                return typeOfAtk;
            }

            set
            {
                typeOfAtk = value;
            }
        }

        public Attacks(string AttackName, string type)
        {
            atkName = AttackName;
            value = setAtkValue();
            typeOfAtk = type;
        }
        private int setAtkValue()
        {
            Thread.Sleep(20);
            Random rndValueAtk = new Random();
            return rndValueAtk.Next(15,66);
        }
    }
}
