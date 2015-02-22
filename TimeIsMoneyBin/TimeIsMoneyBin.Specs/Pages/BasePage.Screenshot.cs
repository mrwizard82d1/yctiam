using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Interactions;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace TimeIsMoneyBin.Specs.Pages
{
    public partial class BasePage
    {
        internal void TakeScreenshot(string saveLocation)
        {
            ITakesScreenshot ssdriver = WebDriver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(saveLocation, ImageFormat.Png);
        }

        internal void TakeFullScreenshot(string saveLocation)
        {// Get the Total Size of the Document
            int totalWidth = (int)EvalScript<long>("return document.width");
            int totalHeight = (int)EvalScript<long>("return document.height");

            // Get the Size of the Viewport
            int viewportWidth = (int)EvalScript<long>("return document.body.clientWidth");
            int viewportHeight = (int)EvalScript<long>("return document.body.clientHeight");

            // Split the Screen in multiple Rectangles
            List<Rectangle> rectangles = new List<Rectangle>();
            // Loop until the Total Height is reached
            for (int i = 0; i < totalHeight; i += viewportHeight)
            {
                int newHeight = viewportHeight;
                // Fix if the Height of the Element is too big
                if (i + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - i;
                }
                // Loop until the Total Width is reached
                for (int ii = 0; ii < totalWidth; ii += viewportWidth)
                {
                    int newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (ii + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - ii;
                    }

                    // Create and add the Rectangle
                    Rectangle currRect = new Rectangle(ii, i, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }

            // Build the Image
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // Get all Screenshots and stitch them together
            Rectangle previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // Calculate the Scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    int xDiff = rectangle.Right - previous.Right;
                    int yDiff = rectangle.Bottom - previous.Bottom;
                    // Scroll
                    RunScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                    System.Threading.Thread.Sleep(200);
                }

                // Take Screenshot
                var screenshot = ((ITakesScreenshot)WebDriver).GetScreenshot();

                // Build an Image out of the Screenshot
                Image screenshotImage;
                using (MemoryStream memStream = new MemoryStream(screenshot.AsByteArray))
                {
                    screenshotImage = Image.FromStream(memStream);
                }

                // Calculate the Source Rectangle
                Rectangle sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);

                // Copy the Image
                using (Graphics g = Graphics.FromImage(stitchedImage))
                {
                    g.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }

                // Set the Previous Rectangle
                previous = rectangle;
            }
            stitchedImage.Save(saveLocation, ImageFormat.Png);
        }

    }
}
