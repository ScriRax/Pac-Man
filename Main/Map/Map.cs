using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace monJeu
{
    public class Map 
    {
        public List<Walls> LoadWalls(ContentManager content)
        {
            var wallTexture = content.Load<Texture2D>("WallPac");

            #region Walls
            return new List<Walls>() {
                new Walls(wallTexture)
                {
                    Position = new Vector2(0,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(0,400),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,100),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,515),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,650),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(35,665),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(45,515),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(45,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(70,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(70,400),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(100,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(100,100),
                },               
                new Walls(wallTexture)
                {
                    Position = new Vector2(100,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(115,515),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(115,580),
                },                   
                new Walls(wallTexture)
                {
                    Position = new Vector2(140,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(140,400),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(185,515),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(185,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(150,690),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(150,700),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(215,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(215,100),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(215,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(255,280),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(255,350),
                },            
                new Walls(wallTexture)
                {
                    Position = new Vector2(255,460),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(255,530),
                },              
                new Walls(wallTexture)
                {
                    Position = new Vector2(255,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(265,650),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(265,665),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(280,650),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(280,665),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(285,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(325,280),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(325,350),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(325,460),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(325,530),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(325,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(330,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(330,100),
                }, 
                new Walls(wallTexture)
                {
                    Position = new Vector2(355,35),
                },            
                new Walls(wallTexture)
                {
                    Position = new Vector2(355,100),
                },               
                new Walls(wallTexture)
                {
                    Position = new Vector2(355,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(395,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(395,690),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(395,700),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(420,35),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(420,100),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(420,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(435,330),
                }, 
                new Walls(wallTexture)
                {
                    Position = new Vector2(435,400),
                },                          
                new Walls(wallTexture)
                {
                    Position = new Vector2(435,470),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(505,330),
                },  
                new Walls(wallTexture)
                {
                    Position = new Vector2(505,400),
                },                            
                new Walls(wallTexture)
                {
                    Position = new Vector2(505,470),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(515,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(515,400),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(515,470),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(515,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(515,650),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(515,665),
                },            
                new Walls(wallTexture)
                {
                    Position = new Vector2(535,0),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(535,70),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(535,140),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(535,210),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(535,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(585,665),
                },           
                new Walls(wallTexture)
                {
                    Position = new Vector2(600,0),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(630,330),
                },  
                new Walls(wallTexture)
                {
                    Position = new Vector2(630,400),
                },                            
                new Walls(wallTexture)
                {
                    Position = new Vector2(630,470),
                },
                 new Walls(wallTexture)
                {
                    Position = new Vector2(655,115),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,145),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,370),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,440),
                },               
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,510),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,650),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(655,665),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(670,0),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(725,115),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(725,145),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(725,215),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(740,0),
                },  
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,580),
                },                
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,690),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,700),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(810,0),
                },  
                new Walls(wallTexture)
                {
                    Position = new Vector2(835,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(835,690),
                },               
                new Walls(wallTexture)
                {
                    Position = new Vector2(835,700),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(880,0),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(840,115),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(840,140),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(840,210),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(840,215),
                }, 
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,370),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,440),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(765,470),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(810,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(810,370),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(810,440),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(810,470),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(900,580),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(900,690),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(900,700),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(920,330),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(920,370),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(920,440),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(920,510),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(920,580),
                },               
                new Walls(wallTexture)
                {
                    Position = new Vector2(950,0),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(950,70),
                },             
                new Walls(wallTexture)
                {
                    Position = new Vector2(950,140),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(950,210),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(950,215),
                }, 
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,0),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,70),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,140),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,210),
                },
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,215),
                }, 
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,690),
                },             
                new Walls(wallTexture)
                {
                    Position = new Vector2(970,700),
                },
                };
            #endregion
        }
    }
}