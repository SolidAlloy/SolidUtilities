namespace SolidUtilities
{
    using UnityEngine;

    /// <summary>
    /// Find the 0, 1 or 2 intersections between a line segment and an axis-aligned Rect.
    /// </summary>
    /// Uses spacial partitioning around the rect to perform the bare minimum raycasts necessary.
    ///
    /// Written by JohannesMP 2018-07-07, released under the Public Domain - No Rights Reserved.
    /// https://gist.github.com/JohannesMP/50dad3175bf2925df508b642091e41c4
    /// Modified by SolidAlloy
    public static class RectLineIntersectionHelper
    {
        /// <summary>
        /// Sectors around a rect
        /// </summary>
        /// Given a rectangle we divide the space around it into the following Sectors (S4 is the rectangle itself):
        ///      S0| S1 |S2
        ///      --+----+--
        ///      S3| S4 |S5
        ///      --+----+--
        ///      S6| S7 |S8
        ///
        /// Note that Sector enums are also used to identify the Side or Corner of the rectangle bordered by a sector
        private enum Sector
        {
            __, // No sector - underscore used to make viewing RaycastLookup below easier
            S0, S1, S2,
            S3, S4, S5,
            S6, S7, S8
        }

        /// <summary>
        /// What raycasts to perform for the sectors a line segment ends and starts in, and whether it is entering or exiting.
        /// </summary>
        /// This pre-computed lookup tables enables us to avoid unnecessary raycasts.
        ///
        /// Lookup is done as follows: [Sector1, Sector2, state(0:1)]
        ///     - state == 0: the line segment is entering the Rect
        ///     - state == 1: the line segment is exiting the Rect
        ///
        /// - If the result is Sector.__ a raycast would not return anything.
        /// - Otherwise the return enum indicates which Edge(s) should be raycast
        ///     - ex: S0 means that the edges that create the corner at sector 0 should be raycast, meaning Edge 1 and Edge 3.
        ///
        /// Examples:
        ///     RaycastLookup[1,4,0] == S1
        ///         - A line segment from Sector 1 to 4 enters the rect at Edge 1
        ///     RaycastLookup[1,4,1] == __
        ///         - A line segment from Sector 1 to 4 does not exit the rect
        ///
        ///     RaycastLookup[0,5,0] == S0
        ///         - A line segment from Sector 0 to 5 enters the rect at the Edges touching corner 0 (Edge 1 and Edge 3)
        ///     RaycastLookup[0,5,1] == S5
        ///         - A line segment from Sector 0 to 5 exits the rect at Edge 5
        private static readonly Sector[,,] _raycastLookup =
        {
            // From 0 - top left corner
            {
                {Sector.__, Sector.__},     {Sector.__, Sector.__},     {Sector.__, Sector.__},
                {Sector.__, Sector.__},     {Sector.S0, Sector.S4},     {Sector.S0, Sector.S5},
                {Sector.__, Sector.__},     {Sector.S0, Sector.S7},     {Sector.S0, Sector.S8},
            },
            // From 1 - top edge
            {
                {Sector.__, Sector.__},     {Sector.__, Sector.__},     {Sector.__, Sector.__},
                {Sector.S1, Sector.S3},     {Sector.S1, Sector.S4},     {Sector.S1, Sector.S5},
                {Sector.S1, Sector.S6},     {Sector.S1, Sector.S7},     {Sector.S1, Sector.S8},
            },
            // From 2 - top right corner
            {
                {Sector.__, Sector.__},     {Sector.__, Sector.__},     {Sector.__, Sector.__},
                {Sector.S2, Sector.S3},     {Sector.S2, Sector.S4},     {Sector.__, Sector.__},
                {Sector.S2, Sector.S6},     {Sector.S2, Sector.S7},     {Sector.__, Sector.__},
            },
            // From 3 - left edge
            {
                {Sector.__, Sector.__},     {Sector.S3, Sector.S1},     {Sector.S3, Sector.S2},
                {Sector.__, Sector.__},     {Sector.S3, Sector.S4},     {Sector.S3, Sector.S5},
                {Sector.__, Sector.__},     {Sector.S3, Sector.S7},     {Sector.S3, Sector.S8},
            },
            // From 4 - center; no entry, only exit
            {
                {Sector.S4, Sector.S0},     {Sector.S4, Sector.S1},     {Sector.S4, Sector.S2},
                {Sector.S4, Sector.S3},     {Sector.S4, Sector.S4},     {Sector.S4, Sector.S5},
                {Sector.S4, Sector.S6},     {Sector.S4, Sector.S7},     {Sector.S4, Sector.S8},
            },
            // From 5 - right edge
            {
                {Sector.S5, Sector.S0},     {Sector.S5, Sector.S1},     {Sector.__, Sector.__},
                {Sector.S5, Sector.S3},     {Sector.S5, Sector.S4},     {Sector.__, Sector.__},
                {Sector.S5, Sector.S6},     {Sector.S5, Sector.S7},     {Sector.__, Sector.__},
            },
            // From 6 - bottom left corner
            {
                {Sector.__, Sector.__},     {Sector.S6, Sector.S1},     {Sector.S6, Sector.S2},
                {Sector.__, Sector.__},     {Sector.S6, Sector.S4},     {Sector.S6, Sector.S5},
                {Sector.__, Sector.__},     {Sector.__, Sector.__},     {Sector.__, Sector.__},
            },
            // From 7 - bottom edge
            {
                {Sector.S7, Sector.S0},     {Sector.S7, Sector.S1},     {Sector.S7, Sector.S2},
                {Sector.S7, Sector.S3},     {Sector.S7, Sector.S4},     {Sector.S7, Sector.S5},
                {Sector.__, Sector.__},     {Sector.__, Sector.__},     {Sector.__, Sector.__},
            },
            // From 8 - bottom right corner
            {
                {Sector.S8, Sector.S0},   {Sector.S8, Sector.S1},   {Sector.__, Sector.__},
                {Sector.S8, Sector.S3},   {Sector.S8, Sector.S4},   {Sector.__, Sector.__},
                {Sector.__, Sector.__},   {Sector.__, Sector.__},   {Sector.__, Sector.__},
            },
        };

        /// <summary>
        /// Find intersects of a Line segment and an axis-aligned Rect
        /// </summary>
        /// <returns>
        /// entry: The fraction along the line segment where it entered the rect. 0 if starting inside the rect. null if no hit.
        /// exit: The fraction along the line segment where it exited the rect. 1 if ending inside the rect. null if not hit.
        /// </returns>
        public static (float? entry, float? exit) GetLineIntersections(this Rect rect, Vector2 begin, Vector2 end)
        {
            Vector2 dir = end - begin;

            int sectorBegin = GetRectPointSector(rect, begin);
            int sectorEnd   = GetRectPointSector(rect, end);

            var entry = GetRayToRectSide(begin, dir, _raycastLookup[sectorBegin, sectorEnd, 0], rect, 0f);
            var exit = GetRayToRectSide(begin, dir, _raycastLookup[sectorBegin, sectorEnd, 1], rect, 1f);

            return (entry, exit);
        }

        /// <summary>
        /// Check in which sector relative to a rect a point is located in.
        /// </summary>
        ///
        /// Return value maps to the rect as follows, where 4 is inclusive:
        ///     0| 1 |2
        ///     -+---+-   ^
        ///     3| 4 |5   |
        ///     -+---+-   y
        ///     6| 7 |8    x-->
        ///
        /// <returns>The sector relative to the rect that the point is in</returns>
        private static int GetRectPointSector(Rect rect, Vector2 point)
        {
            // 0, 1 or 2
            if(point.y > rect.yMax)
            {
                if (point.x < rect.xMin)
                    return 0;

                return point.x > rect.xMax ? 2 : 1;
            }

            // 6, 7, or 8
            if(point.y < rect.yMin)
            {
                if (point.x < rect.xMin)
                    return 6;

                return point.x > rect.xMax ? 8 : 7;
            }

            // 3, 4 or 5
            if (point.x < rect.xMin)
                return 3;

            return point.x > rect.xMax ? 5 : 4;
        }

        /// <summary>
        /// Perform the raycast on an edge or two edges on a corner of a rect.
        /// </summary>
        /// Edges are a guaranteed single raycast
        /// Corners consist of two edges to raycast. if the first is a miss, return the second.
        ///
        /// <param name="side">The Direction(s) of the rect to raycast against</param>
        /// <param name="r4val">The intersect value to return for when we start or stop in the rect</param>
        /// <returns>The fraction along the ray</returns>
        private static float? GetRayToRectSide(Vector2 begin, Vector2 dir, Sector side, Rect rect, float r4val)
        {
            return side switch
            {
                // Raycast Corner S0 - consists of S1 and S3
                Sector.S0 => GetRayToRectSide(begin, dir, Sector.S1, rect, r4val) ?? GetRayToRectSide(begin, dir, Sector.S3, rect, r4val),
                // Raycast Edge S1 - Horizontal
                Sector.S1 => RayToHorizontal(begin, dir, rect.xMin, rect.yMax, rect.width),
                // Raycast Corner S2 - consists of S1 and S5
                Sector.S2 => GetRayToRectSide(begin, dir, Sector.S1, rect, r4val) ?? GetRayToRectSide(begin, dir, Sector.S5, rect, r4val),
                // Raycast Edge S3 - Vertical
                Sector.S3 => RayToVert(begin, dir, rect.xMin, rect.yMin, rect.height),
                // Center S4 - return the intersection value provided by caller
                Sector.S4 => r4val,
                // Raycast Edge S5 - Vertical
                Sector.S5 => RayToVert(begin, dir, rect.xMax, rect.yMin, rect.height),
                // Raycast Corner S6 - consists of S3 and S7
                Sector.S6 => GetRayToRectSide(begin, dir, Sector.S3, rect, r4val) ?? GetRayToRectSide(begin, dir, Sector.S7, rect, r4val),
                // Raycast Edge S7 - Horizontal
                Sector.S7 => RayToHorizontal(begin, dir, rect.xMin, rect.yMin, rect.width),
                // Raycast Corner S8 - consists of S5 and S7
                Sector.S8 => GetRayToRectSide(begin, dir, Sector.S5, rect, r4val) ?? GetRayToRectSide(begin, dir, Sector.S7, rect, r4val),
                _ => null
            };
        }

        /// <summary>
        /// Check if a given line segment and a strictly Horizontal line segment intersect. float.PositiveInfinity if no hit.
        /// </summary>
        /// /// <param name="width">The width of the horizontal line segment. Is Assumed non-zero.</param>
        /// <returns>The parametric fraction where the intersection lies on the 'from' segment. float.PositiveInfinity if no intersection</returns>
        private static float? RayToHorizontal(Vector2 fromPoint, Vector2 fromDir, float x, float y, float width)
        {
            // 1. Check if the extended horizontal line could intersect the segment
            float fromParam = (y - fromPoint.y) / fromDir.y;
            if (fromParam < 0 || fromParam > 1)
                return null;

            // 2. Check if the horizontal line could reach the segment
            float lineParam = (fromPoint.x + fromDir.x * fromParam - x) / width;
            if (lineParam < 0 || lineParam > 1)
                return null;

            return fromParam;
        }

        /// <summary>
        /// If a given line segment and a strictly Vertical line segment intersect. float.PositiveInfinity if no intersection.
        /// </summary>
        /// <param name="height">The height of the vertical line segment. Is Assumed non-zero.</param>
        /// <returns>The parametric fraction where the intersection lies on the 'from' segment. float.PositiveInfinity if no intersection</returns>
        private static float? RayToVert(Vector2 fromPoint, Vector2 fromDir, float x, float y, float height)
        {
            float fromParam = (x - fromPoint.x) / fromDir.x;
            // 1. Check if the extended vertical line could intersect the segment
            if (fromParam < 0 || fromParam > 1)
                return null;

            // 2. Check if the vertical line could reach the segment
            float lineParam = (fromPoint.y + fromDir.y * fromParam - y) / height;
            if (lineParam < 0 || lineParam > 1)
                return null;

            return fromParam;
        }
    }
}