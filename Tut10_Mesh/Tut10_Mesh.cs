﻿using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut10_Mesh", Description = "Yet another FUSEE App.")]
    public class Tut10_Mesh : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private Transform _baseTransform;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"

            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children =
                {
                    new SceneNode
                    {
                        Name = "Camera",
                        Components =
                        {
                            new Transform
                            {
                                Translation = new float3(0, 10, -30),
                                Rotation = new float3(0.25f, 0, 0)
                            },
                            new Camera(ProjectionMethod.Perspective, 5, 100, M.PiOver4)
                            {
                                BackgroundColor =  (float4) ColorUint.Greenery
                            }
                        }
                    },

                    new SceneNode
                    {
                        Components =
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),

                            // MESH COMPONENT
                            new CylinderMesh(5, 10, 8)
                        }
                    },
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            //_baseTransform.Rotation = new float3(0, M.MinAngle(TimeSinceStart), 0);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

    }
}