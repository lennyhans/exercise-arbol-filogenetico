using System;
using System.Collections.Generic;
using System.Linq;

namespace arbolFilogenetico
{
    public class RepositoryTreeFile : IRepositoryTree
    {
        public Nodo Get(string source){
            
            List<Nodo> raw_nodes = new List<Nodo>();
            string line;
            using( var file = new System.IO.StreamReader(@"" + source)){
                while((line = file.ReadLine()) != null)  
                {  
                    raw_nodes.Add(parseNode(line));
                } 
            }

            return createTree(raw_nodes);

        }

        public Nodo createTree(List<Nodo> unorderedList){
            if(unorderedList == null || unorderedList.Count() == 0)
                return null;

            var root = new Nodo(null, null);
            root.childs = new List<Nodo>();
            var registry = new Dictionary<string, bool>();
            foreach (var node in unorderedList)
                registry.Add(node.id, false);

            while( registry.Values.Contains(false))
            {
                foreach (var node in unorderedList
                    .Where(n => registry[n.id] == false )
                    .OrderBy(n => n.id).ToList())
                {
                    if(countLevel(node.id, '.') == 0){
                        root.childs.Add(node);
                        registry[node.id] = true;
                        continue;
                    }
                    
                    registry[node.id] = root.tryAppend(node);
                    
                }
            }
            return root;
        }

        public Nodo getNode( string id, Nodo source ){
            return source.getNode(id);
        }

        public bool isOnBranch( string idBranch, string idChild){
            return idChild.IndexOf(idBranch) == 0;
        }

        public bool isParent( string idChild, string idParent){
            var possibleParentId = getParentId(idChild);
            return idParent == possibleParentId;
        }
        
        public string getParentId(string idChild){
            var endPosition = idChild.LastIndexOf(".");
            return idChild.Substring( 0,  endPosition);
        }

        public int countLevel(string phrase, char lookupChar)
        {
            var arrString = phrase.ToCharArray();
            int found = 0;
            for(int i = 0, l = arrString.Length; i < l; i++){
                if(arrString[i] == lookupChar)
                    found++;
            }
            return found;
        }

        public Nodo parseNode(string line){
            // Form: 1.2.3.4.5.6.7.6.N, Name 1.2.3.4 (...)
            var values = line.Split(",");
            return new Nodo(values[0], values[1]);
        }

        public string Print(string id, Nodo SourceElement ){
            return "";
        }

    }
}