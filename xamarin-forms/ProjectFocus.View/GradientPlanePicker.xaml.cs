using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFocus.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradientPlanePicker : ContentView
    {
        public static readonly BindableProperty XSelectionProperty =
            BindableProperty.CreateAttached("XSelection", typeof(float), typeof(GradientPlanePicker), 0f, propertyChanged: OnPointChanged);

        public float XSelection
        {
            get { return (float)GetValue(XSelectionProperty); }
            set { SetValue(XSelectionProperty, value); }
        }

        public static readonly BindableProperty YSelectionProperty =
            BindableProperty.CreateAttached("YSelection", typeof(float), typeof(GradientPlanePicker), 0f, propertyChanged: OnPointChanged);

        public float YSelection
        {
            get { return (float)GetValue(YSelectionProperty); }
            set { SetValue(YSelectionProperty, value); }
        }

        private static void OnPointChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (GradientPlanePicker)bindable;
            picker.Invalidate();
        }

        public GradientPlanePicker()
        {
            InitializeComponent();
        }

        public void Invalidate()
        {
            if (skipInvalidate)
                skipInvalidate = false;
            else
                skiaCanvas.InvalidateSurface();
        }

        private bool skipInvalidate;
        private int lastCalculatedViewHeight;
        private int lastCalculatedViewWidth;
        private float lastXSelection;
        private float lastYSelection;

        private SKShader backgroundShader;

        private void SkiaCanvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var xSelection = XSelection;
            var ySelection = YSelection;
            var info = e.Info;

            if (lastCalculatedViewHeight == info.Height 
                && lastCalculatedViewWidth == info.Width
                && Math.Abs(lastXSelection - xSelection) < 0.1
                && Math.Abs(lastYSelection - ySelection) < 0.1)
            {
                return;
            }

            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            using (var gradientPaint = new SKPaint())
            {
                var squareSide = Math.Min(info.Width, info.Height);
                var squareLocationX = (info.Width - squareSide) / 2;
                var squareLocationY = (info.Height - squareSide) / 2;

                SKRect rect = new SKRect(
                    squareLocationX,
                    squareLocationY,
                    squareLocationX + squareSide,
                    squareLocationY + squareSide);

                var pointX = rect.MidX + (squareSide * xSelection) / 2;
                var pointY = rect.MidY - (squareSide * ySelection) / 2;

                if (lastCalculatedViewHeight != info.Height || lastCalculatedViewWidth != info.Width)
                {
                    backgroundShader = SKShader.CreateCompose(
                        SKShader.CreateLinearGradient(
                            new SKPoint(rect.Left, rect.Top),
                            new SKPoint(rect.Right, rect.Top),
                            new SKColor[] { SKColors.DarkCyan, SKColors.Red },
                            new float[] { 0, 1 },
                            SKShaderTileMode.Repeat),
                        SKShader.CreateLinearGradient(
                            new SKPoint(rect.Left, rect.Top),
                            new SKPoint(rect.Left, rect.Bottom),
                            new SKColor[] { SKColors.Transparent, SKColors.WhiteSmoke },
                            new float[] { 0, 1 },
                            SKShaderTileMode.Repeat));

                    lastCalculatedViewWidth = info.Width;
                    lastCalculatedViewHeight = info.Height;
                }

                var pointShader = SKShader.CreateTwoPointConicalGradient(
                    new SKPoint(rect.MidX, rect.MidY),
                    squareSide / 15,
                    new SKPoint(pointX, pointY),
                    3,
                    new SKColor[] { SKColors.Transparent, SKColors.Black },
                    null,
                    SKShaderTileMode.Clamp);

                gradientPaint.Style = SKPaintStyle.Fill;
                gradientPaint.Shader = SKShader.CreateCompose(backgroundShader, pointShader);

                lastXSelection = xSelection;
                lastYSelection = ySelection;
                canvas.DrawRect(rect, gradientPaint);
            }
        }

        private void TouchEffect_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            if(args.IsInContact)
            {
                var squareSide = Math.Min(skiaCanvas.Width, skiaCanvas.Height);
                skipInvalidate = true;
                var xSelection = (float)((2 * args.Location.X - skiaCanvas.Width) / squareSide);
                XSelection = xSelection<=1 ? xSelection >= -1 ? xSelection : -1 : 1;
                var ySelection = (float)((skiaCanvas.Height - 2 * args.Location.Y) / squareSide);
                YSelection = ySelection <= 1 ? ySelection >= -1 ? ySelection : -1 : 1;
            }
        }
    }
}