using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Engine.Core.Effects;
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
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "Yet another FUSEE App.")]
    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private Transform _camAngle;
        private Transform _baseTransform;
        private Transform _bodyTransform;
        private Transform _upperArmTransform;
        private Transform _foreArmTransform;
        private Transform _grabberBottomArmLeftTransform;
        private Transform _grabberBottomArmRightTransform;
        private Transform _grabberTopArmTransform;
        private Boolean _camMoved;
        private float _camVelocity;
        private Boolean _grabberOpen = false;
        private Boolean _grabberClose = false;


        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _camAngle = new Transform
            {
                Translation = new float3(0, 0, 0),
            };

            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            _bodyTransform = new Transform
            {
                Translation = new float3(0, 6, 0),
                Rotation = new float3(0, (float) Math.PI/2, 0)
            };

            _upperArmTransform = new Transform
            {
                Translation = new float3(2, 4, 0),
                Rotation = new float3((float) Math.PI/4, 0, 0)
            };

            _foreArmTransform = new Transform
            {
                Translation = new float3(-2, 4, 0),
                Rotation = new float3((float) Math.PI/3, 0, 0)
            };

            _grabberBottomArmLeftTransform = new Transform
            {
                Translation = new float3(0, 4, 0),
            };

            _grabberBottomArmRightTransform = new Transform
            {
                Translation = new float3(0, 4, 0),
            };

            _grabberTopArmTransform = new Transform
            {
                Translation = new float3(0, 4, 0),
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
                            _camAngle
                        },
                        Children =
                        {
                            new SceneNode
                            {
                                Components =
                                {
                                    new Transform
                                    {
                                        Translation = new float3(0, 10, -50),
                                    },
                                    new Camera(ProjectionMethod.Perspective, 5, 100, M.PiOver4)
                                    {
                                        BackgroundColor =  (float4) ColorUint.Greenery
                                    }
                                }
                            }
                        }
                    },

                    new SceneNode
                    {
                        Name = "Base",
                        Components =
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),

                            // MESH COMPONENT
                            new CuboidMesh(new float3(10, 2, 10))
                        },
                        Children =
                        {
                            new SceneNode
                            {
                                Name = "Body",
                                Components = 
                                {
                                    _bodyTransform,
                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.IndianRed),
                                    new CuboidMesh(new float3(2, 10, 2))
                                },
                                Children =
                                {
                                    new SceneNode
                                    {
                                        Name = "UpperArm",
                                        Components = 
                                        {
                                            _upperArmTransform,
                                        },
                                        Children = 
                                        {
                                            new SceneNode
                                            {
                                                Components =
                                                {
                                                    new Transform { Translation = new float3(0, 4, 0)},
                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.ForestGreen),
                                                    new CuboidMesh(new float3(2, 10, 2))
                                                },
                                                Children =
                                                {
                                                    new SceneNode
                                                    {
                                                        Name = "ForeArm",
                                                        Components = 
                                                        {
                                                            _foreArmTransform,
                                                        },
                                                        Children = 
                                                        {
                                                            new SceneNode
                                                            {
                                                                Components =
                                                                {
                                                                    new Transform { Translation = new float3(0, 4, 0)},
                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.SkyBlue),
                                                                    new CuboidMesh(new float3(2, 10, 2))
                                                                },
                                                                Children =
                                                                {
                                                                    new SceneNode
                                                                    {
                                                                        Name = "GrabberBottomArmLeft",
                                                                        Components = 
                                                                        {
                                                                            _grabberBottomArmLeftTransform,
                                                                        },
                                                                        Children = 
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform 
                                                                                    {
                                                                                        Translation = new float3(1, 2, 1),
                                                                                        Rotation = new float3(0, (float) Math.PI/4, 0)
                                                                                    },
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),
                                                                                    new CuboidMesh(new float3(1, 3, 1))
                                                                                }
                                                                                
                                                                            },
                                                                        }
                                                                    },
                                                                    new SceneNode
                                                                    {
                                                                        Name = "GrabberBottomArmRight",
                                                                        Components = 
                                                                        {
                                                                            _grabberBottomArmRightTransform,
                                                                        },
                                                                        Children =
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform 
                                                                                    {
                                                                                        Translation = new float3(-1, 2, 1),
                                                                                        Rotation = new float3(0, (float) Math.PI/4, 0)
                                                                                    },
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),
                                                                                    new CuboidMesh(new float3(1, 3, 1))
                                                                                }
                                                                                
                                                                            }
                                                                        }
                                                                    },
                                                                    new SceneNode
                                                                    {
                                                                        Name = "GrabberTopArm",
                                                                        Components = 
                                                                        {
                                                                            _grabberTopArmTransform,
                                                                        },
                                                                        Children =
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform { Translation = new float3(0, 2, -1)},
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),
                                                                                    new CuboidMesh(new float3(1, 3, 1))
                                                                                }
                                                                                
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // ROTATE CAMERA ON LEFT CLICK
            if(Mouse.LeftButton) {
                _camVelocity = Mouse.Velocity.x;
                _camAngle.Rotation = new float3(0, _camAngle.Rotation.y + 0.5f*((float)Math.PI/180) * DeltaTime * _camVelocity, 0);
                _camMoved = true;
            }

            // CAMERA SWIPE EFFECT
            if(_camMoved == true) {
                _camVelocity = _camVelocity/1.1f;
                _camAngle.Rotation = new float3(0, _camAngle.Rotation.y + 0.5f*((float)Math.PI/180) * DeltaTime * _camVelocity, 0);
                if(_camVelocity > -0.1 & _camVelocity < 0.1) {
                    _camMoved = false;
                }
            }

            // ROTATE ROBOT
            _bodyTransform.Rotation = new float3(0, _bodyTransform.Rotation.y + 90*((float)Math.PI/180) * DeltaTime * Keyboard.LeftRightAxis, 0);
            _upperArmTransform.Rotation = new float3(_upperArmTransform.Rotation.x + 45*((float)Math.PI/180) * DeltaTime * Keyboard.UpDownAxis, 0, 0);
            _foreArmTransform.Rotation = new float3(_foreArmTransform.Rotation.x + 60*((float)Math.PI/180) * DeltaTime * Keyboard.UpDownAxis, 0, 0);

            // CLOSE GRABBER WITH W AND OPEN WITH S
            if(Keyboard.GetKey(KeyCodes.W)) {
                _grabberOpen = true;
                _grabberClose = false;
            }
            if(Keyboard.GetKey(KeyCodes.S)) {
                _grabberClose = true;
                _grabberOpen = false;
            }

            if(_grabberOpen == true) {
                float rotation = 10*((float)Math.PI/180);
                if(_grabberBottomArmLeftTransform.Rotation.x >= -rotation) {
                    _grabberBottomArmLeftTransform.Rotation = new float3(_grabberBottomArmLeftTransform.Rotation.x + (-rotation) * DeltaTime, 0, _grabberBottomArmLeftTransform.Rotation.z + rotation * DeltaTime);
                    _grabberBottomArmRightTransform.Rotation = new float3(_grabberBottomArmRightTransform.Rotation.x + (-rotation) * DeltaTime, 0, _grabberBottomArmRightTransform.Rotation.z + (-rotation) * DeltaTime);
                    _grabberTopArmTransform.Rotation = new float3(_grabberTopArmTransform.Rotation.x + rotation * DeltaTime, 0, 0);
                } else {
                    _grabberOpen = false;
                }
            }

            if(_grabberClose == true) {
                float rotation = 10*((float)Math.PI/180);
                if(_grabberBottomArmLeftTransform.Rotation.x <= rotation) {
                    _grabberBottomArmLeftTransform.Rotation = new float3(_grabberBottomArmLeftTransform.Rotation.x + rotation * DeltaTime, 0, _grabberBottomArmLeftTransform.Rotation.z + (-rotation) * DeltaTime);
                    _grabberBottomArmRightTransform.Rotation = new float3(_grabberBottomArmRightTransform.Rotation.x + rotation * DeltaTime, 0, _grabberBottomArmRightTransform.Rotation.z + rotation * DeltaTime);
                    _grabberTopArmTransform.Rotation = new float3(_grabberTopArmTransform.Rotation.x + (-rotation) * DeltaTime, 0, 0);
                } else {
                    _grabberClose = false;
                }
            }

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }
    }
}