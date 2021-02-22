using System;
using System.Linq;

namespace arbolFilogenetico.cli
{
    public class GUI {

        public static string HR_CHAR = "=";
        public static int HR_WIDTH = 50;
        public static string[] EXIT_ENTRY = new string[]{ "q", "Q", "Exit" };

        private string exit_words { get; set; } 
        private string hr_bar { get; set; }
        private IRepositoryTree repoTree { get; set; }
        private string ResourceName { get; set; }
        private Nodo TreeRoot { get; set; }

        public GUI( 
            IRepositoryTree repoTree,
            string ResourceName
        ){
            this.repoTree = repoTree;
            this.ResourceName = ResourceName;
        }
        public string DisplayInstruction(){
            
            return hr_bar + "\n" +
                "Para navegar por los registros del árbol ingrese un" +
                " identificador de especie (ej. 1.1), puede salir en" +
                "cualquier momento presionando 'ctrl + c' " + exit_words +
                "\n" + hr_bar + "\n";
        }

        public string DisplayEntryWait(){
            return ""+
                "Ingrese identificador : ";
        }

        public string DisplayBadDataMessage(){
            return "" +
                "El identificador no es válido, debería ingresar algo con" + 
                " la forma <numero>.<numero>.<numero>, ejemplo 1.2.1." + 
                " Vuelva a intentarlo";
        }

        public string DisplayNotFoundNodes(){
            return "" +
                "No se han encontrado registros para el identificador ingresado";
        }

        public string DisplayExitMessage(){
            return "" + "\n\n" +
                "Saliendo de la aplicación.";
        }
        public string WaitInput(){
            Console.Write(DisplayEntryWait());
            var readedValue = Console.ReadLine().ToString();
            if(EXIT_ENTRY.Contains(readedValue))
                return readedValue;
            int iAux = 0;
            var option = int.TryParse(readedValue.Replace(".",""), out iAux ) ? iAux : -1;
            if( option == -1 ){
                Console.WriteLine(DisplayBadDataMessage());
                return "";
            }
            return readedValue;
            
        }

        public void init(){
            this.TreeRoot = repoTree.Get(this.ResourceName);

            hr_bar = "";
            for(int i = 0; i < HR_WIDTH; i++){
                hr_bar += HR_CHAR;
            }

            exit_words = "";
            for( int i = 0; i < EXIT_ENTRY.Length; i++){
                exit_words += $" '{EXIT_ENTRY[i]}'";
            }

            Console.Write(DisplayInstruction());
        }

        public void Loop(){
            var input = "";
            while( !EXIT_ENTRY.Contains(input) ){
                input = WaitInput();
                if(EXIT_ENTRY.Contains(input))
                    continue;

                var subNodes = this.TreeRoot.getNode(input);
                if(subNodes == null){
                    Console.WriteLine(DisplayNotFoundNodes());
                    input = "";
                    continue;
                }
                Console.WriteLine(subNodes.PrintNodo());

            }
            Console.WriteLine(DisplayExitMessage());
        }
    }
}