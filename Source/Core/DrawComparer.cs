using System.Collections;
using TownOfYours.Core.Interfaces;

namespace TownOfYours.Core
{
    public class DrawComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if(x is IRenderable == false || y is IRenderable == false )
            {
                return 0;
            }

            if((x as IRenderable).States.DrawOder > (y as IRenderable).States.DrawOder)
            {
                return 1;
            }
            else if ((x as IRenderable).States.DrawOder < (y as IRenderable).States.DrawOder)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
