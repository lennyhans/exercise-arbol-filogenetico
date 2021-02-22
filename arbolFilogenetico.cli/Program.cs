using System;
using arbolFilogenetico;

namespace arbolFilogenetico.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepositoryTree repoTree = new RepositoryTreeFile();
            var UI = new GUI( repoTree, "Input.txt");
            UI.init();
            UI.Loop();
        }

    }
}
