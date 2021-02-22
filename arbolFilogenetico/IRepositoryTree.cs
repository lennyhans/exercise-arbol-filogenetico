using System;
using System.Collections;

namespace arbolFilogenetico
{
    public interface IRepositoryTree
    {
        Nodo Get(string source);

        string Print(string id, Nodo SourceElement);
    }
}
