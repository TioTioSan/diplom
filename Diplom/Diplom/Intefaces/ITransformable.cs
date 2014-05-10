using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Diplom.Intefaces
{
    public interface ITransformable
    {
        string Name { get; set; }

        Vector3 Position { get; set; }
        Vector3 Scale { get; set; }

        Quaternion Orientation { get; set; }

        Vector3 Forward { get; set; }
        Vector3 Up { get; set; }

        BoundingBox BoundingBox { get; }

        float? Select(Ray selectionRay);
    }
}
