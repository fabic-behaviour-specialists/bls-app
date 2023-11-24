using CoreAnimation;
using CoreGraphics;
using Foundation;
using ImageIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;

namespace Fabic.iOS.UIControls
{
    public class AnimatedImageView
    {
        private static int ButterflyCount = 0;
        private static NSData ButterFlyData = null;
        private static Dictionary<string, List<NSObject>> FrameCountDict = new Dictionary<string, List<NSObject>>();
        private static Dictionary<string, List<CGImage>> FrameCGImages = new Dictionary<string, List<CGImage>>();
        private static Dictionary<string, List<double>> FrameDurationsDict = new Dictionary<string, List<double>>();
        private static Dictionary<string, List<NSNumber>> FramePercentageDurationsDict = new Dictionary<string, List<NSNumber>>();
        private static Dictionary<string, List<double>> FramePercentageDurationsDoubleDict = new Dictionary<string, List<double>>();
        private static Dictionary<string, double> FrameDurationDict = new Dictionary<string, double>();

        //public static UIImageView GetAnimatedImageView(string url, string imageName, UIImageView imageView = null)
        //{
        //    var sourceRef = CGImageSource.FromUrl(NSUrl.FromString(url));
        //    return CreateAnimatedImageView(sourceRef, imageName, imageView);
        //}

        public static UIImageView GetAnimatedImageView(NSData nsData, string imageName, UIImageView imageView = null)
        {
            if (ButterflyCount < 3)
            {
                if (ButterFlyData == null)
                {
                    NSUrl path = NSBundle.MainBundle.GetUrlForResource("butterfly", "gif");
                    ButterFlyData = NSData.FromUrl(path);
                }
                var sourceRef = CGImageSource.FromData(ButterFlyData);
                ButterflyCount++;
                return CreateAnimatedImageView(sourceRef, imageName, imageView);
            }
            else
            {
                return new UIImageView();
            }
        }

        private static UIImageView CreateAnimatedImageView(CGImageSource imageSource, string imageName, UIImageView imageView = null)
        {
            var frameCount = Convert.ToInt32(imageSource.ImageCount);

            bool useExisting = true;
            if (!FrameCountDict.ContainsKey(imageName))
                FrameCountDict.Add(imageName, new List<NSObject>());
            if (!FrameCGImages.ContainsKey(imageName))
                FrameCGImages.Add(imageName, new List<CGImage>());
            if (!FrameDurationsDict.ContainsKey(imageName))
                FrameDurationsDict.Add(imageName, new List<double>());
            if (!FramePercentageDurationsDict.ContainsKey(imageName))
                FramePercentageDurationsDict.Add(imageName, new List<NSNumber>());
            if (!FramePercentageDurationsDoubleDict.ContainsKey(imageName))
                FramePercentageDurationsDoubleDict.Add(imageName, new List<double>());
            if (!FrameDurationDict.ContainsKey(imageName))
                FrameDurationDict.Add(imageName, 0);

            if (FrameCountDict[imageName].Count <= 0)
                useExisting = false;
            if (!useExisting)
            {
                FrameCountDict[imageName].Clear();
                FrameCGImages[imageName].Clear();
                FrameDurationsDict[imageName].Clear();
                FramePercentageDurationsDict[imageName].Clear();
                FramePercentageDurationsDoubleDict[imageName].Clear();

                //for (int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < frameCount; i++)
                    {
                        var frameImage = imageSource.CreateImage(i, null);

                        FrameCGImages[imageName].Add(frameImage);
                        FrameCountDict[imageName].Add(NSObject.FromObject(frameImage));

                        var properties = imageSource.GetProperties(i, null);
                        var duration = properties.Dictionary["{GIF}"];
                        var delayTime = duration.ValueForKey(new NSString("DelayTime"));
                        duration.Dispose();
                        var realDuration = double.Parse(delayTime.ToString());
                        FrameDurationsDict[imageName].Add(realDuration);
                        FrameDurationDict[imageName] += realDuration;
                        frameImage.Dispose();
                    }
                }
                NSNumber currentDurationPercentage = 0.0f;
                double currentDurationDouble = 0.0f;
                //for (int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < frameCount; i++)
                    {
                        if (i != 0)
                        {
                            var previousDuration = FrameDurationsDict[imageName][i - 1];
                            var previousDurationPercentage = FramePercentageDurationsDoubleDict[imageName][i - 1];

                            var number = previousDurationPercentage + (previousDuration / FrameDurationDict[imageName]);
                            currentDurationDouble = number;
                            currentDurationPercentage = new NSNumber(number);
                        }
                        FramePercentageDurationsDoubleDict[imageName].Add(currentDurationDouble);
                        FramePercentageDurationsDict[imageName].Add(currentDurationPercentage);
                    }
                }
            }


            var imageSourceProperties = imageSource.GetProperties(null);
            var imageSourceGIFProperties = imageSourceProperties.Dictionary["{GIF}"];
            var loopCount = imageSourceGIFProperties.ValueForKey(new NSString("LoopCount"));
            var imageSourceLoopCount = float.Parse(loopCount.ToString());
            var frameAnimation = new CAKeyFrameAnimation();
            frameAnimation.KeyPath = "contents";
            //if (imageSourceLoopCount <= 0.0f)
            //{
            frameAnimation.RepeatCount = float.MaxValue;
            //}
            //else
            //{
            //    frameAnimation.RepeatCount = imageSourceLoopCount;
            //}

            imageSourceGIFProperties.Dispose();

            FramePercentageDurationsDict[imageName].AddRange(FramePercentageDurationsDict[imageName]);
            FrameCountDict[imageName].AddRange(FrameCountDict[imageName]);

            frameAnimation.CalculationMode = CAAnimation.AnimationDescrete;
            frameAnimation.Values = FrameCountDict[imageName].ToArray();
            frameAnimation.Duration = 26f;//FrameDurationDict[imageName];
            frameAnimation.KeyTimes = FramePercentageDurationsDict[imageName].ToArray();
            frameAnimation.RemovedOnCompletion = false;
            frameAnimation.RepeatDuration = 99000;
            // frameAnimation.FillMode = CAFillMode.Both;
            frameAnimation.AnimationStopped += FrameAnimation_AnimationStopped;
            var firstFrame = FrameCGImages[imageName][0];
            if (imageView == null)
                imageView = new UIImageView(new RectangleF(0.0f, 0.0f, firstFrame.Width, firstFrame.Height));
            else
                imageView.Layer.RemoveAllAnimations();

            imageView.Layer.AddAnimation(frameAnimation, "contents");

            frameAnimation.Dispose();
            return imageView;
        }

        private static void FrameAnimation_AnimationStopped(object sender, CAAnimationStateEventArgs e)
        {
            e.Finished = false;
        }
    }
}
