using System;
using System.Collections.Generic;

namespace arbolFilogenetico
{
    public class Nodo
    {
        public static string PADDER_STRING = "  ";
        public string data { get; set; }
        public string id { get; set; }
        public List<Nodo> childs { get; set; }

        public Nodo(string id, string data){
            this.id = id;
            this.data = data;
            childs = new List<Nodo>();
        }

        public string PrintNodo(int? padding = null){
            var val = "";
            if(padding.HasValue && padding.Value > 0)
                for( int i = 0; i < padding.Value; i++)
                    val += PADDER_STRING;
            val += $"{this.data}\n";
            foreach (var item in this.childs)
            {
                val +=  item.PrintNodo(padding.HasValue ? padding +1 : string.IsNullOrEmpty(this.data) ? 0 : 1);
            }
            return val;
        }

        public string getParentId(){
            var endPosition = this.id.LastIndexOf(".");
            return this.id.Substring( 0,  endPosition);
        }

        public bool tryAppend(Nodo inode){
            var ParentId = inode.getParentId();
            var ParentNode = getNode(ParentId);
            if(ParentNode == null)
                return false;
            ParentNode.childs.Add(inode);
            return true;
        }

        public Nodo getNode( string id, Nodo inner = null){
            if( inner == null)
                inner = this;
            if( id == inner.id )
                return inner;
            foreach (var item in inner.childs)
            {
                if(item.id == id)
                    return item;

                if(isOnBranch(item.id, id))
                {
                    return getNode(id, item);
                }
            }
            return null;
        }
        public bool isOnBranch( string idBranch, string idChild){
            return idChild.IndexOf(idBranch) == 0;
        }
    }
}
