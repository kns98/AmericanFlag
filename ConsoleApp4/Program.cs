using System;
using System.Drawing;

class FlagDrawer
{
    private void DrawStar(Graphics graphics, float xCenter, float yCenter, int nPoints, float outerRadius, float innerRadius)
    {
        PointF[] points = new PointF[2 * nPoints + 1];
        for (int ix = 0; ix < points.Length; ix++)
        {
            double angle = Math.PI * ix / nPoints - Math.PI / 2.0;
            double radius = ix % 2 == 0 ? outerRadius : innerRadius;
            points[ix] = new PointF(
                (float)(xCenter + radius * Math.Cos(angle)),
                (float)(yCenter + radius * Math.Sin(angle))
            );
        }
        graphics.FillPolygon(Brushes.White, points);
    }

    public void DrawAmericanFlag(float x, float y, float height)
    {
        float width = height * 1.9f;
        float xStarSeparation = height * 0.063f;
        float yStarSeparation = height * 0.054f;

        using Bitmap bitmap = new Bitmap((int)width, (int)height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // White background
        graphics.FillRectangle(Brushes.White, 0, 0, width, height);

        // Red stripes
        for (int i = 0; i <= 12; i++)
        {
            if (i % 2 == 1)
            {
                graphics.FillRectangle(Brushes.Red, 0, (float)(i * height / 13.0), width, (float)(height / 13.0));
            }
        }

        // Blue box
        graphics.FillRectangle(Brushes.Blue, 0, 0, (float)(0.76 * height), (float)((7.0 / 13.0) * height));

        // Stars
        float outerRadius = 0.0616f * height / 2.0f;
        float innerRadius = outerRadius * (float)(Math.Sin(Math.PI / 10.0) / Math.Sin(7.0 * Math.PI / 10.0));
        for (int row = 1; row <= 9; row++)
        {
            for (int col = 1; col <= 11; col++)
            {
                if ((row + col) % 2 == 0)
                {
                    DrawStar(graphics, xStarSeparation * col, yStarSeparation * row, 5, outerRadius, innerRadius);
                }
            }
        }

        bitmap.Save("AmericanFlag.png");
    }
}

class Program
{
    static void Main(string[] args)
    {
        FlagDrawer drawer = new FlagDrawer();
        drawer.DrawAmericanFlag(0.0f, 0.0f, 300.0f);
    }
}
