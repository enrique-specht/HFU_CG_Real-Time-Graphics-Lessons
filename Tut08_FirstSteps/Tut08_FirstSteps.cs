using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
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
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;

        private Camera _camera;
        private Transform[] _cubeTransformCircle;
        private Transform _cameraTransform;
        private SurfaceEffect[] _cubeEffectCircle;
        private Transform _cubeTransformMiddle;
        private SurfaceEffect _cubeEffectMiddle;
        private Transform _cubeTransformBottom;
        private SurfaceEffect _cubeEffectBottom;
        private float _cubeAngle = 0;
        private float4 _colorSwitch;

        // Init is called on startup. 
        public override void Init()
        {
            // Create a scene tree containing the camera :
            // _scene---+
            //          |
            //          +---cameraNode-----_camera

            // THE CAMERA
            // A node containing one Camera component.
            _camera = new Camera(ProjectionMethod.Perspective, 5, 100, M.PiOver4)
            {
                BackgroundColor = (float4)ColorUint.WhiteSmoke
            };

            _cameraTransform = new Transform() {Translation = new float3(0, 0, -50)};

            var cameraNode = new SceneNode();
            cameraNode.Components.Add(_cameraTransform);
            cameraNode.Components.Add(_camera);

            // CUBE MESH
            var cubeMesh = new CuboidMesh(new float3(10, 10, 10));

            // MIDDLE CUBE
            _cubeTransformMiddle = new Transform() 
            {
                Scale = new float3(1, 0.3f, 0.3f),
                Translation = new float3(0, 0, 0),
                Rotation = new float3((float) Math.PI/4, 0, 0)
            };
            _cubeEffectMiddle = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Black);

            var cubeNodeMiddle = new SceneNode();
            cubeNodeMiddle.Components.Add(_cubeTransformMiddle);
            cubeNodeMiddle.Components.Add(_cubeEffectMiddle);
            cubeNodeMiddle.Components.Add(cubeMesh);

            // CUBE CIRCLE
            var cubesAmountCircle = 6;

            SceneNode[] cubeNodesCircle = new SceneNode[cubesAmountCircle];
            _cubeTransformCircle = new Transform[cubesAmountCircle];
            _cubeEffectCircle = new SurfaceEffect[cubesAmountCircle];

            int degree = 0;
            int radius = 10;

            for (int i = 0; i < cubesAmountCircle; i++) {
                cubeNodesCircle[i] = new SceneNode();
                double startAngle = i * Math.PI * 2 / cubesAmountCircle;

                _cubeTransformCircle[i] = new Transform() 
                {
                    Rotation = new float3(0, 0, (float) startAngle),
                    Scale = new float3(0.3f, 0.3f, 0.3f),
                    Translation = new float3(radius*M.Cos(degree), radius*M.Sin(degree), 0),
                };
                degree += 90;

                _cubeEffectCircle[i] = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Blue);

                cubeNodesCircle[i].Components.Add(_cubeTransformCircle[i]);
                cubeNodesCircle[i].Components.Add(_cubeEffectCircle[i]);
                cubeNodesCircle[i].Components.Add(cubeMesh);
            }

            // BOTTOM CUBE
            _cubeTransformBottom = new Transform() 
            {
                Scale = new float3(2, 1, 1),
                Translation = new float3(0, -10, -40),
            };
            _cubeEffectBottom = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Black);

            var cubeNodeBottom = new SceneNode();
            cubeNodeBottom.Components.Add(_cubeTransformBottom);
            cubeNodeBottom.Components.Add(_cubeEffectBottom);
            cubeNodeBottom.Components.Add(cubeMesh);

            // THE SCENE
            // Create the scene containing the cube as the only object
            _scene = new SceneContainer();
            _scene.Children.Add(cameraNode);

            _scene.Children.Add(cubeNodeMiddle);
            for (int i = 0; i < cubesAmountCircle; i++) {
                _scene.Children.Add(cubeNodesCircle[i]);
            }
            _scene.Children.Add(cubeNodeBottom);

            // THE RENDERER
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {   
            _cubeAngle = _cubeAngle + 90.0f * M.Pi/180.0f * DeltaTime;
            _colorSwitch = new float4(
                0.5f + 0.5f * M.Sin(4 * TimeSinceStart),
                0.5f + 0.5f * M.Sin(6 * TimeSinceStart),
                0.5f + 0.5f * M.Sin(7 * TimeSinceStart),
                1);

            // MIDDLE CUBE
            _cubeTransformMiddle.Rotation = new float3(_cubeAngle, 0, 0);
            _cubeTransformMiddle.Scale = new float3(0.25f + 0.25f * M.Sin(TimeSinceStart) + 0.3f, 0.3f, 0.3f);
            _cubeTransformMiddle.Translation = new float3(0, 2 * M.Sin(3 * TimeSinceStart), 0);

            _cubeEffectMiddle.SurfaceInput.Albedo = _colorSwitch;

            // CUBE CIRCLE
            int difference = 0;
            int radius = 10;
            for(int i = 0; i < _cubeTransformCircle.Length; i++) {
                _cubeTransformCircle[i].Rotation = new float3(0, 0, _cubeAngle);
                _cubeTransformCircle[i].Translation = new float3(radius*M.Cos(TimeSinceStart + difference), radius*M.Sin(TimeSinceStart + difference), 0);
    	        difference += 200;
                _cubeEffectCircle[i].SurfaceInput.Albedo = _colorSwitch;
            }

            // BOTTOM CUBE
            _cubeTransformBottom.Translation =  new float3(2 * M.Sin(3 * TimeSinceStart), _cubeTransformBottom.Translation.y, _cubeTransformBottom.Translation.z);
            
            _cubeEffectBottom.SurfaceInput.Albedo = _colorSwitch;

            //Diagnostics.Debug(TimeSinceStart);

            // Render the scene tree
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

    }
}