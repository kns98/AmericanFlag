using System;
using System.Drawing;

class FlagDrawer
{
    // Method to draw a star at a given center position with specified radii and number of points
    private void DrawStar(Graphics graphics, float xCenter, float yCenter, int nPoints, float outerRadius, float innerRadius)
    {
        // Create an array of points to form a star
        PointF[] points = new PointF[2 * nPoints + 1];
        for (int ix = 0; ix < points.Length; ix++)
        {
            // Calculate the angle for each point
            double angle = Math.PI * ix / nPoints - Math.PI / 2.0;
            // Alternate between outer and inner radius to get star shape
            double radius = ix % 2 == 0 ? outerRadius : innerRadius;
            // Determine the position of each point
            points[ix] = new PointF(
                (float)(xCenter + radius * Math.Cos(angle)),
                (float)(yCenter + radius * Math.Sin(angle))
            );
        }
        // Draw the star by filling the polygon
        graphics.FillPolygon(Brushes.White, points);
    }

    // Method to draw the American flag
    public void DrawAmericanFlag(float x, float y, float height)
    {
        // Calculate the width of the flag based on its height
        float width = height * 1.9f;
        // Define the separation between stars
        float xStarSeparation = height * 0.063f;
        float yStarSeparation = height * 0.054f;

        // Create a bitmap and a graphics object to draw the flag
        using Bitmap bitmap = new Bitmap((int)width, (int)height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Fill the background with white color
        graphics.FillRectangle(Brushes.White, 0, 0, width, height);

        // Draw the red stripes
        for (int i = 0; i <= 12; i++)
        {
            if (i % 2 == 1)
            {
                graphics.FillRectangle(Brushes.Red, 0, (float)(i * height / 13.0), width, (float)(height / 13.0));
            }
        }

        // Draw the blue box in the top left corner
        graphics.FillRectangle(Brushes.Blue, 0, 0, (float)(0.76 * height), (float)((7.0 / 13.0) * height));

        // Define the radius for the outer and inner points of the stars
        float outerRadius = 0.0616f * height / 2.0f;
        float innerRadius = outerRadius * (float)(Math.Sin(Math.PI / 10.0) / Math.Sin(7.0 * Math.PI / 10.0));

        // Draw the stars in the blue box
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

        // Save the drawn flag as a PNG file
        bitmap.Save("AmericanFlag.png");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of FlagDrawer and draw the flag
        FlagDrawer drawer = new FlagDrawer();
        drawer.DrawAmericanFlag(0.0f, 0.0f, 300.0f);
    }
}
