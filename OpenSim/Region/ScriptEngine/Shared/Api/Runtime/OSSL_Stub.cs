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
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using LibLSLCC.CodeValidator.Enums;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.ScriptEngine.Interfaces;
using OpenSim.Region.ScriptEngine.Shared.Api.Interfaces;
using integer = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLInteger;
using vector = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Vector3;
using rotation = OpenSim.Region.ScriptEngine.Shared.LSL_Types.Quaternion;
using key = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLString;
using LSL_List = OpenSim.Region.ScriptEngine.Shared.LSL_Types.list;
using LSL_String = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLString;
using LSL_Key = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLString;
using LSL_Float = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLFloat;
using LSL_Integer = OpenSim.Region.ScriptEngine.Shared.LSL_Types.LSLInteger;
using LibLSLCC.LibraryData.Reflection;

namespace OpenSim.Region.ScriptEngine.Shared.ScriptBase
{
    public partial class ScriptBaseClass : MarshalByRefObject
    {
        public IOSSL_Api m_OSSL_Functions;

        public void ApiTypeOSSL(IScriptApi api)
        {
            if (!(api is IOSSL_Api))
                return;

            m_OSSL_Functions = (IOSSL_Api) api;

            Prim = new OSSLPrim(this);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetRegionWaterHeight([LSLParam(LSLType.Float)] double height)
        {
            m_OSSL_Functions.osSetRegionWaterHeight(height);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetRegionSunSettings([LSLParam(LSLType.Integer)] bool useEstateSun, [LSLParam(LSLType.Integer)] bool sunFixed, [LSLParam(LSLType.Float)] double sunHour)
        {
            m_OSSL_Functions.osSetRegionSunSettings(useEstateSun, sunFixed, sunHour);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetEstateSunSettings([LSLParam(LSLType.Integer)] bool sunFixed, [LSLParam(LSLType.Float)] double sunHour)
        {
            m_OSSL_Functions.osSetEstateSunSettings(sunFixed, sunHour);
        }

        [LSLFunction(LSLType.Float)]
        public double osGetCurrentSunHour()
        {
            return m_OSSL_Functions.osGetCurrentSunHour();
        }

        [LSLFunction(LSLType.Float)]
        public double osGetSunParam([LSLParam(LSLType.String)] string param)
        {
            return m_OSSL_Functions.osGetSunParam(param);
        }

        // Deprecated
        [LSLFunction(LSLType.Float, Deprecated = true)]
        public double osSunGetParam([LSLParam(LSLType.String)] string param)
        {
            return m_OSSL_Functions.osSunGetParam(param);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetSunParam([LSLParam(LSLType.String)] string param, [LSLParam(LSLType.Float)] double value)
        {
            m_OSSL_Functions.osSetSunParam(param, value);
        }

        // Deprecated
        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void osSunSetParam([LSLParam(LSLType.String)] string param, [LSLParam(LSLType.Float)] double value)
        {
            m_OSSL_Functions.osSunSetParam(param, value);
        }

        [LSLFunction(LSLType.String)]
        public string osWindActiveModelPluginName()
        {
            return m_OSSL_Functions.osWindActiveModelPluginName();
        }

        [LSLFunction(LSLType.Void)]
        public void osSetWindParam([LSLParam(LSLType.String)] string plugin, [LSLParam(LSLType.String)] string param, [LSLParam(LSLType.Float)] LSL_Float value)
        {
            m_OSSL_Functions.osSetWindParam(plugin, param, value);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float osGetWindParam([LSLParam(LSLType.String)] string plugin, [LSLParam(LSLType.String)] string param)
        {
            return m_OSSL_Functions.osGetWindParam(plugin, param);
        }

        [LSLFunction(LSLType.Void)]
        public void osParcelJoin([LSLParam(LSLType.Vector)] vector pos1, [LSLParam(LSLType.Vector)] vector pos2)
        {
            m_OSSL_Functions.osParcelJoin(pos1, pos2);
        }

        [LSLFunction(LSLType.Void)]
        public void osParcelSubdivide([LSLParam(LSLType.Vector)] vector pos1, [LSLParam(LSLType.Vector)] vector pos2)
        {
            m_OSSL_Functions.osParcelSubdivide(pos1, pos2);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetParcelDetails([LSLParam(LSLType.Vector)] vector pos, [LSLParam(LSLType.List)] LSL_List rules)
        {
            m_OSSL_Functions.osSetParcelDetails(pos, rules);
        }

        // Deprecated
        [LSLFunction(LSLType.Void, Deprecated = true)]
        public void osParcelSetDetails([LSLParam(LSLType.Vector)] vector pos, [LSLParam(LSLType.List)] LSL_List rules)
        {
            m_OSSL_Functions.osParcelSetDetails(pos, rules);
        }

        [LSLFunction(LSLType.Float)]
        public double osList2Double([LSLParam(LSLType.List)] LSL_Types.list src, [LSLParam(LSLType.Integer)] int index)
        {
            return m_OSSL_Functions.osList2Double(src, index);
        }

        [LSLFunction(LSLType.String)]
        public string osSetDynamicTextureURL([LSLParam(LSLType.String)] string dynamicID, [LSLParam(LSLType.String)] string contentType, [LSLParam(LSLType.String)] string url, [LSLParam(LSLType.String)] string extraParams, [LSLParam(LSLType.Integer)] int timer)
        {
            return m_OSSL_Functions.osSetDynamicTextureURL(dynamicID, contentType, url, extraParams, timer);
        }

        [LSLFunction(LSLType.String)]
        public string osSetDynamicTextureData([LSLParam(LSLType.String)] string dynamicID, [LSLParam(LSLType.String)] string contentType, [LSLParam(LSLType.String)] string data, [LSLParam(LSLType.String)] string extraParams, [LSLParam(LSLType.Integer)] int timer)
        {
            return m_OSSL_Functions.osSetDynamicTextureData(dynamicID, contentType, data, extraParams, timer);
        }

        [LSLFunction(LSLType.String)]
        public string osSetDynamicTextureURLBlend([LSLParam(LSLType.String)] string dynamicID, [LSLParam(LSLType.String)] string contentType, [LSLParam(LSLType.String)] string url, [LSLParam(LSLType.String)] string extraParams, [LSLParam(LSLType.Integer)] int timer, [LSLParam(LSLType.Integer)] int alpha)
        {
            return m_OSSL_Functions.osSetDynamicTextureURLBlend(dynamicID, contentType, url, extraParams, timer, alpha);
        }

        [LSLFunction(LSLType.String)]
        public string osSetDynamicTextureDataBlend([LSLParam(LSLType.String)] string dynamicID, [LSLParam(LSLType.String)] string contentType, [LSLParam(LSLType.String)] string data, [LSLParam(LSLType.String)] string extraParams, [LSLParam(LSLType.Integer)] int timer, [LSLParam(LSLType.Integer)] int alpha)
        {
            return m_OSSL_Functions.osSetDynamicTextureDataBlend(dynamicID, contentType, data, extraParams, timer, alpha);
        }

        [LSLFunction(LSLType.String)]
        public string osSetDynamicTextureURLBlendFace(
            [LSLParam(LSLType.String)] string dynamicID, 
            [LSLParam(LSLType.String)] string contentType, 
            [LSLParam(LSLType.String)] string url, 
            [LSLParam(LSLType.String)] string extraParams,
            [LSLParam(LSLType.Integer)] bool blend, 
            [LSLParam(LSLType.Integer)] int disp, 
            [LSLParam(LSLType.Integer)] int timer, 
            [LSLParam(LSLType.Integer)] int alpha, 
            [LSLParam(LSLType.Integer)] int face)
        {
            return m_OSSL_Functions.osSetDynamicTextureURLBlendFace(dynamicID, contentType, url, extraParams,
                blend, disp, timer, alpha, face);
        }

        [LSLFunction(LSLType.String)]
        public string osSetDynamicTextureDataBlendFace(
            [LSLParam(LSLType.String)] string dynamicID, 
            [LSLParam(LSLType.String)] string contentType, 
            [LSLParam(LSLType.String)] string data, 
            [LSLParam(LSLType.String)] string extraParams,
            [LSLParam(LSLType.Integer)] bool blend, 
            [LSLParam(LSLType.Integer)] int disp, 
            [LSLParam(LSLType.Integer)] int timer, 
            [LSLParam(LSLType.Integer)] int alpha, 
            [LSLParam(LSLType.Integer)] int face)
        {
            return m_OSSL_Functions.osSetDynamicTextureDataBlendFace(dynamicID, contentType, data, extraParams,
                blend, disp, timer, alpha, face);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float osGetTerrainHeight([LSLParam(LSLType.Integer)] int x, [LSLParam(LSLType.Integer)] int y)
        {
            return m_OSSL_Functions.osGetTerrainHeight(x, y);
        }

        // Deprecated
        [LSLFunction(LSLType.Float, Deprecated = true)]
        public LSL_Float osTerrainGetHeight([LSLParam(LSLType.Integer)] int x, [LSLParam(LSLType.Integer)] int y)
        {
            return m_OSSL_Functions.osTerrainGetHeight(x, y);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osSetTerrainHeight([LSLParam(LSLType.Integer)] int x, [LSLParam(LSLType.Integer)] int y, [LSLParam(LSLType.Float)] double val)
        {
            return m_OSSL_Functions.osSetTerrainHeight(x, y, val);
        }

        // Deprecated
        [LSLFunction(LSLType.Integer, Deprecated = true)]
        public LSL_Integer osTerrainSetHeight([LSLParam(LSLType.Integer)] int x, [LSLParam(LSLType.Integer)] int y, [LSLParam(LSLType.Float)] double val)
        {
            return m_OSSL_Functions.osTerrainSetHeight(x, y, val);
        }

        [LSLFunction(LSLType.Void)]
        public void osTerrainFlush()
        {
            m_OSSL_Functions.osTerrainFlush();
        }

        [LSLFunction(LSLType.Integer)]
        public int osRegionRestart([LSLParam(LSLType.Float)] double seconds)
        {
            return m_OSSL_Functions.osRegionRestart(seconds);
        }

        [LSLFunction(LSLType.Void)]
        public void osRegionNotice([LSLParam(LSLType.String)] string msg)
        {
            m_OSSL_Functions.osRegionNotice(msg);
        }

        public bool osConsoleCommand([LSLParam(LSLType.String)] string Command)
        {
            return m_OSSL_Functions.osConsoleCommand(Command);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetParcelMediaURL([LSLParam(LSLType.String)] string url)
        {
            m_OSSL_Functions.osSetParcelMediaURL(url);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetParcelSIPAddress([LSLParam(LSLType.String)] string SIPAddress)
        {
            m_OSSL_Functions.osSetParcelSIPAddress(SIPAddress);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetPrimFloatOnWater([LSLParam(LSLType.Integer)] int floatYN)
        {
            m_OSSL_Functions.osSetPrimFloatOnWater(floatYN);
        }

        // Teleport Functions

        [LSLFunction(LSLType.Void)]
        public void osTeleportAgent([LSLParam(LSLType.String)] string agent, [LSLParam(LSLType.String)] string regionName, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Vector)] vector lookat)
        {
            m_OSSL_Functions.osTeleportAgent(agent, regionName, position, lookat);
        }

        [LSLFunction(LSLType.Void)]
        public void osTeleportAgent([LSLParam(LSLType.String)] string agent, [LSLParam(LSLType.Integer)] int regionX, [LSLParam(LSLType.Integer)] int regionY, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Vector)] vector lookat)
        {
            m_OSSL_Functions.osTeleportAgent(agent, regionX, regionY, position, lookat);
        }

        [LSLFunction(LSLType.Void)]
        public void osTeleportAgent([LSLParam(LSLType.String)] string agent, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Vector)] vector lookat)
        {
            m_OSSL_Functions.osTeleportAgent(agent, position, lookat);
        }

        [LSLFunction(LSLType.Void)]
        public void osTeleportOwner([LSLParam(LSLType.String)] string regionName, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Vector)] vector lookat)
        {
            m_OSSL_Functions.osTeleportOwner(regionName, position, lookat);
        }

        [LSLFunction(LSLType.Void)]
        public void osTeleportOwner([LSLParam(LSLType.Integer)] int regionX, [LSLParam(LSLType.Integer)] int regionY, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Vector)] vector lookat)
        {
            m_OSSL_Functions.osTeleportOwner(regionX, regionY, position, lookat);
        }

        [LSLFunction(LSLType.Void)]
        public void osTeleportOwner([LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Vector)] vector lookat)
        {
            m_OSSL_Functions.osTeleportOwner(position, lookat);
        }

        // Avatar info functions
        [LSLFunction(LSLType.String)]
        public string osGetAgentIP([LSLParam(LSLType.String)] string agent)
        {
            return m_OSSL_Functions.osGetAgentIP(agent);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osGetAgents()
        {
            return m_OSSL_Functions.osGetAgents();
        }

        // Animation Functions

        [LSLFunction(LSLType.Void)]
        public void osAvatarPlayAnimation([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.String)] string animation)
        {
            m_OSSL_Functions.osAvatarPlayAnimation(avatar, animation);
        }

        [LSLFunction(LSLType.Void)]
        public void osAvatarStopAnimation([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.String)] string animation)
        {
            m_OSSL_Functions.osAvatarStopAnimation(avatar, animation);
        }

        #region Attachment commands

        [LSLFunction(LSLType.Void)]
        public void osForceAttachToAvatar([LSLParam(LSLType.Integer)] int attachmentPoint)
        {
            m_OSSL_Functions.osForceAttachToAvatar(attachmentPoint);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceAttachToAvatarFromInventory([LSLParam(LSLType.String)] string itemName, [LSLParam(LSLType.Integer)] int attachmentPoint)
        {
            m_OSSL_Functions.osForceAttachToAvatarFromInventory(itemName, attachmentPoint);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceAttachToOtherAvatarFromInventory([LSLParam(LSLType.String)] string rawAvatarId, [LSLParam(LSLType.String)] string itemName, [LSLParam(LSLType.Integer)] int attachmentPoint)
        {
            m_OSSL_Functions.osForceAttachToOtherAvatarFromInventory(rawAvatarId, itemName, attachmentPoint);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceDetachFromAvatar()
        {
            m_OSSL_Functions.osForceDetachFromAvatar();
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osGetNumberOfAttachments([LSLParam(LSLType.Key)] LSL_Key avatar, [LSLParam(LSLType.List)] LSL_List attachmentPoints)
        {
            return m_OSSL_Functions.osGetNumberOfAttachments(avatar, attachmentPoints);
        }

        [LSLFunction(LSLType.Void)]
        public void osMessageAttachments([LSLParam(LSLType.Key)] LSL_Key avatar, [LSLParam(LSLType.String)] string message, [LSLParam(LSLType.List)] LSL_List attachmentPoints, [LSLParam(LSLType.Integer)] int flags)
        {
            m_OSSL_Functions.osMessageAttachments(avatar, message, attachmentPoints, flags);
        }

        #endregion

        // Texture Draw functions

        [LSLFunction(LSLType.String)]
        public string osMovePen([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int x, [LSLParam(LSLType.Integer)] int y)
        {
            return m_OSSL_Functions.osMovePen(drawList, x, y);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawLine([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int startX, [LSLParam(LSLType.Integer)] int startY, [LSLParam(LSLType.Integer)] int endX, [LSLParam(LSLType.Integer)] int endY)
        {
            return m_OSSL_Functions.osDrawLine(drawList, startX, startY, endX, endY);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawLine([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int endX, [LSLParam(LSLType.Integer)] int endY)
        {
            return m_OSSL_Functions.osDrawLine(drawList, endX, endY);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawText([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.String)] string text)
        {
            return m_OSSL_Functions.osDrawText(drawList, text);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawEllipse([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int width, [LSLParam(LSLType.Integer)] int height)
        {
            return m_OSSL_Functions.osDrawEllipse(drawList, width, height);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawRectangle([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int width, [LSLParam(LSLType.Integer)] int height)
        {
            return m_OSSL_Functions.osDrawRectangle(drawList, width, height);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawFilledRectangle([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int width, [LSLParam(LSLType.Integer)] int height)
        {
            return m_OSSL_Functions.osDrawFilledRectangle(drawList, width, height);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawPolygon([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.List)] LSL_List x, [LSLParam(LSLType.List)] LSL_List y)
        {
            return m_OSSL_Functions.osDrawPolygon(drawList, x, y);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawFilledPolygon([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.List)] LSL_List x, [LSLParam(LSLType.List)] LSL_List y)
        {
            return m_OSSL_Functions.osDrawFilledPolygon(drawList, x, y);
        }

        [LSLFunction(LSLType.String)]
        public string osSetFontSize([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int fontSize)
        {
            return m_OSSL_Functions.osSetFontSize(drawList, fontSize);
        }

        [LSLFunction(LSLType.String)]
        public string osSetFontName([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.String)] string fontName)
        {
            return m_OSSL_Functions.osSetFontName(drawList, fontName);
        }

        [LSLFunction(LSLType.String)]
        public string osSetPenSize([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int penSize)
        {
            return m_OSSL_Functions.osSetPenSize(drawList, penSize);
        }

        [LSLFunction(LSLType.String)]
        public string osSetPenCap([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.String)] string direction, [LSLParam(LSLType.String)] string type)
        {
            return m_OSSL_Functions.osSetPenCap(drawList, direction, type);
        }

        [LSLFunction(LSLType.String)]
        public string osSetPenColor([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.String)] string color)
        {
            return m_OSSL_Functions.osSetPenColor(drawList, color);
        }

        // Deprecated
        [LSLFunction(LSLType.String, Deprecated = true)]
        public string osSetPenColour([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.String)] string colour)
        {
            return m_OSSL_Functions.osSetPenColour(drawList, colour);
        }

        [LSLFunction(LSLType.String)]
        public string osDrawImage([LSLParam(LSLType.String)] string drawList, [LSLParam(LSLType.Integer)] int width, [LSLParam(LSLType.Integer)] int height, [LSLParam(LSLType.String)] string imageUrl)
        {
            return m_OSSL_Functions.osDrawImage(drawList, width, height, imageUrl);
        }

        [LSLFunction(LSLType.Vector)]
        public vector osGetDrawStringSize([LSLParam(LSLType.String)] string contentType, [LSLParam(LSLType.String)] string text, [LSLParam(LSLType.String)] string fontName, [LSLParam(LSLType.Integer)] int fontSize)
        {
            return m_OSSL_Functions.osGetDrawStringSize(contentType, text, fontName, fontSize);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetStateEvents([LSLParam(LSLType.Integer)] int events)
        {
            m_OSSL_Functions.osSetStateEvents(events);
        }

        [LSLFunction(LSLType.String)]
        public string osGetScriptEngineName()
        {
            return m_OSSL_Functions.osGetScriptEngineName();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osCheckODE()
        {
            return m_OSSL_Functions.osCheckODE();
        }

        [LSLFunction(LSLType.String)]
        public string osGetPhysicsEngineType()
        {
            return m_OSSL_Functions.osGetPhysicsEngineType();
        }

        [LSLFunction(LSLType.String)]
        public string osGetSimulatorVersion()
        {
            return m_OSSL_Functions.osGetSimulatorVersion();
        }

        public Hashtable osParseJSON([LSLParam(LSLType.String)] string JSON)
        {
            return m_OSSL_Functions.osParseJSON(JSON);
        }

        public Object osParseJSONNew([LSLParam(LSLType.String)] string JSON)
        {
            return m_OSSL_Functions.osParseJSONNew(JSON);
        }

        [LSLFunction(LSLType.Void)]
        public void osMessageObject([LSLParam(LSLType.Key)] key objectUUID, [LSLParam(LSLType.String)] string message)
        {
            m_OSSL_Functions.osMessageObject(objectUUID, message);
        }

        [LSLFunction(LSLType.Void)]
        public void osMakeNotecard([LSLParam(LSLType.String)] string notecardName, [LSLParam(LSLType.List)] LSL_Types.list contents)
        {
            m_OSSL_Functions.osMakeNotecard(notecardName, contents);
        }

        [LSLFunction(LSLType.String)]
        public string osGetNotecardLine([LSLParam(LSLType.String)] string name, [LSLParam(LSLType.Integer)] int line)
        {
            return m_OSSL_Functions.osGetNotecardLine(name, line);
        }

        [LSLFunction(LSLType.String)]
        public string osGetNotecard([LSLParam(LSLType.String)] string name)
        {
            return m_OSSL_Functions.osGetNotecard(name);
        }

        [LSLFunction(LSLType.Integer)]
        public int osGetNumberOfNotecardLines([LSLParam(LSLType.String)] string name)
        {
            return m_OSSL_Functions.osGetNumberOfNotecardLines(name);
        }

        [LSLFunction(LSLType.String)]
        public string osAvatarName2Key([LSLParam(LSLType.String)] string firstname, [LSLParam(LSLType.String)] string lastname)
        {
            return m_OSSL_Functions.osAvatarName2Key(firstname, lastname);
        }

        [LSLFunction(LSLType.String)]
        public string osKey2Name([LSLParam(LSLType.String)] string id)
        {
            return m_OSSL_Functions.osKey2Name(id);
        }

        [LSLFunction(LSLType.String)]
        public string osGetGridNick()
        {
            return m_OSSL_Functions.osGetGridNick();
        }

        [LSLFunction(LSLType.String)]
        public string osGetGridName()
        {
            return m_OSSL_Functions.osGetGridName();
        }

        [LSLFunction(LSLType.String)]
        public string osGetGridLoginURI()
        {
            return m_OSSL_Functions.osGetGridLoginURI();
        }

        [LSLFunction(LSLType.String)]
        public string osGetGridHomeURI()
        {
            return m_OSSL_Functions.osGetGridHomeURI();
        }

        [LSLFunction(LSLType.String)]
        public string osGetGridGatekeeperURI()
        {
            return m_OSSL_Functions.osGetGridGatekeeperURI();
        }

        [LSLFunction(LSLType.String)]
        public string osGetGridCustom([LSLParam(LSLType.String)] string key)
        {
            return m_OSSL_Functions.osGetGridCustom(key);
        }

        [LSLFunction(LSLType.String)]
        public string osGetAvatarHomeURI([LSLParam(LSLType.String)] string uuid)
        {
            return m_OSSL_Functions.osGetAvatarHomeURI(uuid);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String osFormatString([LSLParam(LSLType.String)] string str, [LSLParam(LSLType.List)] LSL_List strings)
        {
            return m_OSSL_Functions.osFormatString(str, strings);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osMatchString([LSLParam(LSLType.String)] string src, [LSLParam(LSLType.String)] string pattern, [LSLParam(LSLType.Integer)] int start)
        {
            return m_OSSL_Functions.osMatchString(src, pattern, start);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String osReplaceString([LSLParam(LSLType.String)] string src, [LSLParam(LSLType.String)] string pattern, [LSLParam(LSLType.String)] string replace, [LSLParam(LSLType.Integer)] int count, [LSLParam(LSLType.Integer)] int start)
        {
            return m_OSSL_Functions.osReplaceString(src, pattern, replace, count, start);
        }


        // Information about data loaded into the region
        [LSLFunction(LSLType.String)]
        public string osLoadedCreationDate()
        {
            return m_OSSL_Functions.osLoadedCreationDate();
        }

        [LSLFunction(LSLType.String)]
        public string osLoadedCreationTime()
        {
            return m_OSSL_Functions.osLoadedCreationTime();
        }

        [LSLFunction(LSLType.String)]
        public string osLoadedCreationID()
        {
            return m_OSSL_Functions.osLoadedCreationID();
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osGetLinkPrimitiveParams([LSLParam(LSLType.Integer)] int linknumber, [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_OSSL_Functions.osGetLinkPrimitiveParams(linknumber, rules);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceCreateLink([LSLParam(LSLType.String)] string target, [LSLParam(LSLType.Integer)] int parent)
        {
            m_OSSL_Functions.osForceCreateLink(target, parent);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceBreakLink([LSLParam(LSLType.Integer)] int linknum)
        {
            m_OSSL_Functions.osForceBreakLink(linknum);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceBreakAllLinks()
        {
            m_OSSL_Functions.osForceBreakAllLinks();
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osIsNpc([LSLParam(LSLType.Key)] LSL_Key npc)
        {
            return m_OSSL_Functions.osIsNpc(npc);
        }

        [LSLFunction(LSLType.Key)]
        public key osNpcCreate([LSLParam(LSLType.String)] string user, [LSLParam(LSLType.String)] string name, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Key)] key cloneFrom)
        {
            return m_OSSL_Functions.osNpcCreate(user, name, position, cloneFrom);
        }

        [LSLFunction(LSLType.Key)]
        public key osNpcCreate([LSLParam(LSLType.String)] string user, [LSLParam(LSLType.String)] string name, [LSLParam(LSLType.Vector)] vector position, [LSLParam(LSLType.Key)] key cloneFrom, [LSLParam(LSLType.Integer)] int options)
        {
            return m_OSSL_Functions.osNpcCreate(user, name, position, cloneFrom, options);
        }

        [LSLFunction(LSLType.Key)]
        public key osNpcSaveAppearance([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.String)] string notecard)
        {
            return m_OSSL_Functions.osNpcSaveAppearance(npc, notecard);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcLoadAppearance([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.String)] string notecard)
        {
            m_OSSL_Functions.osNpcLoadAppearance(npc, notecard);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key osNpcGetOwner([LSLParam(LSLType.Key)] LSL_Key npc)
        {
            return m_OSSL_Functions.osNpcGetOwner(npc);
        }

        [LSLFunction(LSLType.Vector)]
        public vector osNpcGetPos([LSLParam(LSLType.Key)] LSL_Key npc)
        {
            return m_OSSL_Functions.osNpcGetPos(npc);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcMoveTo([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.Vector)] vector position)
        {
            m_OSSL_Functions.osNpcMoveTo(npc, position);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcMoveToTarget([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.Vector)] vector target, [LSLParam(LSLType.Integer)] int options)
        {
            m_OSSL_Functions.osNpcMoveToTarget(npc, target, options);
        }

        [LSLFunction(LSLType.Rotation)]
        public rotation osNpcGetRot([LSLParam(LSLType.Key)] key npc)
        {
            return m_OSSL_Functions.osNpcGetRot(npc);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcSetRot([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.Rotation)] rotation rot)
        {
            m_OSSL_Functions.osNpcSetRot(npc, rot);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcStopMoveToTarget([LSLParam(LSLType.Key)] LSL_Key npc)
        {
            m_OSSL_Functions.osNpcStopMoveToTarget(npc);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcSay([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.String)] string message)
        {
            m_OSSL_Functions.osNpcSay(npc, message);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcSay([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.Integer)] int channel, [LSLParam(LSLType.String)] string message)
        {
            m_OSSL_Functions.osNpcSay(npc, channel, message);
        }


        [LSLFunction(LSLType.Void)]
        public void osNpcShout([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.Integer)] int channel, [LSLParam(LSLType.String)] string message)
        {
            m_OSSL_Functions.osNpcShout(npc, channel, message);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcSit([LSLParam(LSLType.Key)] LSL_Key npc, [LSLParam(LSLType.Key)] LSL_Key target, [LSLParam(LSLType.Integer)] int options)
        {
            m_OSSL_Functions.osNpcSit(npc, target, options);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcStand([LSLParam(LSLType.Key)] LSL_Key npc)
        {
            m_OSSL_Functions.osNpcStand(npc);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcRemove([LSLParam(LSLType.Key)] key npc)
        {
            m_OSSL_Functions.osNpcRemove(npc);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcPlayAnimation([LSLParam(LSLType.Key)] LSL_Key npc, [LSLParam(LSLType.String)] string animation)
        {
            m_OSSL_Functions.osNpcPlayAnimation(npc, animation);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcStopAnimation([LSLParam(LSLType.Key)] LSL_Key npc, [LSLParam(LSLType.String)] string animation)
        {
            m_OSSL_Functions.osNpcStopAnimation(npc, animation);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcWhisper([LSLParam(LSLType.Key)] key npc, [LSLParam(LSLType.Integer)] int channel, [LSLParam(LSLType.String)] string message)
        {
            m_OSSL_Functions.osNpcWhisper(npc, channel, message);
        }

        [LSLFunction(LSLType.Void)]
        public void osNpcTouch([LSLParam(LSLType.Key)] LSL_Key npcLSL_Key, [LSLParam(LSLType.Key)] LSL_Key object_key, [LSLParam(LSLType.Integer)] LSL_Integer link_num)
        {
            m_OSSL_Functions.osNpcTouch(npcLSL_Key, object_key, link_num);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key osOwnerSaveAppearance([LSLParam(LSLType.String)] string notecard)
        {
            return m_OSSL_Functions.osOwnerSaveAppearance(notecard);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key osAgentSaveAppearance([LSLParam(LSLType.Key)] LSL_Key agentId, [LSLParam(LSLType.String)] string notecard)
        {
            return m_OSSL_Functions.osAgentSaveAppearance(agentId, notecard);
        }

        public OSSLPrim Prim;

        [Serializable]
        public class OSSLPrim
        {
            internal ScriptBaseClass OSSL;

            public OSSLPrim(ScriptBaseClass bc)
            {
                OSSL = bc;
                Position = new OSSLPrim_Position(this);
                Rotation = new OSSLPrim_Rotation(this);
            }

            public OSSLPrim_Position Position;
            public OSSLPrim_Rotation Rotation;
            private TextStruct _text;

            public TextStruct Text
            {
                get { return _text; }
                set
                {
                    _text = value;
                    OSSL.llSetText(_text.Text, _text.color, _text.alpha);
                }
            }

            [Serializable]
            public struct TextStruct
            {
                public string Text;
                public LSL_Types.Vector3 color;
                public double alpha;
            }
        }

        [Serializable]
        public class OSSLPrim_Position
        {
            private OSSLPrim prim;
            private LSL_Types.Vector3 Position;

            public OSSLPrim_Position(OSSLPrim _prim)
            {
                prim = _prim;
            }

            private void Load()
            {
                Position = prim.OSSL.llGetPos();
            }

            private void Save()
            {
                /* Remove temporarily until we have a handle to the region size
                if (Position.x > ((int)Constants.RegionSize - 1))
                    Position.x = ((int)Constants.RegionSize - 1);
                if (Position.y > ((int)Constants.RegionSize - 1))
                    Position.y = ((int)Constants.RegionSize - 1);
                 */
                if (Position.z > Constants.RegionHeight)
                    Position.z = Constants.RegionHeight;
                if (Position.x < 0)
                    Position.x = 0;
                if (Position.y < 0)
                    Position.y = 0;
                if (Position.z < 0)
                    Position.z = 0;
                prim.OSSL.llSetPos(Position);
            }

            public double x
            {
                get
                {
                    Load();
                    return Position.x;
                }
                set
                {
                    Load();
                    Position.x = value;
                    Save();
                }
            }

            public double y
            {
                get
                {
                    Load();
                    return Position.y;
                }
                set
                {
                    Load();
                    Position.y = value;
                    Save();
                }
            }

            public double z
            {
                get
                {
                    Load();
                    return Position.z;
                }
                set
                {
                    Load();
                    Position.z = value;
                    Save();
                }
            }
        }

        [Serializable]
        public class OSSLPrim_Rotation
        {
            private OSSLPrim prim;
            private LSL_Types.Quaternion Rotation;

            public OSSLPrim_Rotation(OSSLPrim _prim)
            {
                prim = _prim;
            }

            private void Load()
            {
                Rotation = prim.OSSL.llGetRot();
            }

            private void Save()
            {
                prim.OSSL.llSetRot(Rotation);
            }

            public double x
            {
                get
                {
                    Load();
                    return Rotation.x;
                }
                set
                {
                    Load();
                    Rotation.x = value;
                    Save();
                }
            }

            public double y
            {
                get
                {
                    Load();
                    return Rotation.y;
                }
                set
                {
                    Load();
                    Rotation.y = value;
                    Save();
                }
            }

            public double z
            {
                get
                {
                    Load();
                    return Rotation.z;
                }
                set
                {
                    Load();
                    Rotation.z = value;
                    Save();
                }
            }

            public double s
            {
                get
                {
                    Load();
                    return Rotation.s;
                }
                set
                {
                    Load();
                    Rotation.s = value;
                    Save();
                }
            }
        }

        [LSLFunction(LSLType.String)]
        public string osGetGender([LSLParam(LSLType.Key)] LSL_Key rawAvatarId)
        {
            return m_OSSL_Functions.osGetGender(rawAvatarId);
        }

        [LSLFunction(LSLType.Key)]
        public key osGetMapTexture()
        {
            return m_OSSL_Functions.osGetMapTexture();
        }

        [LSLFunction(LSLType.Key)]
        public key osGetRegionMapTexture([LSLParam(LSLType.String)] string regionName)
        {
            return m_OSSL_Functions.osGetRegionMapTexture(regionName);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osGetRegionStats()
        {
            return m_OSSL_Functions.osGetRegionStats();
        }

        [LSLFunction(LSLType.Vector)]
        public vector osGetRegionSize()
        {
            return m_OSSL_Functions.osGetRegionSize();
        }

        /// <summary>
        /// Returns the amount of memory in use by the Simulator Daemon.
        /// Amount in bytes - if >= 4GB, returns 4GB. (LSL is not 64-bit aware)
        /// </summary>
        /// <returns></returns>
        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osGetSimulatorMemory()
        {
            return m_OSSL_Functions.osGetSimulatorMemory();
        }

        [LSLFunction(LSLType.Void)]
        public void osKickAvatar([LSLParam(LSLType.String)] string FirstName, [LSLParam(LSLType.String)] string SurName, [LSLParam(LSLType.String)] string alert)
        {
            m_OSSL_Functions.osKickAvatar(FirstName, SurName, alert);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetSpeed([LSLParam(LSLType.String)] string UUID, [LSLParam(LSLType.Float)] LSL_Float SpeedModifier)
        {
            m_OSSL_Functions.osSetSpeed(UUID, SpeedModifier);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float osGetHealth([LSLParam(LSLType.String)] string avatar)
        {
            return m_OSSL_Functions.osGetHealth(avatar);
        }

        [LSLFunction(LSLType.Void)]
        public void osCauseDamage([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.Float)] double damage)
        {
            m_OSSL_Functions.osCauseDamage(avatar, damage);
        }

        [LSLFunction(LSLType.Void)]
        public void osCauseHealing([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.Float)] double healing)
        {
            m_OSSL_Functions.osCauseHealing(avatar, healing);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceOtherSit([LSLParam(LSLType.String)] string avatar)
        {
            m_OSSL_Functions.osForceOtherSit(avatar);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceOtherSit([LSLParam(LSLType.String)] string avatar, [LSLParam(LSLType.String)] string target)
        {
            m_OSSL_Functions.osForceOtherSit(avatar, target);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osGetPrimitiveParams([LSLParam(LSLType.Key)] LSL_Key prim, [LSLParam(LSLType.List)] LSL_List rules)
        {
            return m_OSSL_Functions.osGetPrimitiveParams(prim, rules);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetPrimitiveParams([LSLParam(LSLType.Key)] LSL_Key prim, [LSLParam(LSLType.List)] LSL_List rules)
        {
            m_OSSL_Functions.osSetPrimitiveParams(prim, rules);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetProjectionParams([LSLParam(LSLType.Integer)] bool projection, [LSLParam(LSLType.Key)] LSL_Key texture, [LSLParam(LSLType.Float)] double fov, [LSLParam(LSLType.Float)] double focus, [LSLParam(LSLType.Float)] double amb)
        {
            m_OSSL_Functions.osSetProjectionParams(projection, texture, fov, focus, amb);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetProjectionParams([LSLParam(LSLType.Key)]     LSL_Key prim, 
                                          [LSLParam(LSLType.Integer)] bool projection, 
                                          [LSLParam(LSLType.Key)]     LSL_Key texture, 
                                          [LSLParam(LSLType.Float)]   double fov, 
                                          [LSLParam(LSLType.Float)]   double focus, 
                                          [LSLParam(LSLType.Float)]   double amb)
        {
            m_OSSL_Functions.osSetProjectionParams(prim, projection, texture, fov, focus, amb);
        }

        [LSLFunction(LSLType.List)]
        public LSL_List osGetAvatarList()
        {
            return m_OSSL_Functions.osGetAvatarList();
        }

        [LSLFunction(LSLType.String)]
        public LSL_String osUnixTimeToTimestamp([LSLParam(LSLType.Integer)] long time)
        {
            return m_OSSL_Functions.osUnixTimeToTimestamp(time);
        }

        [LSLFunction(LSLType.String)]
        public LSL_String osGetInventoryDesc([LSLParam(LSLType.String)] string item)
        {
            return m_OSSL_Functions.osGetInventoryDesc(item);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osInviteToGroup([LSLParam(LSLType.Key)] LSL_Key agentId)
        {
            return m_OSSL_Functions.osInviteToGroup(agentId);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osEjectFromGroup([LSLParam(LSLType.Key)] LSL_Key agentId)
        {
            return m_OSSL_Functions.osEjectFromGroup(agentId);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetTerrainTexture([LSLParam(LSLType.Integer)] int level, [LSLParam(LSLType.Key)] LSL_Key texture)
        {
            m_OSSL_Functions.osSetTerrainTexture(level, texture);
        }

        [LSLFunction(LSLType.Void)]
        public void osSetTerrainTextureHeight([LSLParam(LSLType.Integer)] int corner, [LSLParam(LSLType.Float)] double low, [LSLParam(LSLType.Float)] double high)
        {
            m_OSSL_Functions.osSetTerrainTextureHeight(corner, low, high);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osIsUUID([LSLParam(LSLType.String)] string thing)
        {
            return m_OSSL_Functions.osIsUUID(thing);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float osMin([LSLParam(LSLType.Float)] double a, [LSLParam(LSLType.Float)] double b)
        {
            return m_OSSL_Functions.osMin(a, b);
        }

        [LSLFunction(LSLType.Float)]
        public LSL_Float osMax([LSLParam(LSLType.Float)] double a, [LSLParam(LSLType.Float)] double b)
        {
            return m_OSSL_Functions.osMax(a, b);
        }

        [LSLFunction(LSLType.Key)]
        public LSL_Key osGetRezzingObject()
        {
            return m_OSSL_Functions.osGetRezzingObject();
        }

        [LSLFunction(LSLType.Void)]
        public void osSetContentType([LSLParam(LSLType.Key)] LSL_Key id, [LSLParam(LSLType.String)] string type)
        {
            m_OSSL_Functions.osSetContentType(id, type);
        }

        [LSLFunction(LSLType.Void)]
        public void osDropAttachment()
        {
            m_OSSL_Functions.osDropAttachment();
        }

        [LSLFunction(LSLType.Void)]
        public void osForceDropAttachment()
        {
            m_OSSL_Functions.osForceDropAttachment();
        }

        [LSLFunction(LSLType.Void)]
        public void osDropAttachmentAt([LSLParam(LSLType.Vector)] vector pos, [LSLParam(LSLType.Rotation)] rotation rot)
        {
            m_OSSL_Functions.osDropAttachmentAt(pos, rot);
        }

        [LSLFunction(LSLType.Void)]
        public void osForceDropAttachmentAt([LSLParam(LSLType.Vector)] vector pos, [LSLParam(LSLType.Rotation)] rotation rot)
        {
            m_OSSL_Functions.osForceDropAttachmentAt(pos, rot);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osListenRegex([LSLParam(LSLType.Integer)] int channelID, [LSLParam(LSLType.String)] string name, [LSLParam(LSLType.String)] string ID, [LSLParam(LSLType.String)] string msg, [LSLParam(LSLType.Integer)] int regexBitfield)
        {
            return m_OSSL_Functions.osListenRegex(channelID, name, ID, msg, regexBitfield);
        }

        [LSLFunction(LSLType.Integer)]
        public LSL_Integer osRegexIsMatch([LSLParam(LSLType.String)] string input, [LSLParam(LSLType.String)] string pattern)
        {
            return m_OSSL_Functions.osRegexIsMatch(input, pattern);
        }
    }
}