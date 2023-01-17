using System;
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
            
            float3[] verts = new float3[4*segments+2];
            float3[] norms = new float3[4*segments+2];
            uint[] tris = new uint[4*segments*3];

            float delta = 2 * M.Pi / segments;

            //Top
            verts[0] = new float3(radius, 0.5f * height, 0);
            norms[0] = new float3(0, 1, 0);

            verts[4*segments+1] = new float3(0, 0.5f * height, 0);
            norms[4*segments+1] = new float3(0, 1, 0);

            //Bottom
            verts[segments] = new float3(radius, -0.5f * height, 0);
            norms[segments] = new float3(0, -1, 0);

            verts[4*segments] = new float3(0, -0.5f * height, 0);
            norms[4*segments] = new float3(0, -1, 0);

            //Top Side
            verts[2*segments] = new float3(radius, 0.5f * height, 0);
            norms[2*segments] = new float3(1, 0, 0);

            //Bottom Side
            verts[3*segments] = new float3(radius, -0.5f * height, 0);
            norms[3*segments] = new float3(1, 0, 0);


            for (int i = 1; i < segments; i++)
            {
                var x = radius * Math.Cos(i*delta);
                var y = radius * Math.Sin(i*delta);

                var x_normale = Math.Cos(i*delta);
                var y_normale = Math.Sin(i*delta);

                //Top
                verts[i] = new float3((float) x, 0.5f * height,(float) y);

                norms[i] = new float3(0, 1, 0);

                tris[(i-1)*3 + 0] = (uint) i-1;
                tris[(i-1)*3 + 1] = (uint) i;
                tris[(i-1)*3 + 2] = (uint) (4*segments+1);

                //Bottom
                verts[segments+i] = new float3((float) x, -0.5f * height,(float) y);

                norms[segments+i] = new float3(0, -1, 0);

                tris[(segments+i-1)*3 + 0] = (uint) (segments+i-1);
                tris[(segments+i-1)*3 + 1] = (uint) (segments+i);
                tris[(segments+i-1)*3 + 2] = (uint) (4*segments);

                //Side
                verts[2*segments+i] = new float3((float) x, 0.5f * height,(float) y);
                verts[3*segments+i] = new float3((float) x, -0.5f * height,(float) y);

                norms[2*segments+i] = new float3((float) x_normale, 0, (float) y_normale);
                norms[3*segments+i] = new float3((float) x_normale, 0, (float) y_normale);

                tris[(2*segments+i-1)*3 + 0] = (uint) (2*segments+i-1);
                tris[(2*segments+i-1)*3 + 1] = (uint) (3*segments+i-1);
                tris[(2*segments+i-1)*3 + 2] = (uint) (2*segments+i);

                tris[(3*segments+i-1)*3 + 0] = (uint) (3*segments+i-1);
                tris[(3*segments+i-1)*3 + 1] = (uint) (3*segments+i);
                tris[(3*segments+i-1)*3 + 2] = (uint) (2*segments+i);

            }

            //Top
            tris[3*segments-3] = (uint) segments-1;
            tris[3*segments-2] = 0;
            tris[3*segments-1] = (uint) (4*segments+1);

            //Bottom
            tris[2*3*segments-3] = (uint) (2*segments-1);
            tris[2*3*segments-2] = (uint) segments;
            tris[2*3*segments-1] = (uint) (4*segments);

            //Side
            tris[3*3*segments-3] = (uint) (4*segments-1);
            tris[3*3*segments-2] = (uint) (2*segments);
            tris[3*3*segments-1] = (uint) (3*segments-1);

            tris[4*3*segments-3] = (uint) (3*segments);
            tris[4*3*segments-2] = (uint) (2*segments);
            tris[4*3*segments-1] = (uint) (4*segments-1);

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
            Vertices = new MeshAttributes<float3>(new float3[]
            {
                //base
                new float3 {x = +0.5f * baselen, y = 0, z = +0.5f * baselen},
                new float3 {x = -0.5f * baselen, y = 0, z = +0.5f * baselen},
                new float3 {x = -0.5f * baselen, y = 0, z = -0.5f * baselen},
                new float3 {x = +0.5f * baselen, y = 0, z = -0.5f * baselen},
                //base for connecting to top
                new float3 {x = +0.5f * baselen, y = 0, z = 0},
                new float3 {x = +0.5f * baselen, y = 0, z = +0.5f * baselen},
                new float3 {x = +0.5f * baselen, y = 0, z = +0.5f * baselen},
                new float3 {x = 0, y = 0, z = +0.5f * baselen},
                new float3 {x = -0.5f * baselen, y = 0, z = +0.5f * baselen},
                new float3 {x = -0.5f * baselen, y = 0, z = +0.5f * baselen},
                new float3 {x = -0.5f * baselen, y = 0, z = 0},
                new float3 {x = -0.5f * baselen, y = 0, z = -0.5f * baselen},
                new float3 {x = -0.5f * baselen, y = 0, z = -0.5f * baselen},
                new float3 {x = 0, y = 0, z = -0.5f * baselen},
                new float3 {x = +0.5f * baselen, y = 0, z = -0.5f * baselen},
                new float3 {x = +0.5f * baselen, y = 0, z = -0.5f * baselen},
                //top
                new float3 {x = 0, y = height, z = 0},
            });

            var ak = baselen/2;
            var gk = height;
            var ht = Math.Sqrt(Math.Pow(ak, 2) + Math.Pow(gk, 2));

            Normals = new MeshAttributes<float3>(new float3[]
            {
                //base
                new float3(0, -1, 0),
                new float3(0, -1, 0),
                new float3(0, -1, 0),
                new float3(0, -1, 0),
                //base for connecting to top
                new float3((float)Math.Cos(ak/ht), 0, 0),
                new float3((float)Math.Cos(ak/ht), 0, 0),
                new float3(0, 0, (float)Math.Sin(gk/ht)),
                new float3(0, 0, (float)Math.Sin(gk/ht)),
                new float3(0, 0, (float)Math.Sin(gk/ht)),
                new float3(-(float)Math.Cos(ak/ht), 0, 0),
                new float3(-(float)Math.Cos(ak/ht), 0, 0),
                new float3(-(float)Math.Cos(ak/ht), 0, 0),
                new float3(0, 0, -(float)Math.Sin(gk/ht)),
                new float3(0, 0, -(float)Math.Sin(gk/ht)),
                new float3(0, 0, -(float)Math.Sin(gk/ht)),
                new float3((float)Math.Cos(ak/ht), 0, 0),
                //top
                new float3(0, 1, 0),
            });

            Triangles = new MeshAttributes<uint>(new uint[]
            {
                // base
                0, 1, 2, 2, 3, 0,
                //base to top
                4, 5, 16,
                6, 7, 16, 7, 8, 16,
                9, 10, 16, 10, 11, 16,
                11, 12, 16, 12, 13, 16,
                13, 14, 16, 14, 15, 16,
                15, 4, 16
            });

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
