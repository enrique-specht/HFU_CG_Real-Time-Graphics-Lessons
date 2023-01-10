﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace FuseeApp
{
    public class CuboidMesh : Mesh
    {
        public CuboidMesh(float3 size)
        {
            Vertices = new MeshAttributes<float3>(new float3[]
            {
                new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z}
            });

            Triangles = new MeshAttributes<uint>(new uint[]
            {
                // front face
                0, 2, 1, 0, 3, 2,
                // right face
                4, 6, 5, 4, 7, 6,
                // back face
                8, 10, 9, 8, 11, 10,
                // left face
                12, 14, 13, 12, 15, 14,
                // top face
                16, 18, 17, 16, 19, 18,
                // bottom face
                20, 22, 21, 20, 23, 22
            });

            Normals = new MeshAttributes<float3>(new float3[]
            {
                new float3(0, 0, 1),
                new float3(0, 0, 1),
                new float3(0, 0, 1),
                new float3(0, 0, 1),
                new float3(1, 0, 0),
                new float3(1, 0, 0),
                new float3(1, 0, 0),
                new float3(1, 0, 0),
                new float3(0, 0, -1),
                new float3(0, 0, -1),
                new float3(0, 0, -1),
                new float3(0, 0, -1),
                new float3(-1, 0, 0),
                new float3(-1, 0, 0),
                new float3(-1, 0, 0),
                new float3(-1, 0, 0),
                new float3(0, 1, 0),
                new float3(0, 1, 0),
                new float3(0, 1, 0),
                new float3(0, 1, 0),
                new float3(0, -1, 0),
                new float3(0, -1, 0),
                new float3(0, -1, 0),
                new float3(0, -1, 0)
            });

            UVs = new MeshAttributes<float2>(new float2[]
            {
                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0),
                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0),
                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0),
                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0),
                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0),
                new float2(1, 0),
                new float2(1, 1),
                new float2(0, 1),
                new float2(0, 0)
            });
        }
    }

    public class CylinderMesh : Mesh
    {
        public CylinderMesh(float radius, float height, int segments) {
            
            float3[] verts = new float3[segments+1];
            float3[] norms = new float3[segments+1];
            uint[] tris = new uint[segments*3];

            verts[0] = new float3(radius, 0, 0);
            norms[0] = new float3(0, 1, 0);

            verts[segments] = new float3(0, 0, 0);
            norms[segments] = new float3(0, 1, 0);

            float delta = 2 * M.Pi / segments;

            for (int i = 1; i < segments; i++)
            {
                var x = radius * Math.Cos(i*delta);
                var y = radius * Math.Sin(i*delta);

                verts[i] = new float3((float) x, 0,(float) y);

                norms[i] = new float3(0, 1, 0);

                tris[(i-1)*3 + 0] = (uint) i-1;
                tris[(i-1)*3 + 1] = (uint) i;
                tris[(i-1)*3 + 2] = (uint) segments;
            }

            tris[tris.Length-3] = (uint) segments-1;
            tris[tris.Length-2] = 0;
            tris[tris.Length-1] = (uint) segments;
            
            Vertices = new MeshAttributes<float3>(verts);
            Normals = new MeshAttributes<float3>(norms);
            Triangles = new MeshAttributes<uint>(tris);

        }
    }

    public class ConeMesh : ConeFrustumMesh
    {
        public ConeMesh(float radius, float height, int segments) : base(radius, 0.0f, height, segments) { }
    }

    public class ConeFrustumMesh : Mesh
    {
        public ConeFrustumMesh(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }
    }

    public class PyramidMesh : Mesh
    {
        public PyramidMesh(float baselen, float height)
        {
            throw new NotImplementedException();
        }
    }

    public class TetrahedronMesh : Mesh
    {
        public TetrahedronMesh(float edgelen)
        {
            throw new NotImplementedException();
        }
    }

    public class TorusMesh : Mesh
    {
        public TorusMesh(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }
    }
}
