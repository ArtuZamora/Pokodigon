using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ejercicio_2_Pokodigon
{
    class Program
    {
        static void Main(string[] args)
        {
            Home:
            AddTypes();
            AddAttacks();
            AddPokemons();

            List<Pokemon> machinePokemons = new List<Pokemon>();
            List<Pokemon> playerPokemons = new List<Pokemon>();
            Random rnd = new Random();
            int random;
            string option;

            for (int i = 0; i < 6; i+=2)
            {
                random = rnd.Next(0, Repository.Pokemons.Count);
                if(random % 2 == 0)
                {
                    machinePokemons.Add(Repository.Pokemons[random]);
                    machinePokemons.Add(Repository.Pokemons[random + 1]);
                    Repository.Pokemons.RemoveRange(random, 2);
                }
                else
                {
                    machinePokemons.Add(Repository.Pokemons[random]);
                    machinePokemons.Add(Repository.Pokemons[random - 1]);
                    Repository.Pokemons.RemoveRange(random - 1, 2);
                }
                Thread.Sleep(15);
            }

            playerPokemons.Add(Repository.Pokemons[rnd.Next(0, Repository.Pokemons.Count)]);

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(Menu());
                    Console.Write("Elija una opción: ");
                    option = Console.ReadLine();
                } while (option != "1" && option != "2" && option != "3");
                if(option == "1")
                {
                    Console.Clear();
                    if (machinePokemons.Count() == 0)
                    {
                        Console.WriteLine("Has capturado todos los pokemons!");
                        Console.WriteLine(">Presiona cualquier tecla para reiniciar datos del juego<");
                        goto Home;
                    }
                    int actualFighter = rnd.Next(0,machinePokemons.Count());
                    int actualPlayerFighter = 0;
                    String option2;
                    Console.WriteLine("¡¡¡¡¡¡Has encontrado a un {0}!!!!!", machinePokemons[actualFighter].Name);
                    Thread.Sleep(20);
                    int turn = rnd.Next(0, 2); //0 PC, 1 Jugador
                    do
                    {
                        if (turn == 1)
                        {
                            Console.WriteLine("\nEs tu turno de atacar");
                            do
                            {
                                Console.WriteLine("\nEstás jugando con un {0} (con {1} de vida)", 
                                    playerPokemons[actualPlayerFighter].Name,
                                    playerPokemons[actualPlayerFighter].HP);
                                Console.WriteLine(MenuInGame());
                                Console.Write("Elija una opción: ");
                                option2 = Console.ReadLine();
                                while (option2 != "1" && option2 != "2" && option2 != "3" && option2 != "4")
                                {
                                    Console.WriteLine("Opcion no contemplada.");
                                    Console.Write("Elija una opción: ");
                                    option2 = Console.ReadLine();
                                }
                                switch (option2)
                                {
                                    case "1":
                                        Console.WriteLine("\n\n--->Lista de Ataques:");
                                        Console.WriteLine("Ataque 1: {0}. Daño: {1}",
                                            playerPokemons[actualPlayerFighter].Atk1.AtkName,
                                            playerPokemons[actualPlayerFighter].Atk1.Value);
                                        Console.WriteLine("Ataque 2: {0}. Daño: {1}",
                                            playerPokemons[actualPlayerFighter].Atk2.AtkName,
                                            playerPokemons[actualPlayerFighter].Atk2.Value);
                                        break;
                                    case "2":
                                        PrintPokemons(playerPokemons);
                                        Console.WriteLine("Elija un pokemon (elija entre el rango de la cantidad de pokemones que posee): ");
                                        string option3 = Console.ReadLine();
                                        while (!(Convert.ToInt32(option3) > 0 && Convert.ToInt32(option3) <= playerPokemons.Count))
                                        {
                                            Console.WriteLine("Opcion no contemplada.");
                                            Console.Write("Ingrese otra opción: ");
                                            option3 = Console.ReadLine();
                                        }
                                        actualPlayerFighter = Convert.ToInt32(option3) - 1;
                                        break;
                                    case "3":
                                        Console.WriteLine("\nHas atacado con {0}", playerPokemons[actualPlayerFighter].Atk1.AtkName);
                                        machinePokemons[actualFighter].HP -= playerPokemons[actualPlayerFighter].Atk1.Value;
                                        Console.WriteLine("\n{0} ataca, haciendo {1} de daño", 
                                            playerPokemons[actualPlayerFighter].Name,
                                            playerPokemons[actualPlayerFighter].Atk1.Value);
                                        Thread.Sleep(2000);
                                        break;
                                    default:
                                        Console.WriteLine("\nHas atacado con {0}", playerPokemons[actualPlayerFighter].Atk2.AtkName);
                                        machinePokemons[actualFighter].HP -= playerPokemons[actualPlayerFighter].Atk2.Value;
                                        Console.WriteLine("\n{0} ataca, haciendo {1} de daño",
                                            playerPokemons[actualPlayerFighter].Name,
                                            playerPokemons[actualPlayerFighter].Atk2.Value);
                                        Thread.Sleep(2000);
                                        break;
                                }
                            } while (option2 == "1" || option2 == "2");
                            if (machinePokemons[actualFighter].HP <= 0)
                            {
                                machinePokemons[actualFighter].HP = 150;
                                random = rnd.Next(0, 2); //0 no lo capturó, 1 lo capturó
                                Console.WriteLine("\nHas derrotado al pokemon, tienes la oportunidad de capturarlo");
                                Console.WriteLine(">Presiona alguna tecla para averiguar si lo has cazado<");
                                Console.ReadKey();
                                if (random == 1)
                                {
                                    playerPokemons.Add(machinePokemons[actualFighter]);
                                    machinePokemons.RemoveAt(actualFighter);
                                    Console.WriteLine("\nLo has logrado capturar, ahora es tu pokemon");
                                }
                                else Console.WriteLine("\nNo lo has capturado, buena suerte la proxima!!");
                                Console.WriteLine("\n>Presiona alguna tecla para continuar<");
                                Console.ReadKey();
                                break;
                            }
                            turn = 0;
                        }
                        else
                        {
                            Console.WriteLine("\nEs el turno de la computadora\n");
                            Console.WriteLine("\nLa computadora juega con un {0} (con {1} de vida)\n",
                                machinePokemons[actualFighter].Name,
                                machinePokemons[actualFighter].HP);
                            Thread.Sleep(2000);
                            random = rnd.Next(1,3); //1 ataque 1, 2 ataque 2
                            if(random == 1)
                            {
                                Console.WriteLine("\n{0} ataca con {1}, haciendo {2} de daño",
                                    machinePokemons[actualFighter].Name,
                                    machinePokemons[actualFighter].Atk1.AtkName,
                                    machinePokemons[actualFighter].Atk1.Value);
                                playerPokemons[actualPlayerFighter].HP -= machinePokemons[actualFighter].Atk1.Value;
                            }
                            else
                            {
                                Console.WriteLine("\n{0} ataca con {1}, haciendo {2} de daño",
                                    machinePokemons[actualFighter].Name,
                                    machinePokemons[actualFighter].Atk2.AtkName,
                                    machinePokemons[actualFighter].Atk2.Value);
                                playerPokemons[actualPlayerFighter].HP -= machinePokemons[actualFighter].Atk2.Value;
                            }
                            Thread.Sleep(3000);
                            if(playerPokemons[actualPlayerFighter].HP <= 0)
                            {
                                Console.WriteLine("Has perdido el juego, ¡buena suerte la proxima!");
                                machinePokemons.Clear();
                                playerPokemons.Clear();
                                Repository.AtkV.Clear();
                                Repository.Pokemons.Clear();
                                Repository.Type.Clear();
                                Thread.Sleep(5000);
                                goto Home;
                            }
                            turn = 1;
                        }
                    } while(machinePokemons[actualFighter].HP > 0 && playerPokemons[actualPlayerFighter].HP > 0);
                }
                else if(option == "2")
                {
                    Console.Clear();
                    PrintPokemons(playerPokemons);
                    Console.WriteLine("\n>Presione cualquier tecla para continuar<");
                    Console.ReadKey();
                }
            } while (option != "3");
        }
        public static void AddTypes()
        {
            Repository.Type.Add("Bicho"); //0
            Repository.Type.Add("Dragón"); //1
            Repository.Type.Add("Eléctrico"); //2
            Repository.Type.Add("Hada"); //3
            Repository.Type.Add("Lucha"); //4

            Repository.Type.Add("Fuego"); //5
            Repository.Type.Add("Volador"); //6
            Repository.Type.Add("Fantasma"); //7
            Repository.Type.Add("Planta"); //8
            Repository.Type.Add("Tierra"); //9

            Repository.Type.Add("Hielo"); //10
            Repository.Type.Add("Normal"); //11
            Repository.Type.Add("Veneno"); //12
            Repository.Type.Add("Psíquico"); //13
            Repository.Type.Add("Roca"); //14

            Repository.Type.Add("Acero"); //15
            Repository.Type.Add("Agua"); //16
        }
        public static void AddAttacks()
        {
            Repository.AtkV.Add(new Attacks("Picadura", Repository.Type[0]));
            Repository.AtkV.Add(new Attacks("Estoicismo", Repository.Type[0]));
            Repository.AtkV.Add(new Attacks("Garra Dragón", Repository.Type[1]));
            Repository.AtkV.Add(new Attacks("Ciclón", Repository.Type[1]));

            Repository.AtkV.Add(new Attacks("Voltiocambio", Repository.Type[2]));
            Repository.AtkV.Add(new Attacks("Voltio Cruel", Repository.Type[2]));
            Repository.AtkV.Add(new Attacks("Carantoña", Repository.Type[3]));
            Repository.AtkV.Add(new Attacks("Brillo Mágico", Repository.Type[3]));

            Repository.AtkV.Add(new Attacks("A Bocajarro", Repository.Type[4]));
            Repository.AtkV.Add(new Attacks("Plancha Voladora", Repository.Type[4]));
            Repository.AtkV.Add(new Attacks("Meteorobola", Repository.Type[5]));
            Repository.AtkV.Add(new Attacks("Patada Ígnea", Repository.Type[5]));

            Repository.AtkV.Add(new Attacks("Pájaro Osado", Repository.Type[6]));
            Repository.AtkV.Add(new Attacks("Ataque Ala", Repository.Type[6]));
            Repository.AtkV.Add(new Attacks("Sombra Vil", Repository.Type[7]));
            Repository.AtkV.Add(new Attacks("Hueso Sombrío", Repository.Type[7]));
        
            Repository.AtkV.Add(new Attacks("Latigazo", Repository.Type[8]));
            Repository.AtkV.Add(new Attacks("Semilladora", Repository.Type[8]));
            Repository.AtkV.Add(new Attacks("Disparo Lodo", Repository.Type[9]));
            Repository.AtkV.Add(new Attacks("Tierra Viva", Repository.Type[9]));

            Repository.AtkV.Add(new Attacks("Alud", Repository.Type[10]));
            Repository.AtkV.Add(new Attacks("Rayo Aurora", Repository.Type[10]));
            Repository.AtkV.Add(new Attacks("Pisotón", Repository.Type[11]));
            Repository.AtkV.Add(new Attacks("Hipercolmillo", Repository.Type[11]));

            Repository.AtkV.Add(new Attacks("Residuos", Repository.Type[12]));
            Repository.AtkV.Add(new Attacks("Bomba Ácida", Repository.Type[12]));
            Repository.AtkV.Add(new Attacks("Manto Espejo", Repository.Type[13]));
            Repository.AtkV.Add(new Attacks("Psicoataque", Repository.Type[13]));

            Repository.AtkV.Add(new Attacks("Joya de Luz", Repository.Type[14]));
            Repository.AtkV.Add(new Attacks("Pedrada", Repository.Type[14]));
            Repository.AtkV.Add(new Attacks("Foco Resplandor", Repository.Type[15]));
            Repository.AtkV.Add(new Attacks("Cuerpo Pesado", Repository.Type[15]));

            Repository.AtkV.Add(new Attacks("Meteorobola", Repository.Type[16]));
            Repository.AtkV.Add(new Attacks("Acua Cola", Repository.Type[16]));
        }
        public static void AddPokemons()
        {
            Repository.Pokemons.Add(new Pokemon("Caterpie", Repository.Type[0]));
            Repository.Pokemons.Add(new Pokemon("Metapod", Repository.Type[0]));
            Repository.Pokemons.Add(new Pokemon("Shelgon", Repository.Type[1]));
            Repository.Pokemons.Add(new Pokemon("Druddigon", Repository.Type[1]));
       
            Repository.Pokemons.Add(new Pokemon("Magneton", Repository.Type[2]));
            Repository.Pokemons.Add(new Pokemon("Zapdos", Repository.Type[2]));
            Repository.Pokemons.Add(new Pokemon("Clefable", Repository.Type[3]));
            Repository.Pokemons.Add(new Pokemon("Xerneas", Repository.Type[3]));

            Repository.Pokemons.Add(new Pokemon("Poliwrath", Repository.Type[4]));
            Repository.Pokemons.Add(new Pokemon("Machamp", Repository.Type[4]));
            Repository.Pokemons.Add(new Pokemon("Growlithe", Repository.Type[5]));
            Repository.Pokemons.Add(new Pokemon("Torchic", Repository.Type[5]));
        
            Repository.Pokemons.Add(new Pokemon("Noctowl", Repository.Type[6]));
            Repository.Pokemons.Add(new Pokemon("Articuno", Repository.Type[6]));
            Repository.Pokemons.Add(new Pokemon("Giratina", Repository.Type[7]));
            Repository.Pokemons.Add(new Pokemon("Gengar", Repository.Type[7]));
        
            Repository.Pokemons.Add(new Pokemon("Tangela", Repository.Type[8]));
            Repository.Pokemons.Add(new Pokemon("Leafeon", Repository.Type[8]));
            Repository.Pokemons.Add(new Pokemon("Donphan", Repository.Type[9]));
            Repository.Pokemons.Add(new Pokemon("Garchomp", Repository.Type[9]));

            Repository.Pokemons.Add(new Pokemon("Weavile", Repository.Type[10]));
            Repository.Pokemons.Add(new Pokemon("Lapras", Repository.Type[10]));
            Repository.Pokemons.Add(new Pokemon("Eevee", Repository.Type[11]));
            Repository.Pokemons.Add(new Pokemon("Tauros", Repository.Type[11]));

            Repository.Pokemons.Add(new Pokemon("Scolipede", Repository.Type[12]));
            Repository.Pokemons.Add(new Pokemon("Nidoqueen", Repository.Type[12]));
            Repository.Pokemons.Add(new Pokemon("Slowbro", Repository.Type[13]));
            Repository.Pokemons.Add(new Pokemon("Exeggcute", Repository.Type[13]));

            Repository.Pokemons.Add(new Pokemon("Gigalith", Repository.Type[14]));
            Repository.Pokemons.Add(new Pokemon("Rampardos", Repository.Type[14]));
            Repository.Pokemons.Add(new Pokemon("Steelix", Repository.Type[15]));
            Repository.Pokemons.Add(new Pokemon("Mawile", Repository.Type[15]));

            Repository.Pokemons.Add(new Pokemon("Blastoise", Repository.Type[16]));
            Repository.Pokemons.Add(new Pokemon("Poliwag", Repository.Type[16]));
        }
        public static string Menu()
        {
            StringBuilder menuStr = new StringBuilder();
            menuStr.Append("+++++++++++ MENU +++++++++++\n\n");
            menuStr.Append(" 1 - Tratar de atrapar un pokemon\n\n");
            menuStr.Append(" 2 - Ver mis pokemones\n\n");
            menuStr.Append(" 3 - Salir\n");
            return menuStr.ToString();
        }
        public static string MenuInGame()
        {
            StringBuilder menuStr = new StringBuilder();
            menuStr.Append("\n+++++++++++ Menu de jugador +++++++++++\n");
            menuStr.Append(" 1 - Ver Ataques\n");
            menuStr.Append(" 2 - Cambiar de pokemon\n");
            menuStr.Append(" 3 - Ataque 1\n");
            menuStr.Append(" 4 - Ataque 2\n");
            return menuStr.ToString();
        }
        public static void PrintPokemons(List<Pokemon> pokemons)
        {
            Console.WriteLine("-------- Lista de Pokemons --------\n");
            foreach (Pokemon pok in pokemons)
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("Nombre: {0}", pok.Name);
                Console.WriteLine("Tipo: {0}", pok.Type);
                Console.WriteLine("Vida: {0}", pok.HP);
                Console.WriteLine("Ataque 1: {0}", pok.Atk1.AtkName);
                Console.WriteLine("Ataque 2: {0}", pok.Atk2.AtkName);
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            }
        }
    }
}
