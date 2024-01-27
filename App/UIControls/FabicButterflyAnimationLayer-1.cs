using CoreGraphics;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.iOS.UIControls
{
    enum AnimationLocation
    {
        TopRight = 0,
        TopLeft = 1,
        BottomRight = 2,
        BottomLeft = 3
    }

    enum AnimationSize
    {
        Small = 0,
        Medium = 1,
        Big = 2
    }

    public class FabicButterflyAnimationLayer : UIView
    {
        NSData ButterFlyData;
        public FabicButterflyAnimationLayer()
        {
            //AnimatedImageView.GetAnimatedImageView(null, "fabic");
            this.UserInteractionEnabled = false;
        }

        public async Task<bool> Animate(int numButterflys, bool repeat = false)
        {
            try
            {
                AnimationLocation location;

                if (numButterflys > 2)
                    numButterflys = 2;

                for (int i = 0; i < numButterflys; i++)
                {
                    UIImageView imageButterFly;
                    CGPoint startLocation = new CGPoint();
                    CGPoint endLocation = new CGPoint();
                    CGPoint middleLocation;
                    CGSize startSize = new CGSize();
                    CGSize endSize = new CGSize();
                    int speed = 5;
                    int size = 0;
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    int loc = random.Next(0, 3);

                    // start location
                    //AnimationLocation startLoc = (AnimationLocation)loc;
                    //switch (startLoc)
                    //{
                    //    case AnimationLocation.BottomLeft:
                    startLocation = new CGPoint(random.Next(0, Convert.ToInt32(UIScreen.MainScreen.Bounds.Width)), UIScreen.MainScreen.Bounds.Height + 50);
                    //        loc = random.Next(0, 1);
                    //        break;
                    //    case AnimationLocation.BottomRight:
                    //        startLocation = new CGPoint(random.Next(Convert.ToInt32(UIScreen.MainScreen.Bounds.Width / 2), Convert.ToInt32(UIScreen.MainScreen.Bounds.Width)), UIScreen.MainScreen.Bounds.Height + 50);
                    //        loc = random.Next(0, 1);
                    //        break;
                    //    case AnimationLocation.TopLeft:
                    //        startLocation = new CGPoint(random.Next(0, Convert.ToInt32(UIScreen.MainScreen.Bounds.Width / 2)), -100);
                    //        loc = random.Next(2, 3);
                    //        down = true;
                    //        break;
                    //    case AnimationLocation.TopRight:
                    //        startLocation = new CGPoint(random.Next(Convert.ToInt32(UIScreen.MainScreen.Bounds.Width / 2), Convert.ToInt32(UIScreen.MainScreen.Bounds.Width)), -100);
                    //        loc = random.Next(2, 3);
                    //        down = true;
                    //        break;
                    //}

                    // end location
                    //AnimationLocation endLoc = (AnimationLocation)loc;
                    //switch (endLoc)
                    //{
                    //    case AnimationLocation.BottomLeft:
                    //        endLocation = new CGPoint(random.Next(0, Convert.ToInt32(UIScreen.MainScreen.Bounds.Width / 2)), UIScreen.MainScreen.Bounds.Height + 50);
                    //        break;
                    //    case AnimationLocation.BottomRight:
                    //        endLocation = new CGPoint(random.Next(Convert.ToInt32(UIScreen.MainScreen.Bounds.Width / 2), Convert.ToInt32(UIScreen.MainScreen.Bounds.Width)), UIScreen.MainScreen.Bounds.Height + 50);
                    //        break;
                    //    case AnimationLocation.TopLeft:
                    endLocation = new CGPoint(random.Next(0, Convert.ToInt32(UIScreen.MainScreen.Bounds.Width)), -100);
                    //        break;
                    //    case AnimationLocation.TopRight:
                    //        endLocation = new CGPoint(random.Next(Convert.ToInt32(UIScreen.MainScreen.Bounds.Width / 2), Convert.ToInt32(UIScreen.MainScreen.Bounds.Width)), -100);
                    //        break;
                    //}

                    // speed
                    speed = random.Next(5, 9);

                    // start size
                    size = random.Next(0, 2);
                    AnimationSize sSize = (AnimationSize)size;
                    switch (sSize)
                    {
                        case AnimationSize.Small:
                            startSize = new CGSize(80, 50);
                            break;
                        case AnimationSize.Medium:
                            startSize = new CGSize(130, 95);
                            break;
                        case AnimationSize.Big:
                            startSize = new CGSize(160, 105);
                            break;
                    }

                    // end size
                    size = random.Next(0, 2);
                    AnimationSize eSize = (AnimationSize)size;
                    switch (eSize)
                    {
                        case AnimationSize.Small:
                            endSize = new CGSize(80, 50);
                            break;
                        case AnimationSize.Medium:
                            endSize = new CGSize(130, 95);
                            break;
                        case AnimationSize.Big:
                            endSize = new CGSize(160, 105);
                            break;
                    }

                    imageButterFly = new UIImageView();
                    imageButterFly.ContentMode = UIViewContentMode.ScaleAspectFit;
                    //imageButterFly = AnimatedImageView.GetAnimatedImageView(ButterFlyData, "fabic", imageButterFly);
                    imageButterFly.Frame = new CGRect(startLocation, startSize);
                    imageButterFly.ClipsToBounds = true;
                    imageButterFly.Layer.ShadowColor = UIColor.Black.CGColor;
                    imageButterFly.Layer.ShadowOpacity = 0.4f;
                    imageButterFly.Layer.ShadowOffset = new CGSize(2, 4);
                    imageButterFly.Layer.ShadowRadius = 3;

                    //float xDiff = Convert.ToSingle(startLocation.X - endLocation.X);
                    //float yDiff = Convert.ToSingle(startLocation.Y - endLocation.Y);
                    //float angle = Convert.ToSingle(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
                    //imageButterFly.Transform = CGAffineTransform.MakeRotation(angle);

                    this.Add(imageButterFly);

                    UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(speed, UIViewAnimationCurve.EaseOut, () => { });
                    propertyAnimator.AddAnimations(() =>
                    {
                        imageButterFly.Frame = new CGRect(endLocation, endSize);
                    }, 1);
                    propertyAnimator.AddCompletion((UIViewAnimatingPosition po) =>
                    {
                        imageButterFly.RemoveFromSuperview();
                        imageButterFly.Layer.RemoveAllAnimations();
                        imageButterFly.Dispose();
                        imageButterFly = null;
                    });
                    propertyAnimator.StartAnimation();

                    await Task.Delay(250);
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}
