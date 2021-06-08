using SpaceInvaders.ECSFramework.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>An util class for manage all the algorithms of  processing image</summary>
    public static class ImageProcessing
    {
        #region PublicStaticMethods

        /// <summary>Detect if 2 image are in collision</summary>
        /// <param name="rEntity">The rectangle source.</param>
        /// <param name="rMissile">The r dest.</param>
        /// <param name="entityBitmap">The dest.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static bool OnCollision(Rectangle rEntity, Rectangle rMissile, Bitmap entityBitmap)
        {
            Point v = new Point(rEntity.X, rEntity.Y);
            rEntity.Intersect(rMissile);//Take the intersection for keep the focus on just collision part
            Rectangle coerce = rEntity;
            if (!(coerce.Height == 0 || coerce.Width == 0))
            {
                for (int y = coerce.Y - (int)v.Y; y < coerce.Y - (int)v.Y + coerce.Height; y++)
                {
                    for (int x = coerce.X - (int)v.X; x < coerce.X - (int)v.X + coerce.Width; x++)
                    {
                        if (entityBitmap.GetPixel(x, y).A != 0)//If a pixel have an opacity more than 0 we enter on collision
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>Generate a randomColor from an range and an existing color</summary>
        /// <param name="color">The color.</param>
        /// <param name="rangeLess">The less value of the range.</param>
        /// <param name="rangeHigh">The high value of the range.</param>
        /// <param name="rdn">The RDN.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static Color RandomColor(Color color, int rangeLess, int rangeHigh, Random rdn)
        {
            //Generate the channel from the random value of the range
            int r = color.R + (rdn.Next(rangeLess, rangeHigh));
            int b = color.B + (rdn.Next(rangeLess, rangeHigh));
            int g = color.G + (rdn.Next(rangeLess, rangeHigh));

            r = r < 0 ? 0 : r > 255 ? 255 : r;
            b = b < 0 ? 0 : b > 255 ? 255 : b;
            g = g < 0 ? 0 : g > 255 ? 255 : g;

            return Color.FromArgb(color.A, r, g, b);
        }

        private static Dictionary<string, HashSet<Bitmap>> verif = new Dictionary<string, HashSet<Bitmap>>();

        /// <summary>Change the color of the bitmap to the current foreground color</summary>
        /// <param name="image">The image.</param>
        public static void AsCurrentImageColor(Bitmap image)
        {
            var key = Game.game.ForegroundColor.ToString();
            if (Game.game.ForegroundColor != null)
            {
                //Check the image was already modify with the actual foreground color else add to the dictionnary and make the color transformation
                if (verif.ContainsKey(key))
                {
                    if (verif[key].Contains(image))
                    {
                        return;
                    }
                    verif[key].Add(image);
                }
                else
                {
                    verif.Add(key, new HashSet<Bitmap>() { image });
                }

                ColorTransformation(image, Game.game.ForegroundColor);
                return;
            }
        }

        /// <summary>Count the number of pixels with an opacity</summary>
        /// <param name="image">The image.</param>
        /// <returns>
        ///   The count of pixel
        /// </returns>
        public static int GetNbPixels(Bitmap image)
        {
            int sum = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).A == 255)
                    {
                        sum += 1;
                    }
                }
            }
            return sum;
        }

        /// <summary>Flips the bitmap.</summary>
        /// <param name="source">The source.</param>
        /// <returns>The flipped Bitmap</returns>
        public static Bitmap FlipBitmap(Bitmap source)
        {
            Bitmap dest = new Bitmap(source);
            dest.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return dest;
        }

        public static Bitmap ColorTransformation(Bitmap image, Color foregroundColor)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).A == 255)
                    {
                        image.SetPixel(x, y, foregroundColor);
                    }
                }
            }
            return image;
        }

        /// <summary>Deletes the pixels from the entity, when it's a collision.</summary>
        /// <param name="rMissile">The rectangle missile.</param>
        /// <param name="rEntity">The rectangle entity.</param>
        /// <param name="entity">The entity bitmap.</param>
        /// <param name="missile">The missile bitmap.</param>
        /// <param name="nbPixels">The nb pixels.</param>
        public static void DeleteData(Rectangle rEntity, Rectangle rMissile, Bitmap entity, Bitmap missile, out int nbPixels)
        {
            nbPixels = 0;
            Point positionMissile = new Point(rMissile.X, rMissile.Y);
            Point positionEntity = new Point(rEntity.X, rEntity.Y);
            Rectangle rSource2 = rMissile;
            rMissile.Intersect(rEntity);
            rEntity.Intersect(rSource2);
            Rectangle coerce = rMissile;
            if (!(coerce.Height == 0 || coerce.Width == 0))
            {
                for (int y = coerce.Y - (int)positionEntity.Y; y < coerce.Y - (int)positionEntity.Y + coerce.Height; y++)
                {
                    for (int x = coerce.X - (int)positionEntity.X; x < coerce.X - (int)positionEntity.X + coerce.Width; x++)
                    {
                        //Get the missile bitmap coordinate
                        int missileX = x - (coerce.X - (int)positionEntity.X) + (rMissile.X - positionMissile.X);
                        int missileY = y - (coerce.Y - (int)positionEntity.Y) + (rMissile.Y - positionMissile.Y);

                        //If two bitmap have a opacity different of zero it's a collision
                        if (entity.GetPixel(x, y).A != 0 && missile.GetPixel(missileX, missileY).A != 0)
                        {
                            entity.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
                            nbPixels++;
                        }
                    }
                }
            }
        }

        #endregion PublicStaticMethods
    }
}