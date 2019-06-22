using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VSNewProjectDialogExample.Implementation.Templates;

namespace VSNewProjectDialogExample.Implementation
{
    public class DynamicIconSourceGenerator
    {


        private static ResourceDictionary _resourceDictionary
            = new ResourceDictionary() { Source = new Uri("/Resources/Styles.xaml", UriKind.Relative) };

        public static object GetResource(string key)
        {
                return _resourceDictionary[key];
        }

        private DynamicIconSourceGenerator()
        {
            
        }

        private static DynamicIconSourceGenerator _generator = new DynamicIconSourceGenerator();
        public static DynamicIconSourceGenerator Instance
        {
            get
            {
                    return _generator;
            }
        }
        

        
        public DrawingImage CreateDrawingImageFromProjectTemplate(ProjectTemplate template)
        {
            var drawing = new DrawingImage();
            var geometryDrawing  = new GeometryDrawing();
            var geometryGroup = new GeometryGroup() { };
            geometryGroup.Children.Add(new RectangleGeometry() {Rect = new Rect(0,0,128,128)});
            geometryDrawing.Geometry = geometryGroup;
            var visualBrush = new VisualBrush();

            var brushTemplate = (Border)GetResource("DynamicIconVisualBrush");
            brushTemplate.DataContext = template;
            brushTemplate.ApplyTemplate();

            visualBrush.Visual = brushTemplate;
            geometryDrawing.Brush = visualBrush;
            drawing.Freeze();
            
            
            return drawing;
        }
    }
}
