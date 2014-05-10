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

    public enum TransformationMode
    {
        Translate,
        Rotate,
        Scale
    }

    public enum SubObjectMode
    {
        None,
        Vertex,
        Edge,
        Triangle
    }
}
