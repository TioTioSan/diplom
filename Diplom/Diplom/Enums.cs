using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diplom
{
    public enum Axis
    {
        X,
        Y,
        Z,
        XY,
        ZX,
        YZ,
        XYZ,
        None
    }

    public enum TransformationMode : int
    {
        Translate = 0,
        Rotate = 1,
        Scale = 2
    }

    public enum SubObjectMode : int
    {
        None = 0,
        Vertex = 1,
        Edge = 2,
        Triangle = 3
    }

    public enum DrawMode : int
    {
        EntityOnly = 0,
        WithEdges = 1
    }
}
