using System;
using UIKit;

namespace Fabic.Data.Extensions
{
    public static class UIExtensions
    {
        public static UIColor FabicColour(this UIColor colour, Fabic.Data.Enums.FabicColour fabicColour = Enums.FabicColour.Purple, bool highlighted = false)
        {
            switch (fabicColour)
            {
                case Enums.FabicColour.Purple:
                    if (highlighted)
                        return new UIColor((nfloat)(0.2431372549019608), (nfloat)(0.0941176470588235), (nfloat)(0.2431372549019608), 1); //62, 24, 62, #3e183e
                    else
                        return new UIColor((nfloat)(0.4352941176470588), (nfloat)(0.1803921568627451), (nfloat)(0.4431372549019608), 1); //111, 46, 113, #6f2e71
                case Enums.FabicColour.LightBlue:
                    if (highlighted)
                        return new UIColor((nfloat)0.5, (nfloat)0.7, 1, 1);
                    else
                        return new UIColor(0, (nfloat)0.39215, (nfloat)0.7, 1);
                case Enums.FabicColour.Blue:
                    if (highlighted)
                        return new UIColor((nfloat)0.0196078431372549, (nfloat)0.7647058823529412, (nfloat)0.9529411764705882, 1); //5, 195, 243, #05c3f3
                    else
                        return new UIColor((nfloat)0.1647058823529412, (nfloat)0.603921568627451, (nfloat)0.8392156862745098, 1); //42, 154, 214, #2a9ad6
                case Enums.FabicColour.Gray:
                    if (highlighted)
                        return new UIColor((nfloat)0.43922, (nfloat)0.45882, (nfloat)0.48235, 1); //112, 117, 123 #70757b
                    else
                        return new UIColor((nfloat)0.5096, (nfloat)0.52549, (nfloat)0.5294, 1); //130, 134, 135, #828687
                case Enums.FabicColour.Red:
                    if (highlighted)
                        return new UIColor((nfloat)0.9529, (nfloat)0.58824, (nfloat)0.64314, 1);
                    else
                        return new UIColor((nfloat)0.8039, (nfloat)0.12941, (nfloat)0.16863, 1); //205, 33, 43 #cd212b
                case Enums.FabicColour.Orange:
                    if (highlighted)
                        return new UIColor((nfloat)0.964705, (nfloat)0.75696, (nfloat)0.61568, 1);
                    else
                        return new UIColor((nfloat)0.95294, (nfloat)0.41569, (nfloat)0.18836, 1); //243, 106, 48 #f36a30
                case Enums.FabicColour.Yellow:
                    if (highlighted)
                        return new UIColor((nfloat)0.96862, (nfloat)0.90980, (nfloat)0.63137, 1);
                    else
                        return new UIColor((nfloat)0.94509, (nfloat)0.78824, (nfloat)0.17647, 1); //241, 201, 45 #f1c92d
                case Enums.FabicColour.Green:
                    if (highlighted)
                        return new UIColor((nfloat)0.58823, (nfloat)0.81569, (nfloat)0.67843, 1);
                    else
                        return new UIColor(0, (nfloat)0.64314, (nfloat)0.32549, 1); //0, 164, 83 #00a453
                case Enums.FabicColour.DarkBlue:
                    if (highlighted)
                        return new UIColor((nfloat)0.54902, (nfloat)0.67843, (nfloat)0.84706, 1);
                    else
                        return new UIColor((nfloat)0.16863, (nfloat)0.30196, (nfloat)0.61569, 1); //43, 77, 157 #2B4D9D
            }
            return new UIColor(0, (nfloat)0.4588, (nfloat)0.6784, 1);
        }
    }
}
