using System.Collections;
using UnityEngine;

namespace RunUp.NObstacle {
    public class ObstacleColor {
        public enum ColorCategory {
            Pink,
            Blue
        };
        
        private static ObstacleColor instance;
        private Hashtable _colors;

        public static ObstacleColor Instance {
            get { return instance ??= new ObstacleColor(); }
        }

        private ObstacleColor() {
            _colors = new Hashtable();

            Color[] colorsPink = {
                new Color(32 / 255f, 177 / 255f, 138 / 255f),
                new Color(27 / 255f, 145 / 255f, 114 / 255f),
                new Color(44 / 255f, 198 / 255f, 94 / 255f),
                new Color(37 / 255f, 161 / 255f, 77 / 255f),
            };
            _colors[ColorCategory.Pink] = colorsPink;
            
            Color[] colorsBlue = {
                new Color(254 / 255f, 196 / 255f, 11 / 255f),
                new Color(253 / 255f, 152 / 255f, 8 / 255f),
                new Color(221 / 255f, 105 / 255f, 27 / 255f),
                new Color(198 / 255f, 63 / 255f, 5 / 255f),
            };
            _colors[ColorCategory.Blue] = colorsBlue;
        }
        
        public Color GetColor(ColorCategory colorCategory) {
            var colors = (Color[]) _colors[colorCategory];
            var index = Random.Range(0, colors.Length);
            
            return colors[index];
        }
    }
}