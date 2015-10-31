/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using LibLSLCC.CodeValidator.Enums;
using LibLSLCC.LibraryData.Reflection;
using vector = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Vector3;
using rotation = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Quaternion;
using LSLInteger = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLInteger;

namespace OpenSim.Region.ScriptEngine.Shared.ScriptBase
{
    public partial class ScriptBaseClass
    {
        // Constants for cmWindlight*
        [LSLConstant(LSLType.Integer)] public const int WL_WATER_COLOR = 0;
        [LSLConstant(LSLType.Integer)] public const int WL_WATER_FOG_DENSITY_EXPONENT = 1;
        [LSLConstant(LSLType.Integer)] public const int WL_UNDERWATER_FOG_MODIFIER = 2;
        [LSLConstant(LSLType.Integer)] public const int WL_REFLECTION_WAVELET_SCALE = 3;
        [LSLConstant(LSLType.Integer)] public const int WL_FRESNEL_SCALE = 4;
        [LSLConstant(LSLType.Integer)] public const int WL_FRESNEL_OFFSET = 5;
        [LSLConstant(LSLType.Integer)] public const int WL_REFRACT_SCALE_ABOVE = 6;
        [LSLConstant(LSLType.Integer)] public const int WL_REFRACT_SCALE_BELOW = 7;
        [LSLConstant(LSLType.Integer)] public const int WL_BLUR_MULTIPLIER = 8;
        [LSLConstant(LSLType.Integer)] public const int WL_BIG_WAVE_DIRECTION = 9;
        [LSLConstant(LSLType.Integer)] public const int WL_LITTLE_WAVE_DIRECTION = 10;
        [LSLConstant(LSLType.Integer)] public const int WL_NORMAL_MAP_TEXTURE = 11;
        [LSLConstant(LSLType.Integer)] public const int WL_HORIZON = 12;
        [LSLConstant(LSLType.Integer)] public const int WL_HAZE_HORIZON = 13;
        [LSLConstant(LSLType.Integer)] public const int WL_BLUE_DENSITY = 14;
        [LSLConstant(LSLType.Integer)] public const int WL_HAZE_DENSITY = 15;
        [LSLConstant(LSLType.Integer)] public const int WL_DENSITY_MULTIPLIER = 16;
        [LSLConstant(LSLType.Integer)] public const int WL_DISTANCE_MULTIPLIER = 17;
        [LSLConstant(LSLType.Integer)] public const int WL_MAX_ALTITUDE = 18;
        [LSLConstant(LSLType.Integer)] public const int WL_SUN_MOON_COLOR = 19;
        [LSLConstant(LSLType.Integer)] public const int WL_AMBIENT = 20;
        [LSLConstant(LSLType.Integer)] public const int WL_EAST_ANGLE = 21;
        [LSLConstant(LSLType.Integer)] public const int WL_SUN_GLOW_FOCUS = 22;
        [LSLConstant(LSLType.Integer)] public const int WL_SUN_GLOW_SIZE = 23;
        [LSLConstant(LSLType.Integer)] public const int WL_SCENE_GAMMA = 24;
        [LSLConstant(LSLType.Integer)] public const int WL_STAR_BRIGHTNESS = 25;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_COLOR = 26;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_XY_DENSITY = 27;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_COVERAGE = 28;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_SCALE = 29;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_DETAIL_XY_DENSITY = 30;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_SCROLL_X = 31;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_SCROLL_Y = 32;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_SCROLL_Y_LOCK = 33;
        [LSLConstant(LSLType.Integer)] public const int WL_CLOUD_SCROLL_X_LOCK = 34;
        [LSLConstant(LSLType.Integer)] public const int WL_DRAW_CLASSIC_CLOUDS = 35;
        [LSLConstant(LSLType.Integer)] public const int WL_SUN_MOON_POSITION = 36;

    }
}
